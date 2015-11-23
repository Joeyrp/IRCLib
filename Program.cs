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
            server.ConsoleMessageEvent += ConsoleEvent;
            server.MessageEvent += MessageEvent;
            if (!server.Connect("irc.speedrunslive.com", 6667, "BelTest", ""))
            {
                DebugLogger.LogLine("Couldn't connect to the server.");
                return;
            }

            string command = "";
            int logLines = 0;
            do
            {
                if (Console.KeyAvailable)
                {
                    command = Console.ReadLine();
                    if (command.StartsWith("/join") && command.Split('#').Length > 1)
                    {
                       if (server.JoinChannel(command.Split('#')[1]))
                        {
                            Console.WriteLine("* Joined channel: #" + command.Split('#')[1]);
                        }
                    }
                }

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

                server.PollServer();
                
            } while ("exit" != command);

            #endregion

            server.Disconnect();

            DebugLogger.LogLine("Program End");
            return;
        }

        static void MessageEvent(object sender, IRCMessageArgs args)
        {
            Console.WriteLine("*" + args.channel + "* <" + args.fromUser + "> " + args.text);
        }

        static void ConsoleEvent(object sender, IRCConsoleMsgArgs args)
        {
            Console.WriteLine("*CONSOLE* " + args.numeric.ToString() + " " + args.text);
        }
    }
}
