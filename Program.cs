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

            IRCServer ircServer = new IRCServer();
            ircServer.Connect("irc.speedrunslive.com", 6667, "BelTest", "");


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
                    Console.WriteLine(response);

            } while ("exit" != command);

            ircServer.Disconnect();
            
            DebugLogger.LogLine("Program End");
            return;
        }
    }
}
