using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IRCLib
{
    class Program
    {
        const string oauth = "oauth:b8gextvroq67b60rmyxifoz2bk850g";
        const string tn = "guru_of_reason";
        

        static void Main(string[] args)
        {
            DebugLogger.Initialize();
            DebugLogger.AddLogFile("Log.txt");

            DebugLogger.LogLine("", true);
            DebugLogger.LogLine("\tNEW INSTANCE STARTED\n");
            DebugLogger.LogLine("", true);

            //////////////////////////////////////////////////
            //      EXAMPLE IRCLib USAGE
            //////////////////////////////////////////////////
            #region IRCServerConnetion TEST
            /*
            IRCServerConnection ircServer = new IRCServerConnection();
            Console.WriteLine("Connecting to irc.speedrunslive.com");

            //if (!ircServer.Connect("irc.twitch.tv", 6667, tn, oauth))
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
            } while ("exit" != command);

            ircServer.Disconnect();
            */
            #endregion

            #region IRCServer TEST

            IRCServer server = new IRCServer();

            Debug.WriteLine("Yo yo");
            Debug.WriteLine("Yo yo");

            // Events
            server.ConsoleMessageEvent += ConsoleEvent;
            server.MessageEvent += MessageEvent;
            server.TopicChangeEvent += TopicChange;
            server.ShowTopicEvent += ShowTopic;
            server.NamesEvent += NamesList;
            server.JoinEvent += Join;
            server.PartEvent += Part;
            server.ChannelModeEvent += ChannelModeEvent;
            server.UserModeEvent += UserModeEvent;

            // Connect
            if (!server.Connect("irc.speedrunslive.com", 6667, "BelTest", ""))
            {
                DebugLogger.LogLine("Couldn't connect to the server.");
                return;
            }

            // Command loop
            string command = "";
            int logLines = 0;
            do
            {
                if (Console.KeyAvailable)
                {
                    // NOTE: Console.ReadLine() should probably not be used as it will 
                    //       block until the user presses enter. This could result in server.PollServer()
                    //       not being called in time to respond to a PING.
                    command = Console.ReadLine();
                    if (command.StartsWith("/join") && command.Split('#').Length > 1)
                    {
                       if (server.JoinChannel(command.Split('#')[1]))
                        {
                            Console.WriteLine("* Joined channel: #" + command.Split('#')[1]);
                        }
                    }
                    else
                    {
                        server.SendRawCommand(command);
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
                
            } while ("exit" != command);

            #endregion

            server.Disconnect();

            DebugLogger.LogLine("Program End");
            return;
        }

        #region EVENT HANDLERS
        static void MessageEvent(object sender, IRCMessageArgs args)
        {
            Console.WriteLine("*" + args.channel + "* <" + args.fromUser + "> " + args.text);
        }

        static void ConsoleEvent(object sender, IRCConsoleMsgArgs args)
        {
            if (366 == args.numeric)
                return;

            Console.WriteLine("*CONSOLE* " + args.numeric.ToString() + " " + args.text);
        }

        static void TopicChange(object sender, IRCTopicArgs args)
        {
            //string lastLine = r.roomLog.Split('\n')[r.roomLog.Split('\n').Length - 1];
            // Console.WriteLine(lastLine);

            Console.WriteLine(" * " + args.byUser + " changes topic to '" + args.topic + "'");
        }

        static void ShowTopic(object sender, IRCTopicArgs args)
        {
            // This event occurs in 2 parts. The first is when the date is -1. This means only the topic and channel are set.
            // When the date is > -1 the channel, byUser, and date are set but the topic is not.
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
            Console.WriteLine(" *" + args.channel + "* " + args.user + " has left the channel" + msg);
        }

        static void ChannelModeEvent(object sender, IRCChannelModeArgs args)
        {
            Console.WriteLine(" *" + args.channel + "* " + args.settingUser + " sets mode " + args.mode);
        }

        static void UserModeEvent(object sender, IRCUserModeArgs args)
        {
            Console.WriteLine(" *" + args.channel + "* " + args.settingUser + " sets mode " + args.mode + " " + args.targetUser);
        }
        #endregion
   
    }
}
