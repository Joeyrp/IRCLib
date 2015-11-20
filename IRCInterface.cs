using IRCLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OldIRC
{

    public class IRCConnection
    {
        public delegate void LogEventHandler(object sender, string msg);
        public event LogEventHandler LogEvent;

        public delegate void MessageHandler(object sender, IRCMessageArgs args);
        public event MessageHandler MessageEvent;

        public delegate void ConnectionEventHandler(object sender, IRCEvent e);
        public event ConnectionEventHandler ConnectionEvent;

        public event LogEventHandler ResponseEvent;

        TcpClient client = null;
        StreamReader inputStream;
        StreamWriter outputStream;
        string server = "irc.speedrunslive.com";
        int port = 6667;
        string nick = "guruofreason";
        volatile bool disconnect = false;

        Thread thread;

        string ResponseBuffer;
        bool WaitingForResponse = false;

        public bool Connect()
        {
            if (client != null && client.Connected)
                return false;

            DebugLogger.LogLine("Creating new tcp client and starting work thread");

            disconnect = false;
            thread = new Thread(new ThreadStart(WorkThread));
            thread.Start();

            return true;
        }

        public void Disconnect()
        {
            SendCommand("PING");
            disconnect = true;
            thread.Join();
        }

        public void SendCommand(string command, bool response = false)
        {
            if (null == outputStream)
                return;

            if (response)
            {
                WaitingForResponse = true;
                ResponseBuffer = "";
            }

            outputStream.WriteLine(command);
            outputStream.Flush();
        }
        
        private void WorkThread()
        {
            client = new TcpClient(server, port);

            if (!client.Connected)
            {
                if (ConnectionEvent != null)
                {
                    ConnectionEvent(this, IRCEvent.IRC_CONNECTION_FAILED);
                }
                return;
            }

            inputStream = new StreamReader(client.GetStream());
            outputStream = new StreamWriter(client.GetStream());

            outputStream.WriteLine("PASS go321kickit");
            outputStream.Flush();
            outputStream.WriteLine("NICK " + nick);
            outputStream.Flush();
            outputStream.WriteLine("USER 0 " + nick + " * :test");
            outputStream.Flush();
            

            // COMMAND LOOP
            bool connected = false;
            string buffer;
            while ((buffer = inputStream.ReadLine()) != null)
            {
                DebugLogger.LogLine(buffer + "\n");

                if (buffer[0] == '\0')
                    continue;

                if (WaitingForResponse)
                {
                    ResponseBuffer += buffer;

                    if (buffer.Contains(":End"))
                    {
                        WaitingForResponse = false;
                        
                        if (ResponseEvent != null)
                        {
                            ResponseEvent(this, ResponseBuffer);
                            ResponseBuffer = "";
                        }
                    }
                }

                if (LogEvent != null)
                {
                    LogEvent(this, buffer);
                }

                if (disconnect)
                {
                    ConnectionEvent(this, IRCEvent.IRC_DISCONNECTED);
                    DebugLogger.LogLine("\nDisconnect flag set\n");
                    break;
                }

                if (buffer.StartsWith("PING"))
                {
                    string response = buffer.Replace("PING", "PONG");
                    outputStream.WriteLine(response);
                    outputStream.Flush();
                }

                if (buffer[0] == ':')
                {
                    if (buffer.StartsWith(":GuruOfLive") && !connected)
                    {
                        //outputStream.WriteLine("PRIVMSG nickserv identify go321kickit");
                        outputStream.Flush();
                        outputStream.WriteLine("JOIN #GuruOfLive");
                        outputStream.Flush();
                        outputStream.Write("MODE " + nick + " +B");
                        outputStream.Flush();
                        connected = true;

                        ConnectionEvent(this, IRCEvent.IRC_CONNECTED);
                    }


                    if (buffer.Split(' ')[1] == "001")
                    {

                        continue;
                    }

                    // Example of recieved PRIVMSG
                    //:Belthasar!Belthasar@SRL-469F0442.res.bhn.net PRIVMSG #guruoflive :Testing
                    if (buffer.Contains("PRIVMSG"))
                    {
                        
                        if (MessageEvent != null)
                        {
                            IRCMessageArgs message = new IRCMessageArgs();

                            // Don't try to parse a PM
                            if (!buffer.Contains('#'))
                                continue;
                            
                            // BUG - breaks stuff if a channel name has '#' in it.
                            string[] msg = buffer.Split('#')[1].Split(':');
                            message.fromUser = buffer.Split('!')[0].TrimStart(':');
                            message.channel = msg[0];

                            
                            string[] core = buffer.Split('#');
                            message.text = core[1].Split(':')[1];
                            for (int j = 2; j < core.Length; j++)
                            {
                                if ("" == core[j])
                                    continue;

                                for (int i = 0; i < core[j].Length; i++)
                                {
                                    message.text += core[j][i];
                                }
                            }
                            MessageEvent(this, message);
                        }
                    }

                }
            }
            
            // DISCONNECT
            inputStream.Close();
            outputStream.Close();
            client.Close();
        }
    }

    public class IRCMessageArgs
    {
        public string channel;
        public string fromUser;
        public string text;
    }

    public enum IRCEvent{ IRC_CONNECTED, IRC_DISCONNECTED, IRC_CONNECTION_FAILED };
    
}
