using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO: *DONE - TESTING* Add support for all channel prefixes 
// TODO: *DONE - TESTING* Add support for kick and ban (should be handled already through user modes) events
// TODO: *DONE* Add support for NOTICE
// TODO: CTCP Version, Time and Ping

namespace IRCLib
{
    public class IRCUser
    {
        public enum USERMODES { VOICE, HALFOP, OPERATOR, SOP, ADMIN = SOP, FOUNDER, OWNER = FOUNDER, INVALID };

        // Contains mode prefix (+, @, etc)
        public string nick;
        public List<USERMODES> modes = new List<USERMODES>();
    }
    public class IRCRoom
    {
       public string Name = "";   // WITH PREFIX
       public string Modes = "";
        public string topic = "";
       public string roomLog = "";
       public List<IRCUser> nickList = new List<IRCUser>();
       //public List<string> nickList = new List<string>();
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
        public delegate void ConsoleMessageHandler(object sender, IRCConsoleMsgArgs args);
        public delegate void TopicEventHandler(object sender, IRCTopicArgs args);
        public delegate void NamesEventHandler(object sender, IRCNamesArgs args);
        public delegate void JoinEventHandler(object sender, IRCJoinArgs args);
        public delegate void PartEventHandler(object sender, IRCPartArgs args);
        public delegate void KickEventHandler(object sender, IRCKickArgs args);
        // public delegate void QuitEventHandler(object sender, IRCQ args);
        public delegate void UserModeEventHandler(object sender, IRCUserModeArgs args);
        public delegate void ChannelModeEventHandler(object sender, IRCChannelModeArgs args);
        public delegate void ChannelCreationDateEventHandler(object sender, IRCChannelCreationArgs args);



        /// <summary>
        /// A user message received from a channel.
        /// </summary>
        public event MessageHandler MessageEvent;

        /// <summary>
        /// No specific events from the server.
        /// IRCServer ignores these messages but passes them on to anyone handling this event.
        /// </summary>
        public event ConsoleMessageHandler ConsoleMessageEvent;

        /// <summary>
        /// A user changed the channel topic.
        /// </summary>
        public event TopicEventHandler TopicChangeEvent;

        /// <summary>
        /// The user requested the topic (or the channel was just joined).
        /// This event has 2 parts: the first time it is raised it contains
        ///     the channel and the topic, the second time it will contain
        ///     the channel, date, and user that set the topic.
        ///     date will be -1 if this is the first part of the event.
        /// </summary>
        public event TopicEventHandler ShowTopicEvent;

        /// <summary>
        /// The server sent a list of user names in a given channel.
        /// </summary>
        public event NamesEventHandler NamesEvent;

        /// <summary>
        /// Raised when a user joins a channel.
        /// </summary>
        public event JoinEventHandler JoinEvent;

        /// <summary>
        /// Raised when a user leaves a channel.
        /// </summary>
        public event PartEventHandler PartEvent;

        /// <summary>
        /// Raised what a user is kicked from a channel
        /// </summary>
        public event KickEventHandler KickEvent;

        /// <summary>
        /// Raised when a user mode is changed.
        /// </summary>
        public event UserModeEventHandler UserModeEvent;

        /// <summary>
        /// Raised when a channel mode is changed or queried. If it was a query then the settingUser will be "".
        /// </summary>
        public event ChannelModeEventHandler ChannelModeEvent;

        /// <summary>
        /// Raised after the channel mode was queried.
        /// </summary>
        public event ChannelCreationDateEventHandler ChannelCreationEvent;
        #endregion

        public IRCServer()
        {
            DebugLogger.AddLogFile("IRCServer full log.txt", true);
        }

        /// <summary>
        /// Attempt to connect to the given server.
        /// </summary>
        /// <param name="_serverAddress">eg. irc.speedrunslive.com</param>
        /// <param name="_port">usually 6667</param>
        /// <param name="_nick">your nick</param>
        /// <param name="_password">your password if one is required</param>
        /// <returns>true if the connection is successful. False if there is already a connection or the connection failed. 
        /// There may be more info in the log file.</returns>
        public bool Connect(string _serverAddress, int _port, string _nick, string _password, string _name = "Default")
        {
            DebugLogger.LogLine("\nConnecting to " + _serverAddress, "IRCServer full log.txt");
            return connection.Connect(_serverAddress, _port, _nick, _password, _name);
        }

        public void Disconnect()
        {
            if (connection.IsConnected)
            {
                DebugLogger.LogLine("Server Disconnecting", "IRCServer full log.txt");
                connection.Disconnect();
                channels.Clear();
                consoleLog = "";
            }
        }

        /// <summary>
        /// Change nick for this connection
        /// </summary>
        /// <param name="nick">The nick to change to</param>
        public void ChangeNick(string nick)
        {
            string oldNick = connection.ConnectionInfo.nick;
            connection.ConnectionInfo.nick = nick;
            SendRawCommand("NICK " + nick);

            // update nick lists
            foreach (IRCRoom c in channels)
            {
                foreach (IRCUser u in c.nickList)
                {
                    // handle the possiblity of a mode prefix on the nick
                    string checkNick = u.nick;
                    string prefix = checkNick[0].ToString();
                    if (IsUserModePrefix(prefix))
                    {
                        checkNick = checkNick.TrimStart(checkNick[0]);
                    }
                    //

                    if (checkNick == oldNick)
                    {
                        u.nick = prefix + nick;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Join an IRC Channel if there is a connection.
        /// </summary>
        /// <param name="channel">Name of the channel</param>
        /// <returns>true if the channel was joined.</returns>
        public bool JoinChannel(string channel)
        {
            if (!connection.IsConnected)
                return false;

            // NOTE: Removed to support multiple channel prefixes
            //if (channel.StartsWith("#"))
            //    channel = channel.TrimStart('#');

            bool inChannel = false;
            foreach (IRCRoom room in channels)
            {
                if (room.Name == channel)
                {
                    inChannel = true;
                    break;
                }
            }

            if (inChannel)
                return false;

            IRCRoom chan = new IRCRoom();
            chan.Name = channel;
            channels.Add(chan);


            connection.SendMessage("JOIN " + channel);
            connection.SendMessage("MODE " + channel);

            return true; 
        }

        /// <summary>
        /// Leave an IRC channel
        /// </summary>
        /// <param name="channel">Name of the channel</param>
        /// <param name="partMsg">A message to be shown on leaving.</param>
        /// <returns>true if the channel was left</returns>
        public bool LeaveChannel(string channel, string partMsg)
        {
            if (!connection.IsConnected)
                return false;

            // NOTE: Removed to support multiple channel prefixes
            //if (channel.StartsWith("#"))
            //    channel = channel.TrimStart('#');

            foreach (IRCRoom room in channels)
            {
                if (room.Name == channel)
                {
                    channels.Remove(room);
                    connection.SendMessage("PART " + channel + " " + partMsg);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Send a message to the given channel
        /// </summary>
        /// <param name="target">user nick or channel name (WITH the channel prefix)</param>
        /// <param name="msg">message to send</param>
        /// <param name="asNotice">true to send NOTICE instead of PRIVMSG</param>
        public void SendMessageTo(string target, string msg, bool asNotice = false)
        {
            string command = "";
            if (asNotice)
                command = "PRIVMSG " + target + " :" + msg;
            else
                command = "NOTICE " + target + " :" + msg;

            SendRawCommand(command);
        }

        /// <summary>
        /// Send a command to the server as is - with no changes.
        /// </summary>
        /// <param name="cmd">The raw command to send</param>
        public void SendRawCommand(string cmd)
        {
            if (connection.IsConnected)
                connection.SendMessage(cmd);
        }


        /// <summary>
        /// Gets and parses messages from the server. Invokes events based on the message.
        /// This method should be called regularly or the connection may time out.
        /// </summary>
        public void PollServer()
        {
            if (!connection.IsConnected)
                return;

            string msg = connection.PollServer();

            if ("" == msg || msg.StartsWith("PING"))
                return;


            DebugLogger.LogLine(msg, "IRCServer full log.txt");


            #region PARSE MESSAGE

            string[] spaceSplit = msg.Split(' ');
            string source = spaceSplit[0];
            
            // Source will contain an "!" if this is from or about a user.
            // Otherwise it's a numeric from the server.
            if (source.Contains("!"))
            {
                string user = source.Split('!')[0].TrimStart(':');
                string cmd = spaceSplit[1];

                ParseUserMessage(user, cmd, msg, spaceSplit);
            }
            else
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

            #endregion

        }

        private void ParseUserMessage(string user, string cmd, string msg, string[] spaceSplit)
        {
            #region MODE
            if ("MODE" == cmd)
            {
                if (spaceSplit.Length < 5)
                {
                    DebugLogger.LogLine("MODE Command with not enough arguments: " + msg, "IRCServer full log.txt");
                    return;
                }
                string ch = spaceSplit[2];
                string mode = spaceSplit[3];
                string targetUser = spaceSplit[4];

                for (int i = 5; i < spaceSplit.Length; i++)
                {
                    targetUser += " " + spaceSplit[i];
                }

                // Is this a channel mode?
                if ("" == targetUser)
                {
                    foreach (IRCRoom room in channels)
                    {
                        if (room.Name == ch)
                        {
                            room.Modes = UpdateChannelModes(room.Modes, mode);
                        }
                    }

                    if (ChannelModeEvent != null)
                    {
                        IRCChannelModeArgs a = new IRCChannelModeArgs();
                        a.channel = ch;
                        a.modes = mode;
                        a.settingUser = user;
                        ChannelModeEvent(this, a);
                    }
                }
                else
                {
                    SendRawCommand("NAMES #" + ch);

                    foreach (IRCRoom room in channels)
                    {
                        if (room.Name == ch)
                        {
                            foreach (IRCUser u in room.nickList)
                            {
                                if (u.nick == targetUser)
                                {
                                    u.modes.Add(GetModeFromPrefix(mode));
                                }
                            }
                        }
                    }

                    if (UserModeEvent != null)
                    {
                        IRCUserModeArgs a = new IRCUserModeArgs();
                        a.channel = ch;
                        a.modes = mode;
                        a.settingUser = user;
                        a.targetUser = targetUser;
                        UserModeEvent(this, a);
                    }
                }

            }
            #endregion

            #region JOIN PART KICK QUIT
            if ("JOIN" == cmd)
            {
                string ch = spaceSplit[2];
                foreach (IRCRoom room in channels)
                {
                    if (room.Name == ch)
                    {
                        IRCUser joinedUser = new IRCUser();
                        joinedUser.nick = user;
                        room.nickList.Add(joinedUser);
                        if (JoinEvent != null)
                        {
                            IRCJoinArgs a = new IRCJoinArgs();
                            a.channel = ch;
                            a.user = user;
                            JoinEvent(this, a);
                        }
                        break;
                    }
                }
            }

            if ("PART" == cmd)
            {
                string ch = spaceSplit[2];
                foreach (IRCRoom room in channels)
                {
                    if (room.Name == ch)
                    {
                        //room.nickList.Remove(user);
                        foreach (IRCUser u in room.nickList)
                        {
                            if (u.nick == user)
                            {
                                room.nickList.Remove(u);
                                break;
                            }
                        }
                        if ("PART" == cmd)
                        {
                            IRCPartArgs a = new IRCPartArgs();
                            a.channel = ch;
                            a.user = user;
                            a.partMsg = "";

                            string[] s = msg.Split(':');
                            if (s.Length > 2)
                                a.partMsg = s[2];

                            DebugLogger.LogLine(a.user + " left channel " + a.channel + " (" + a.partMsg + ")");

                            if (PartEvent != null)
                                PartEvent(this, a);
                        }
                        break;
                    }
                }
            }

            if ("KICK" == cmd)
            {
                // TODO: HANDLE KICK EVENT
                // kicked user starts at [3] reason is after second ':'
                // kicking user is at [0] after first ':' and ends with '!'
                string ch = spaceSplit[2];
                string kickedUser = spaceSplit[3];

                foreach (IRCRoom room in channels)
                {
                    if (room.Name == ch)
                    {

                        IRCKickArgs a = new IRCKickArgs();

                        if (connection.ConnectionInfo.nick == kickedUser)
                        {
                            // We were kicked
                            channels.Remove(room);

                            a.thisClient = true;
                        }
                        else
                        {
                            // Some one else was kicked
                            foreach (IRCUser u in room.nickList)
                            {
                                if (u.nick == kickedUser)
                                {
                                    room.nickList.Remove(u);
                                    break;
                                }
                            }

                            a.thisClient = false;
                        }

                        a.channel = ch;
                        a.kickedUser = kickedUser;
                        a.kickMsg = "";
                        a.kickingUser = "";

                        // spaceSplit[4].Split(':')[2];
                        string[] s = msg.Split(':');
                        if (s.Length > 2)
                            a.kickMsg = s[2];

                        string rawkicker = spaceSplit[0].TrimStart(':');
                        foreach (char c in rawkicker)
                        {
                            if (c == '!')
                                break;

                            a.kickingUser += c;
                        }

                        DebugLogger.LogLine(a.kickedUser + " was kicked from channel " + a.channel + " by " + a.kickingUser + " (" + a.kickMsg + ")");

                        if (KickEvent != null)
                            KickEvent(this, a);

                        break;
                    }
                }

            
            }

            if ("QUIT" == cmd)
            {
                // TODO: QUIT
            }

            #endregion

            #region TOPIC
            if ("TOPIC" == cmd)
            {
                string ch = spaceSplit[2];
                string text = "";
                for (int i = 3; i < spaceSplit.Length; i++)
                {
                    text += spaceSplit[i] + ' ';
                }
                text = text.TrimEnd(' ');
                text = text.TrimStart(':');

                foreach (IRCRoom room in channels)
                {
                    if (room.Name == ch)
                    {
                        room.roomLog += " * " + user + " changes topic to " + text;
                        room.topic = text;
                        if (TopicChangeEvent != null)
                        {
                            IRCTopicArgs a = new IRCTopicArgs();
                            a.topic = text;
                            a.channel = ch;
                            a.byUser = user;
                            a.date = -1;

                            TopicChangeEvent(this, a);
                        }
                    }
                }
            }
            #endregion

            #region PRIVMSG/NOTICE
            if ("PRIVMSG" == cmd || "NOTICE" == cmd)
            {
                string ch = spaceSplit[2];
                string text = "";
                for (int i = 3; i < spaceSplit.Length; i++)
                {
                    text += spaceSplit[i] + ' ';
                }
                text = text.TrimEnd(' ');
                text = text.TrimStart(':');

                foreach (IRCRoom room in channels)
                {
                    if (room.Name == ch)
                    {
                        user = GetUserFromList(room.nickList, user);
                        room.roomLog += "\n<" + user + "> " + text;

                        // EVENT MESSAGE
                        if (MessageEvent != null)
                        {
                            IRCMessageArgs ma = new IRCMessageArgs();
                            ma.channel = ch;
                            ma.fromUser = user;
                            ma.text = text;
                            ma.room = room;
                            ma.isNotice = ("NOTICE" == cmd) ? true : false;

                            MessageEvent(this, ma);
                        }
                        break;
                    }
                }
            }
            #endregion
        }

        private void ParseNumeric(int numeric, string args)
        {
            switch (numeric)
            {
                #region NO SPECIAL ARGS NEEDED

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

                #region SPECIAL CASES

                case Numerics.RPL_TOPIC:
                    {
                        consoleLog += "\n" + args;
                        IRCTopicArgs a = new IRCTopicArgs();
                        //a.channel = args.Split('#')[1].Split(' ')[0];
                        a.channel = args.Split(' ')[1];
                        string[] topicParts = args.Split(':');

                        for (int i = 1; i < topicParts.Length; i++)
                        {
                            a.topic += topicParts[i] + ":";
                        }
                        a.topic = a.topic.TrimEnd(':');

                        a.byUser = "";
                        a.date = -1;

                        foreach (IRCRoom ch in channels)
                        {
                            if (ch.Name == a.channel)
                            {
                                ch.topic = a.topic;
                                break;
                            }
                        }
                        

                       if (ShowTopicEvent != null)
                        {
                            ShowTopicEvent(this, a);
                        }
                }
                break;

                case Numerics.RPL_TOPICP2:
                    {
                        consoleLog += "\n" + args;

                        string[] splitArgs = args.Split(' ');
                        if (splitArgs.Length < 4)
                        {
                            DebugLogger.LogLine("ERROR - Numerics.RPL_TOPIC2 - Not enought args to process event: " + args);
                            break;
                        }

                        IRCTopicArgs a = new IRCTopicArgs();
                        a.channel = splitArgs[1];
                        a.topic = "";
                        a.byUser = splitArgs[2];

                        if (!long.TryParse(splitArgs[3], out a.date))
                        {
                            DebugLogger.LogLine("ERROR - Numerics.RPL_TOPIC2 - Couldn't parse the date the topic was set in channel: " + a.channel);
                            return;
                        }

                        if (ShowTopicEvent != null)
                        {
                            ShowTopicEvent(this, a);
                        }
                    }
                break;

                case Numerics.RPL_NAMREPLY:
                    {
                        consoleLog += "\n" + args;
                        IRCNamesArgs a = new IRCNamesArgs();
                        a.channel = args.Split(' ')[2];
                        string[] nicks = args.Split(':')[1].Split(' ');

                        
                        foreach (IRCRoom r in channels)
                        {
                            if (r.Name == a.channel)
                            {
                                r.nickList.Clear();
                                foreach (string n in nicks)
                                {
                                    IRCUser user = new IRCUser();
                                    user.nick = n;
                                    IRCUser.USERMODES mode = GetModeFromPrefix(n[0].ToString());
                                    if (mode != IRCUser.USERMODES.INVALID)
                                        user.modes.Add(mode);

                                    r.nickList.Add(user);
                                    a.names.Add(n);
                                }
                                break;
                            }
                        }

                        if (NamesEvent != null)
                        {
                            NamesEvent(this, a);
                        }
                }
                break;

                case Numerics.RPL_CHANNELMODEIS:
                {
                    consoleLog += "\n" + args;
                    IRCChannelModeArgs a = new IRCChannelModeArgs();
                    a.channel = args.Split(' ')[1];
                    a.modes = args.Split(' ')[2];
                    a.settingUser = "";

                    foreach (IRCRoom channel in Channels)
                    {
                        if (channel.Name == a.channel)
                        {
                            channel.Modes = a.modes.TrimStart('+');
                        }
                    }

                    if (ChannelModeEvent != null)
                    {
                        ChannelModeEvent(this, a);
                    }
                }
                break;

                case Numerics.RPL_CREATIONTIME:
                {
                     consoleLog += "\n" + args;

                        IRCChannelCreationArgs a = new IRCChannelCreationArgs();
                        a.channel = args.Split(' ')[1];

                        if (!long.TryParse(args.Split(' ')[2], out a.date))
                        {
                            DebugLogger.LogLine("Couldn't parse the date the channel was created on: #" + a.channel);
                            return;
                        }

                        if (ChannelCreationEvent != null)
                        {
                            ChannelCreationEvent(this, a);
                        }
                    }
                break;
                #endregion


                // DEFAULT
                default:
                {
                    consoleLog += "\n[UNHANDLED] " + args;

                    if (ConsoleMessageEvent != null)
                    {
                        IRCConsoleMsgArgs a = new IRCConsoleMsgArgs();
                        a.numeric = numeric;
                        a.text = args;
                        a.fullConsoleLog = consoleLog;

                        ConsoleMessageEvent(this, a);
                    }
                }
                break;
            }
        }

        #region HELPER METHODS

        string GetUserFromList(List<IRCUser> nicks, string match)
        {
            foreach (IRCUser n in nicks)
            {
                char[] tm = { '+', '%', '@', '&', '~' };
                if (n.nick.TrimStart(tm) == match)
                    return n.nick;
            }

            return match;
        }

        /// <summary>
        /// NO LONGER USED
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        string GetTrimmedChannel(string channel)
        {
            throw new Exception("GetTrimmedChannel method should not be used anymore!");

            // ORIGINAL CODE
            //string final = "";

            //if ('#' == channel[0])
            //{
            //    for (int i = 1; i < channel.Length; i++)
            //    {
            //        final += channel[i];
            //    }

            //}

            //return final;
        }

        IRCRoom FindRoom(string name)
        {
            foreach (IRCRoom room in channels)
            {
                if (room.Name == name)
                    return room;
            }

            return null;
        }

        IRCUser.USERMODES GetModeFromPrefix(string mode)
        {
            string[] prefixes = { "+", "%", "@", "&", "~" };
            mode = mode.ToLower();
            mode = mode.Trim();

            for (int i = 0; i < prefixes.Length; i++)
            {
                if (mode == prefixes[i])
                {
                    return (IRCUser.USERMODES)i;
                }
            }

            return IRCUser.USERMODES.INVALID;
        }

        string UpdateChannelModes(string modes, string mod)
        {
            // Add new modes
            if (mod[0] == '+')
            {
                for (int i = 1; i < mod.Length; i++)
                    modes += mod[i];
            }
            // Remove modes
            else
            {
                for (int i = 1; i < mod.Length; i++)
                    modes = modes.Remove(modes.IndexOf(mod[i]), 1);
            }

            return modes;
        }

        bool IsUserModePrefix(string token)
        {
            string[] prefixes = { "+", "%", "@", "&", "~" };
            foreach (string p in prefixes)
            {
                if (p == token)
                    return true;
            }

            return false;
        }

        #endregion
    }

    #region EVENT ARGS CLASSES
    public class IRCMessageArgs
    {
        public string channel;
        public string fromUser;
        public string text;
        public IRCRoom room;
        public bool isNotice;
    }

    public class IRCConsoleMsgArgs
    {
        public int numeric;
        public string text;
        public string fullConsoleLog;
    }

    public class IRCTopicArgs
    {
        public string channel;
        public string byUser;
        public string topic;
        public long date;
    }

    public class IRCNamesArgs
    {
        public string channel;
        public List<string> names = new List<string>();
    }

    public class IRCJoinArgs
    {
        public string channel;
        public string user;
    }

    public class IRCPartArgs
    {
        public string channel;
        public string user;
        public string partMsg;
    }

    public class IRCKickArgs
    {
        public string channel;
        public string kickingUser;
        public string kickedUser;
        public string kickMsg;
        public bool thisClient;
    }

    public class IRCUserModeArgs
    {
        public string channel;
        public string settingUser;
        public string modes;
        public string targetUser;
    }

    public class IRCChannelModeArgs
    {
        public string channel;
        public string settingUser;
        public string modes;
    }

    public class IRCChannelCreationArgs
    {
        public string channel;
        public long date;
    }

    #endregion

    #region NUMERIC SERVER MESSAGES
    /// <summary>
    /// These numeric server messages are implemented unless otherwise noted.
    /// </summary>
    public static class Numerics
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

        /// <summary>
        /// NOT IMPLEMENTED
        /// "<channel> <# visible> :<topic>"
        /// </summary>
        public const int RPL_LIST = 322;

        /// <summary>
        /// NOT IMPLEMENTED
        /// ":End of LIST"
        /// </summary>
        public const int RPL_LISTEND = 323;

        /// <summary>
        /// <channel> +<modes>
        /// </summary>
        public const int RPL_CHANNELMODEIS = 324;

        /// <summary>
        /// <channel> <date>
        /// </summary>
        public const int RPL_CREATIONTIME = 329;

        /// <summary>
        /// "<channel> :<topic>"
        /// </summary>
        public const int RPL_TOPIC = 332;

        /// <summary>
        /// "<channel> <user> <date>"
        /// </summary>
        public const int RPL_TOPICP2 = 333;

        /// <summary>
        /// "( "=" / "*" / "@" ) <channel>
        ///      :[ "@" / "+" ] <nick> *( " " [ "@" / "+" ] <nick> )
        /// - "@" is used for secret channels, "*" for private
        //   channels, and "=" for others(public channels).
        /// </summary>
        public const int RPL_NAMREPLY = 353;

        /// <summary>
        /// "<channel> :End of NAMES list"
        /// </summary>
        public const int RPL_ENDOFNAMES = 366;

    }
    #endregion
}
