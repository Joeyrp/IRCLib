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

            DebugLogger.LogLine("");
            DebugLogger.LogLine("\tNEW INSTANCE STARTED\n");
            DebugLogger.LogLine("");
            OldIRC.IRCConnection oldIrc = new OldIRC.IRCConnection();
            oldIrc.Connect();
            Thread.Sleep(10000);
            oldIrc.SendCommand("QUIT");
            Thread.Sleep(2000);
            DebugLogger.LogLine("Program End");
            return;
        }
    }
}
