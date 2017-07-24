/******************************************************************************
*	File		-	Program.cs
*	Author		-	Joey Pollack
*	Date		-	11/19/2015 (m/d/y)
*	Mod Date	-	7/14/2017 (m/d/y)
*	Description	-	Contains IRCLib usage examples
******************************************************************************/
using System;

namespace IRCLib
{
    class Program
    {
        
        static void Main(string[] args)
        {
            DebugLogger.Initialize();
            DebugLogger.AddLogFile("ExampleProgramLog.txt");

            DebugLogger.LogLine("", true);
            DebugLogger.LogLine("\tNEW INSTANCE STARTED\n");
            DebugLogger.LogLine("", true);

            //////////////////////////////////////////////////
            //      EXAMPLE IRCLib USAGE
            //////////////////////////////////////////////////

            // Twitch credentials
            const string tn = "[valid twitch username]";
            const string oauth = "oauth:[valid twitch oauth]";


            #region IRCServerConnetion TEST -- Recommended to use IRCServer instead

            // NOTE: IRCServerConnection can be used directly but this will only give
            //       you raw server messages that you will have to parse on your own.


            /*
            IRCServerConnection ircServer = new IRCServerConnection();
            Console.WriteLine("Connecting to irc.speedrunslive.com");

            if (!ircServer.Connect("irc.speedrunslive.com", 6667, "BelTest", ""))
            {
                Console.WriteLine("Could not connect to the server.");
                Console.ReadLine();
                return;
            }


            string command = "";
            do
            {
                if (Console.KeyAvailable)
                {
                    command = Console.ReadLine();
                    ircServer.SendMessage(command);
                }

                string response = ircServer.PollServer();
                if (response != "")
                {
                    Console.WriteLine(response);
                    DebugLogger.LogLine(response);
                }
            } while ("quit" != command);

            ircServer.Disconnect();
            */
            #endregion

            #region IRCServer EXAMPLE

            // NOTE: This is a fairly bare-bones example of how to use the IRCServer class.
            //       It covers all of the main features even if the result isn't very usable.
            //       The most correct way to set up an event loop would be to put the IRCServer
            //       (specifically the server.PollServer() method) and the user input on 2 separate threads.

            IRCServer server = new IRCServer();

            // Events
            server.ConsoleMessageEvent += ConsoleEvent;
            server.MessageEvent += MessageEvent;
            server.TopicChangeEvent += TopicChange;
            server.ShowTopicEvent += ShowTopic;
            server.NamesEvent += NamesList;
            server.JoinEvent += Join;
            server.PartEvent += Part;
            server.KickEvent += Kick;
            server.ChannelModeEvent += ChannelModeEvent;
            server.UserModeEvent += UserModeEvent;

            // Connect
            //if (!server.Connect("irc.speedrunslive.com", 6667, "IRCLibTest1", ""))
            //if (!server.Connect("irc.twitch.tv", 6667, tn, oauth))
            if (!server.Connect("chat.freenode.net", 6667, "IRCLibTest1", ""))
            {
                DebugLogger.LogLine("Couldn't connect to the server.", "ExampleProgramLog.txt");
                Console.WriteLine("Couldn't connect to the server: " + server.ConnectionInfo.serverAddress);
                return;
            }

            

            // Command loop: Very basic client loop.
            // We are only handling a few commands: /nick, /join, /part, /raw and /quit
            // Everything else is considered a message and sent to the last joined channel.
            // As stated above this should really be separated into 2 threads - one for the user
            // input and another to execute server methods.
            string command = "";
            do
            {
                if (Console.KeyAvailable)
                {
                    // NOTE: Console.ReadLine() should probably not be used as it will 
                    //       block until the user presses enter. This could result in server.PollServer()
                    //       not being called in time to respond to a PING.
                    command = Console.ReadLine();

                    // Change nick
                    if (command.ToLower().StartsWith("/nick") && command.Split(' ').Length > 1)
                    {
                        server.ChangeNick(command.Split(' ')[1]);
                    }
                    // Join the given channel
                    else if (command.ToLower().StartsWith("/join") && command.Split(' ').Length > 1)
                    {
                        if (server.JoinChannel(command.Split(' ')[1]))
                        {
                            Console.WriteLine("* Joined channel: " + command.Split(' ')[1]);
                        }
                    }
                    // Leave the given channel
                    else if (command.ToLower().StartsWith("/part") && command.Split(' ').Length > 1)
                    {
                        string msg = "";
                        if (command.Split(' ').Length > 2)
                        {
                            char[] sep = { ' ' };
                            msg = command.Split(sep, 3)[2];
                        }
                        string channel = command.Split(' ')[1];

                        if (server.LeaveChannel(channel, msg))
                        {
                            Console.WriteLine("* Leaving channel: " + channel);
                        }
                        else
                        {
                            Console.WriteLine(" Not in channel " + channel);
                        }
                    }
                    // Send the following to the server without modification
                    // this can be used to send any unsupported command to the server
                    // WARNING: Sending raw commands can put the IRCServer object in a bad state
                    //          because it will not process these commands at all.
                    //          For instance, if you use this to send a NICK command
                    //          the IRCserver object will be unaware of the nick change
                    //          and will still be using the old nick.
                    else if (command.ToLower().StartsWith("/raw"))
                    {
                        char[] sep = { ' ' };
                        string[] raw = command.Split(sep, 2);

                        if (raw.Length > 1)
                            server.SendRawCommand(raw[1]);
                    }
                    // Everything else is considered a message sent to a channel
                    else if (!command.StartsWith("/"))
                    {
                        // Only sending messages to the last joined channel.
                        // This could be improved to keep track of the current channel the user is viewing
                        // but this example is not going to cover that.
                        if (server.Channels.Count > 0)
                            server.SendMessageTo(server.Channels[server.Channels.Count - 1].Name, command);
                    }
                }

                #region NOT RECOMMENDED
                // Possible way to get messages from a channel.
                // Handling events is recommended over this however.
                //if (server.Channels.Count > 0)
                //{
                //    string[] lines = server.Channels[0].roomLog.Split('\n');
                //    int newLines = lines.Length;
                //    if (newLines > logLines)
                //    {
                //        for (int i = logLines; i < newLines; i++)
                //        {
                //            Console.WriteLine(lines[i]);
                //        }

                //        logLines = newLines;
                //    }
                //}
                #endregion

                server.PollServer();
                
            } while ("/quit" != command);

            server.Disconnect();

            #endregion


            DebugLogger.LogLine("Program End", "ExampleProgramLog.txt");
            return;
        }

        #region EVENT HANDLERS
        static void MessageEvent(object sender, IRCMessageArgs args)
        {
            if (args.isNotice)
                Console.WriteLine("*" + args.channel + "* -" + args.fromUser + "- " + args.text);
            else
                Console.WriteLine("*" + args.channel + "* <" + args.fromUser + "> " + args.text);
        }

        // Any numeric (or other response) that does not have an event associated with it will be sent here
        static void ConsoleEvent(object sender, IRCConsoleMsgArgs args)
        {
            if (Numerics.RPL_ENDOFNAMES == args.numeric)
                return;

            // The Twitch.tv server has extra capabilities that need to be
            // explicitly requested. This can be done when the Welcome message
            // is received.
            if (Numerics.RPL_WELCOME == args.numeric)
            {
                IRCServer server = (IRCServer)sender;
                if (server.ConnectionInfo.serverAddress == "irc.twitch.tv")
                {
                    server.SendRawCommand("CAP REQ :twitch.tv/membership");
                    server.SendRawCommand("CAP REQ :twitch.tv/tags");
                    server.SendRawCommand("CAP REQ :twitch.tv/commands");
                }
            }
            
            Console.WriteLine("*CONSOLE* " + args.numeric.ToString() + " " + args.text);
        }

        static void TopicChange(object sender, IRCTopicArgs args)
        {
            Console.WriteLine(" * " + args.byUser + " changes topic to '" + args.topic + "'");
        }

        static void ShowTopic(object sender, IRCTopicArgs args)
        {
            //  This event occurs in 2 parts (and so will be recieved twice). The first time the date is -1. 
            //  This means only the topic and channel are available.
            //  The second time the date is > -1 and the channel, byUser, and date are available but the topic is not.
            if (-1 == args.date)
            {
                Console.WriteLine(" * " + "Topic is '" + args.topic + "'");
            }
            else
            {
                DateTime epoch = new DateTime(1970, 1, 1).ToLocalTime();
                DateTime setAt = epoch.AddSeconds(args.date).ToLocalTime();
                Console.WriteLine(" * Topic set by " + args.byUser + " on " + setAt);
            }
        }

        static void NamesList(object sender, IRCNamesArgs args)
        {
            Console.WriteLine(" * Nick List:");
            foreach (string n in args.names)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine(" * Nick list received!");
        }

        static void Join(object sender, IRCJoinArgs args)
        {
            Console.WriteLine(" *" + args.channel + "* " + args.user + " has joined the channel");
        }

        static void Part(object sender, IRCPartArgs args)
        {
            string msg = "";
            if (args.partMsg != "")
            {
                msg = "(" + args.partMsg + ")";
            }
            Console.WriteLine(" *" + args.channel + "* " + args.user + " has left the channel " + msg);
        }

        static void Kick(object sender, IRCKickArgs args)
        {
            string msg = "";
            if (args.kickMsg != "")
            {
                msg = " (" + args.kickMsg + ")";
            }

            if (args.thisClient)
            {
                Console.WriteLine(" *You were kicked from " + args.channel + " by " + args.kickingUser + msg);
            }
            else
            {
                Console.WriteLine(" *" + args.channel + "* " + args.kickedUser + " was kicked by " + args.kickingUser + msg);
            }
        }

        static void ChannelModeEvent(object sender, IRCChannelModeArgs args)
        {
            if (args.settingUser != "")
            {
                Console.WriteLine(" *" + args.channel + "* " + args.settingUser + " sets mode " + args.modes);
            }
            else
            {
                Console.WriteLine(" * Modes for " + args.channel + " are " + args.modes);
            }
        }

        static void UserModeEvent(object sender, IRCUserModeArgs args)
        {
            if (args.settingUser != "")
            {
                Console.WriteLine(" *" + args.channel + "* " + args.settingUser + " sets mode " + args.modes + " " + args.targetUser);
            }
            else
            {
                Console.WriteLine(" * Modes for " + args.targetUser + " in channel " + args.channel + " are " + args.modes);
            }
        }
        #endregion
   
    }
}
