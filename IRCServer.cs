using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRCLib
{
    class IRCRoom
    {
       public string roomName = "";   // NO #
       public string roomModes = "";
       public string roomLog = "";
       public List<string> nickList = new List<string>();
    }

    class IRCServer
    {
        #region DATA MEMBERS
        IRCServerConnection connection = new IRCServerConnection();
        List<IRCRoom> channels = new List<IRCRoom>();
        string consoleLog = "";
        #endregion

        #region PROPERTIES
        public ConnectionInfo ConnectionInfo
        {
            get { return connection.ConnectionInfo; }
            set { }
        }

        public List<IRCRoom> Channels
        {
            get { return channels; }
            set { }
        }
        #endregion

        #region EVENTS
        public delegate void MessageHandler(object sender, IRCMessageArgs args);
        public event MessageHandler MessageEvent;

        public delegate void ConsoleMessageHandler(object sender, IRCConsoleMsgArgs args);
        public event ConsoleMessageHandler ConsoleMessageEvent;
        #endregion

        /// <summary>
        /// Attempt to connect to the given server.
        /// </summary>
        /// <param name="_serverAddress">eg. irc.speedrunslive.com</param>
        /// <param name="_port">usually 6667</param>
        /// <param name="_nick">your nick</param>
        /// <param name="_password">your password. This can be empty if the server uses nickserv.</param>
        /// <returns>true if the connection is successful. If false if there is already a connection if the connection failed. 
        /// There may be more info in the log file.</returns>
        public bool Connect(string _serverAddress, int _port, string _nick, string _password)
        {
            return connection.Connect(_serverAddress, _port, _nick, _password);
        }

        public void Disconnect()
        {
            if (connection.IsConnected)
            {
                connection.Disconnect();
                channels.Clear();
                consoleLog = "";
            }
        }

        /// <summary>
        /// Join an IRC Channel if there is a connection.
        /// </summary>
        /// <param name="channel">Name of the channel ('#' is optional)</param>
        /// <returns>true if the channel was joined.</returns>
        public bool JoinChannel(string channel)
        {
            if (!connection.IsConnected)
                return false;

            if (channel.StartsWith("#"))
                channel = channel.TrimStart('#');

            bool inChannel = false;
            foreach (IRCRoom room in channels)
            {
                if (room.roomName == channel)
                {
                    inChannel = true;
                    break;
                }
            }

            if (inChannel)
                return false;

            IRCRoom chan = new IRCRoom();
            chan.roomName = channel;
            channels.Add(chan);


            connection.SendMessage("JOIN #" + channel);

            return true; 
        }

        /// <summary>
        /// Leave an IRC channel
        /// </summary>
        /// <param name="channel">Name of the channel ('#' is optional)</param>
        /// <param name="partMsg">A message to be shown on leaving.</param>
        /// <returns>true if the channel was left</returns>
        public bool LeaveChannel(string channel, string partMsg)
        {
            if (!connection.IsConnected)
                return false;

            if (channel.StartsWith("#"))
                channel = channel.TrimStart('#');
            
            foreach (IRCRoom room in channels)
            {
                if (room.roomName == channel)
                {
                    channels.Remove(room);
                    connection.SendMessage("PART #" + channel + " " + partMsg);
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Gets and parses messages from the server.
        /// This method should be called regularly or the connection may time out.
        /// </summary>
        public void PollServer()
        {
            if (!connection.IsConnected)
                return;

            string msg = connection.PollServer();

            if ("" == msg || msg.StartsWith("PING"))
                return;



            #region PARSE MESSAGE

            string[] spaceSplit = msg.Split(' ');

            string source = spaceSplit[0];
            
            // Source will contain an "!" if this is from a user.
            // Otherwise it's from the server.
            if (source.Contains("!"))
            {
                string user = source.Split('!')[0].TrimStart(':');
                string cmd = spaceSplit[1];

                if ("PRIVMSG" == cmd)
                {
                    string ch = spaceSplit[2].TrimStart('#');
                    string text = "";
                    for (int i = 3; i < spaceSplit.Length; i++)
                    {
                        text += spaceSplit[i] + ' ';
                    }
                    text = text.TrimEnd(' ');
                    text = text.TrimStart(':');

                    foreach (IRCRoom room in channels)
                    {
                        if (room.roomName == ch)
                        {
                            room.roomLog += "\n<" + user + "> " + text;

                            // EVENT MESSAGE
                            if (MessageEvent != null)
                            {
                                IRCMessageArgs ma = new IRCMessageArgs();
                                ma.channel = ch;
                                ma.fromUser = user;
                                ma.text = text;

                                MessageEvent(this, ma);
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                // TODO: Make msg more readable before adding to console log.
                // consoleLog += "\n" + msg;

                if (source.TrimStart(':') == connection.ConnectionInfo.serverAddress)
                {
                    int numeric = 0;
                    string args = "";
                    if (spaceSplit.Length < 3 || !int.TryParse(spaceSplit[1], out numeric))
                    {
                        DebugLogger.LogLine("Unknown server message format: " + msg);
                        ParseNumeric(-1, msg);
                        return;
                    }

                    for (int i = 2; i < spaceSplit.Length; i++)
                    {
                        args += spaceSplit[i] + ' ';
                    }
                    args = args.Trim(' ');
                    ParseNumeric(numeric, args);
                    DebugLogger.LogLine(msg);
                }

            }

            #endregion

        }

        private void ParseNumeric(int numeric, string args)
        {
            switch (numeric)
            {
                #region NO EXTRA ACTIONS NEEDED

                case Numerics.RPL_WELCOME:
                case Numerics.RPL_YOURHOST:
                case Numerics.RPL_CREATED:
                case Numerics.RPL_MYINFO:
                {
                    consoleLog += "\n" + args;

                    if (ConsoleMessageEvent != null)
                    {
                        IRCConsoleMsgArgs a = new IRCConsoleMsgArgs();
                        a.numeric = numeric;
                        a.text = args;

                        ConsoleMessageEvent(this, a);
                    }
                }
                break;


                #endregion

                default:
                {
                    consoleLog += "\n[UNHANDLED] " + args;

                    if (ConsoleMessageEvent != null)
                    {
                        IRCConsoleMsgArgs a = new IRCConsoleMsgArgs();
                        a.numeric = numeric;
                        a.text = args;

                        ConsoleMessageEvent(this, a);
                    }
                }
                break;
            }
        }
    }

    public class IRCMessageArgs
    {
        public string channel;
        public string fromUser;
        public string text;
    }

    public class IRCConsoleMsgArgs
    {
        public int numeric;
        public string text;
    }

    /// <summary>
    /// These numeric server messages are implemented unless otherwise noted.
    /// </summary>
    static class Numerics
    {
        /// <summary>
        /// "Welcome to the Internet Relay Network
        ///     <nick>!<user>@<host>"
        /// </summary>
        public const int RPL_WELCOME = 001;

        /// <summary>
        /// "Your host is <servername>, running version <ver>"
        /// </summary>
        public const int RPL_YOURHOST = 002;

        /// <summary>
        /// "This server was created <date>"
        /// </summary>
        public const int RPL_CREATED = 003;

        /// <summary>
        /// "<servername> <version> <available user modes>
        ///    <available channel modes>"
        /// </summary>
        public const int RPL_MYINFO = 004;

    }
}
