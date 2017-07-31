/******************************************************************************
*	File		-	IRCNumerics.cs
*	Author		-	Joey Pollack
*	Date		-	7/24/2017 (m/d/y)
*	Mod Date	-	7/31/2017 (m/d/y)
*	Description	-	Definitions for IRC Numerics
*	                Info source from: https://github.com/ircdocs/irc-defs
*	                Some numerics have different values on different
*	                Networks. This is handled by storing an array of numeric
*	                values with a name in a static class instance. This instance
*	                can be directly compared to int values 
*	                ex. if (IRCNumerics.RPL_WELCOME == 1) 
******************************************************************************/
using System;

namespace IRCLib
{

    public class Numeric
    {
        public string Name;
        readonly int[] Values;

        public Numeric(string _name, params int[] _values)
        {
            Name = _name;
            Values = _values;
        }

        #region Operator Overloads

        public int this[int key]
        {
            get
            {
                return Values[key];
            }
            set
            {
                // NOTE: Do not change these values
                // SetValue(key, value);
                Console.Error.WriteLine("ERROR: Attempting to change IRCNumeric value.");
                Console.Error.Flush();
            }
        }



        public static bool operator ==(Numeric lhs, Numeric rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Numeric lhs, Numeric rhs)
        {
            return !lhs.Equals(rhs);
        }

        public static bool operator ==(Numeric lhs, int rhs)
        {
            foreach (int n in lhs.Values)
            {
                if (rhs == n)
                    return true;
            }
            return false;
        }

        public static bool operator !=(Numeric lhs, int rhs)
        {
            foreach (int n in lhs.Values)
            {
                if (rhs == n)
                    return false;
            }
            return true;
        }

        public static bool operator ==(int lhs, Numeric rhs)
        {
            foreach (int n in rhs.Values)
            {
                if (lhs == n)
                    return true;
            }
            return false;
        }

        public static bool operator !=(int lhs, Numeric rhs)
        {
            foreach (int n in rhs.Values)
            {
                if (lhs == n)
                    return false;
            }
            return true;
        }

        public override bool Equals(object rhs)
        {
            if (null == rhs)
                return false;

            Numeric nrhs = (Numeric)rhs;
            if (null == nrhs)
                return false;

            if (nrhs.Values.Length != Values.Length)
                return false;

            for (int i = 0; i < Values.Length; i++)
            {
                if (Values[i] != nrhs.Values[i])
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            // no idea if this is okay.
            return (Name.GetHashCode() + Values.GetHashCode()).GetHashCode();
        }

        #endregion
    }

    public static class IRCNumerics
    {


        /// <summary>
        /// <client> :Welcome to the Internet Relay Network <nick>!<user>@<host>
        /// 
        /// The first message sent after client registration. The text used varies widely
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_WELCOME = new Numeric("RPL_WELCOME", 001);

        /// <summary>
        /// <client> :Your host is <servername>, running version <version>
        /// 
        /// Part of the post-registration greeting. Text varies widely. Also known as RPL_YOURHOSTIS (InspIRCd)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_YOURHOST = new Numeric("RPL_YOURHOST", 002);

        /// <summary>
        /// <client> :This server was created <date>
        /// 
        /// Part of the post-registration greeting. Text varies widely and &lt;date&gt; is returned in a human-readable format. Also known as RPL_SERVERCREATED (InspIRCd)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_CREATED = new Numeric("RPL_CREATED", 003);

        /// <summary>
        /// <client> <server_name> <version> <usermodes> <chanmodes> [chanmodes_with_a_parameter]
        /// 
        /// Part of the post-registration greeting. Also known as RPL_SERVERVERSION (InspIRCd)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_MYINFO = new Numeric("RPL_MYINFO", 004);

        /// <summary>
        /// <client> :Try server <server_name>, port <port_number>
        /// <client> <hostname> <port> :<info>
        /// 
        /// Sent by the server to a user to suggest an alternative server, sometimes used when the connection is refused because the server is already full. Also known as RPL_SLINE (AustHex), and RPL_REDIR
        /// 
        /// Sent to the client to redirect it to another server. Also known as RPL_REDIR
        /// 
        /// 
        /// See also: RPL_BOUNCE (010)
        /// 
        /// </summary>
        public static readonly Numeric RPL_BOUNCE = new Numeric("RPL_BOUNCE", 005, 010);

        /// <summary>
        /// <client> <1-13 tokens> :are supported by this server
        /// 
        /// Advertises features, limits, and protocol options that clients should be aware of. Also known as RPL_PROTOCTL (Bahamut, Unreal, Ultimate)
        /// 
        /// http://modern.ircdocs.horse/#rplisupport-005
        /// 
        /// See also: RPL_REMOTEISUPPORT (105)
        /// 
        /// </summary>
        public static readonly Numeric RPL_ISUPPORT = new Numeric("RPL_ISUPPORT", 005);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_MAP = new Numeric("RPL_MAP", 006, 015, 357);

        /// <summary>
        /// Also known as RPL_ENDMAP (InspIRCd)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_MAPEND = new Numeric("RPL_MAPEND", 017, 007, 359);

        /// <summary>
        /// Server notice mask (hex). Also known as RPL_SNOMASKIS (InspIRCd)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_SNOMASK = new Numeric("RPL_SNOMASK", 008);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATMEMTOT = new Numeric("RPL_STATMEMTOT", 009);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATMEM = new Numeric("RPL_STATMEM", 010);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_YOURCOOKIE = new Numeric("RPL_YOURCOOKIE", 014);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_MAPMORE = new Numeric("RPL_MAPMORE", 358, 610, 016, 615, 623);

        /// <summary>
        /// <client> :<info>
        /// 
        /// Used by Rusnet to send the initial "Please wait while we process your connection" message, rather than a server-sent NOTICE.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_HELLO = new Numeric("RPL_HELLO", 020);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_APASSWARN_SET = new Numeric("RPL_APASSWARN_SET", 030);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_APASSWARN_SECRET = new Numeric("RPL_APASSWARN_SECRET", 031);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_APASSWARN_CLEAR = new Numeric("RPL_APASSWARN_CLEAR", 032);

        /// <summary>
        /// Also known as RPL_YOURUUID (InspIRCd)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_YOURID = new Numeric("RPL_YOURID", 042);

        /// <summary>
        /// <client> <newnick> :<info>
        /// 
        /// Sent to the client when their nickname was forced to change due to a collision
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_SAVENICK = new Numeric("RPL_SAVENICK", 043);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ATTEMPTINGJUNC = new Numeric("RPL_ATTEMPTINGJUNC", 050);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ATTEMPTINGREROUTE = new Numeric("RPL_ATTEMPTINGREROUTE", 051);

        /// <summary>
        /// Same format as RPL_ISUPPORT, but returned when the client is requesting information from a remote server instead of the server it is currently connected to
        /// 
        /// http://www.irc.org/tech_docs/005.html
        /// 
        /// See also: RPL_ISUPPORT (005)
        /// 
        /// </summary>
        public static readonly Numeric RPL_REMOTEISUPPORT = new Numeric("RPL_REMOTEISUPPORT", 105);

        /// <summary>
        /// <client> Link <version>[.<debug_level>] <destination> <next_server> [V<protocol_version> <link_uptime_in_seconds> <backstream_sendq> <upstream_sendq>]
        /// 
        /// See RFC
        /// 
        /// </summary>
        public static readonly Numeric RPL_TRACELINK = new Numeric("RPL_TRACELINK", 200);

        /// <summary>
        /// <client> Try. <class> <server>
        /// 
        /// See RFC
        /// 
        /// </summary>
        public static readonly Numeric RPL_TRACECONNECTING = new Numeric("RPL_TRACECONNECTING", 201);

        /// <summary>
        /// <client> H.S. <class> <server>
        /// 
        /// See RFC
        /// 
        /// </summary>
        public static readonly Numeric RPL_TRACEHANDSHAKE = new Numeric("RPL_TRACEHANDSHAKE", 202);

        /// <summary>
        /// <client> ???? <class> [<connection_address>]
        /// 
        /// See RFC
        /// 
        /// </summary>
        public static readonly Numeric RPL_TRACEUNKNOWN = new Numeric("RPL_TRACEUNKNOWN", 203);

        /// <summary>
        /// <client> Oper <class> <nick>
        /// 
        /// See RFC
        /// 
        /// </summary>
        public static readonly Numeric RPL_TRACEOPERATOR = new Numeric("RPL_TRACEOPERATOR", 204);

        /// <summary>
        /// <client> User <class> <nick>
        /// 
        /// See RFC
        /// 
        /// </summary>
        public static readonly Numeric RPL_TRACEUSER = new Numeric("RPL_TRACEUSER", 205);

        /// <summary>
        /// <client> Serv <class> <int>S <int>C <server> <nick!user|*!*>@<host|server> [V<protocol_version>]
        /// 
        /// See RFC
        /// 
        /// </summary>
        public static readonly Numeric RPL_TRACESERVER = new Numeric("RPL_TRACESERVER", 206);

        /// <summary>
        /// <client> Service <class> <name> <type> <active_type>
        /// 
        /// See RFC
        /// 
        /// </summary>
        public static readonly Numeric RPL_TRACESERVICE = new Numeric("RPL_TRACESERVICE", 207);

        /// <summary>
        /// <client> <newtype> 0 <client_name>
        /// 
        /// See RFC
        /// 
        /// </summary>
        public static readonly Numeric RPL_TRACENEWTYPE = new Numeric("RPL_TRACENEWTYPE", 208);

        /// <summary>
        /// <client> Class <class> <count>
        /// 
        /// See RFC
        /// 
        /// </summary>
        public static readonly Numeric RPL_TRACECLASS = new Numeric("RPL_TRACECLASS", 209);

        /// <summary>
        /// Used instead of having multiple stats numerics
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATS = new Numeric("RPL_STATS", 210);

        /// <summary>
        /// Used to send lists of stats flags and other help information.
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSHELP = new Numeric("RPL_STATSHELP", 210);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_TRACERECONNECT = new Numeric("RPL_TRACERECONNECT", 210);

        /// <summary>
        /// <client> <linkname> <sendq> <sent_msgs> <sent_bytes> <recvd_msgs> <rcvd_bytes> <time_open>
        /// 
        /// Reply to STATS (See RFC)
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSLINKINFO = new Numeric("RPL_STATSLINKINFO", 211);

        /// <summary>
        /// <client> <command> <count> [<byte_count> <remote_count>]
        /// 
        /// Reply to STATS (See RFC)
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSCOMMANDS = new Numeric("RPL_STATSCOMMANDS", 212);

        /// <summary>
        /// <client> C <host> * <name> <port> <class>
        /// 
        /// Reply to STATS (See RFC)
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSCLINE = new Numeric("RPL_STATSCLINE", 213);

        /// <summary>
        /// <client> N <host> * <name> <port> <class>
        /// 
        /// Reply to STATS (See RFC), Also known as RPL_STATSOLDNLINE (ircu, Unreal)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSNLINE = new Numeric("RPL_STATSNLINE", 226, 214);

        /// <summary>
        /// <client> I <host> * <host> <port> <class>
        /// 
        /// Reply to STATS (See RFC)
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSILINE = new Numeric("RPL_STATSILINE", 215);

        /// <summary>
        /// <client> K <host> * <username> <port> <class>
        /// 
        /// Reply to STATS (See RFC)
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSKLINE = new Numeric("RPL_STATSKLINE", 216);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSPLINE = new Numeric("RPL_STATSPLINE", 217, 220);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSQLINE = new Numeric("RPL_STATSQLINE", 217, 228);

        /// <summary>
        /// <client> Y <class> <ping_freq> <connect_freq> <max_sendq>
        /// 
        /// Reply to STATS (See RFC)
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSYLINE = new Numeric("RPL_STATSYLINE", 218);

        /// <summary>
        /// <client> <query> :<info>
        /// 
        /// End of RPL_STATS* list.
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFSTATS = new Numeric("RPL_ENDOFSTATS", 219);

        /// <summary>
        /// Returns details about active DNS blacklists and hits.
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSBLINE = new Numeric("RPL_STATSBLINE", 247, 227, 222, 220);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSWLINE = new Numeric("RPL_STATSWLINE", 220);

        /// <summary>
        /// <client> <user_modes> [<user_mode_params>]
        /// 
        /// Information about a user's own modes. Some daemons have extended the mode command and certain modes take parameters (like channel modes).
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_UMODEIS = new Numeric("RPL_UMODEIS", 221);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_SQLINE_NICK = new Numeric("RPL_SQLINE_NICK", 222);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSJLINE = new Numeric("RPL_STATSJLINE", 222);

        /// <summary>
        /// <?> 0x<?> <?> <?>
        /// 
        /// Output from the MODLIST command
        /// 
        /// </summary>
        public static readonly Numeric RPL_MODLIST = new Numeric("RPL_MODLIST", 222, 702);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CODEPAGE = new Numeric("RPL_CODEPAGE", 222);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSGLINE = new Numeric("RPL_STATSGLINE", 223, 227, 247);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHARSET = new Numeric("RPL_CHARSET", 223);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSELINE = new Numeric("RPL_STATSELINE", 223, 225);

        /// <summary>
        /// Feature lines?
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSFLINE = new Numeric("RPL_STATSFLINE", 238, 224);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSTLINE = new Numeric("RPL_STATSTLINE", 224, 246, 245);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSDLINE = new Numeric("RPL_STATSDLINE", 225, 275, 250);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSZLINE = new Numeric("RPL_STATSZLINE", 225);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSCLONE = new Numeric("RPL_STATSCLONE", 225);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSCOUNT = new Numeric("RPL_STATSCOUNT", 226);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSALINE = new Numeric("RPL_STATSALINE", 226);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSVLINE = new Numeric("RPL_STATSVLINE", 227, 240);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSBANVER = new Numeric("RPL_STATSBANVER", 228);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSSPAMF = new Numeric("RPL_STATSSPAMF", 229);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSEXCEPTTKL = new Numeric("RPL_STATSEXCEPTTKL", 230);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_SERVICEINFO = new Numeric("RPL_SERVICEINFO", 231);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_RULES = new Numeric("RPL_RULES", 232, 621);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDOFSERVICES = new Numeric("RPL_ENDOFSERVICES", 232);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_SERVICE = new Numeric("RPL_SERVICE", 233);

        /// <summary>
        /// <client> <name> <server> <mask> <type> <hopcount> <info>
        /// 
        /// A service entry in the service list
        /// 
        /// </summary>
        public static readonly Numeric RPL_SERVLIST = new Numeric("RPL_SERVLIST", 234);

        /// <summary>
        /// <client> <mask> <type> :<info>
        /// 
        /// Termination of an RPL_SERVLIST list
        /// 
        /// </summary>
        public static readonly Numeric RPL_SERVLISTEND = new Numeric("RPL_SERVLISTEND", 235);

        /// <summary>
        /// Verbose server list?
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSVERBOSE = new Numeric("RPL_STATSVERBOSE", 236);

        /// <summary>
        /// Engine name?
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSENGINE = new Numeric("RPL_STATSENGINE", 237);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSIAUTH = new Numeric("RPL_STATSIAUTH", 239);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSXLINE = new Numeric("RPL_STATSXLINE", 247, 240);

        /// <summary>
        /// <client> L <hostmask> * <servername> <maxdepth>
        /// 
        /// Reply to STATS (See RFC)
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSLLINE = new Numeric("RPL_STATSLLINE", 241);

        /// <summary>
        /// <client> :Server Up <days> days <hours>:<minutes>:<seconds>
        /// 
        /// Reply to STATS (See RFC)
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSUPTIME = new Numeric("RPL_STATSUPTIME", 242);

        /// <summary>
        /// <client> O <hostmask> * <nick> [:<info>]
        /// 
        /// Reply to STATS (See RFC); The info field is an extension found in some IRC daemons, which returns info such as an e-mail address or the name/job of an operator
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSOLINE = new Numeric("RPL_STATSOLINE", 243);

        /// <summary>
        /// <client> H <hostmask> * <servername>
        /// 
        /// Reply to STATS (See RFC)
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSHLINE = new Numeric("RPL_STATSHLINE", 244);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSSLINE = new Numeric("RPL_STATSSLINE", 245);

        /// <summary>
        /// Extension to RFC1459?
        /// 
        /// </summary>
        public static readonly Numeric RPL_STATSULINE = new Numeric("RPL_STATSULINE", 249, 246, 248);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSSERVICE = new Numeric("RPL_STATSSERVICE", 246);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSPING = new Numeric("RPL_STATSPING", 246);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSDEFINE = new Numeric("RPL_STATSDEFINE", 248);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSDEBUG = new Numeric("RPL_STATSDEBUG", 249);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSCONN = new Numeric("RPL_STATSCONN", 250);

        /// <summary>
        /// <client> :There are <int> users and <int> invisible on <int> servers
        /// 
        /// Reply to LUSERS command, other versions exist (eg. RFC2812); Text may vary.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_LUSERCLIENT = new Numeric("RPL_LUSERCLIENT", 251);

        /// <summary>
        /// <client> <int> :operator(s) online
        /// 
        /// Reply to LUSERS command - Number of IRC operators online
        /// 
        /// </summary>
        public static readonly Numeric RPL_LUSEROP = new Numeric("RPL_LUSEROP", 252);

        /// <summary>
        /// <client> <int> :unknown connection(s)
        /// 
        /// Reply to LUSERS command - Number of connections in an unknown/unregistered state
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_LUSERUNKNOWN = new Numeric("RPL_LUSERUNKNOWN", 253);

        /// <summary>
        /// <client> <int> :channels formed
        /// 
        /// Reply to LUSERS command - Number of channels formed
        /// 
        /// </summary>
        public static readonly Numeric RPL_LUSERCHANNELS = new Numeric("RPL_LUSERCHANNELS", 254);

        /// <summary>
        /// <client> :I have <int> clients and <int> servers
        /// 
        /// Reply to LUSERS command - Information about local connections; Text may vary.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_LUSERME = new Numeric("RPL_LUSERME", 255);

        /// <summary>
        /// <client> <server> :Administrative info
        /// 
        /// Start of an RPL_ADMIN* reply. In practise, the server parameter is often never given, and instead the last param contains the text 'Administrative info about <server>'. Newer daemons seem to follow the RFC and output the server's hostname in the last parameter, but also output the server name in the text as per traditional daemons.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_ADMINME = new Numeric("RPL_ADMINME", 256);

        /// <summary>
        /// <client> :<admin_location>
        /// 
        /// Reply to ADMIN command (Location, first line)
        /// 
        /// </summary>
        public static readonly Numeric RPL_ADMINLOC1 = new Numeric("RPL_ADMINLOC1", 257);

        /// <summary>
        /// <client> :<admin_location>
        /// 
        /// Reply to ADMIN command (Location, second line)
        /// 
        /// </summary>
        public static readonly Numeric RPL_ADMINLOC2 = new Numeric("RPL_ADMINLOC2", 258);

        /// <summary>
        /// <client> :<email_address>
        /// 
        /// Reply to ADMIN command (E-mail address of administrator)
        /// 
        /// </summary>
        public static readonly Numeric RPL_ADMINEMAIL = new Numeric("RPL_ADMINEMAIL", 259);

        /// <summary>
        /// <client> File <logfile> <debug_level>
        /// 
        /// See RFC
        /// 
        /// </summary>
        public static readonly Numeric RPL_TRACELOG = new Numeric("RPL_TRACELOG", 261);

        /// <summary>
        /// Extension to RFC1459?
        /// 
        /// </summary>
        public static readonly Numeric RPL_TRACEPING = new Numeric("RPL_TRACEPING", 262);

        /// <summary>
        /// <client> <server_name> <version>[.<debug_level>] :<info>
        /// 
        /// Used to terminate a list of RPL_TRACE* replies. Also known as RPL_ENDOFTRACE
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_TRACEEND = new Numeric("RPL_TRACEEND", 262);

        /// <summary>
        /// <client> <command> :Please wait a while and try again.
        /// 
        /// When a server drops a command without processing it, it MUST use this reply. The last param text changes, and commonly provides the client with more information about why the command could not be processed (such as rate-limiting). Also known as RPL_LOAD_THROTTLED and RPL_LOAD2HI, I'm presuming they do the same thing.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_TRYAGAIN = new Numeric("RPL_TRYAGAIN", 263);

        /// <summary>
        /// <client> <nick> :is using a secure connection (SSL)
        /// 
        /// </summary>
        public static readonly Numeric RPL_USINGSSL = new Numeric("RPL_USINGSSL", 264, 275);

        /// <summary>
        /// <client> [<u> <m>] :Current local users <u>, max <m>
        /// 
        /// Returns the number of clients currently and the maximum number of clients that have been connected directly to this server at one time, respectively. The two optional parameters are not always provided. Also known as RPL_CURRENT_LOCAL
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_LOCALUSERS = new Numeric("RPL_LOCALUSERS", 265);

        /// <summary>
        /// <client> [<u> <m>] :Current global users <u>, max <m>
        /// 
        /// Returns the number of clients currently connected to the network, and the maximum number of clients ever connected to the network at one time, respectively. Also known as RPL_CURRENT_GLOBAL
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_GLOBALUSERS = new Numeric("RPL_GLOBALUSERS", 266);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_START_NETSTAT = new Numeric("RPL_START_NETSTAT", 267);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_NETSTAT = new Numeric("RPL_NETSTAT", 268);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_END_NETSTAT = new Numeric("RPL_END_NETSTAT", 269);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_PRIVS = new Numeric("RPL_PRIVS", 270);

        /// <summary>
        /// <client> :<count> servers and <count> users, average <average count> users per server
        /// 
        /// </summary>
        public static readonly Numeric RPL_MAPUSERS = new Numeric("RPL_MAPUSERS", 270);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_SILELIST = new Numeric("RPL_SILELIST", 271);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDOFSILELIST = new Numeric("RPL_ENDOFSILELIST", 272);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_NOTIFY = new Numeric("RPL_NOTIFY", 273);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDNOTIFY = new Numeric("RPL_ENDNOTIFY", 274);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSDELTA = new Numeric("RPL_STATSDELTA", 274);

        /// <summary>
        /// <nick> :has client certificate fingerprint <fingerprint>
        /// 
        /// Shows the SSL/TLS certificate fingerprint used by the client with the given nickname. Only sent when users <code>WHOIS</code> themselves or when an operator sends the <code>WHOIS</code>. Also adopted by hybrid 8.1 and charybdis 3.2
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOISCERTFP = new Numeric("RPL_WHOISCERTFP", 276);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_STATSRLINE = new Numeric("RPL_STATSRLINE", 276);

        /// <summary>
        /// Gone from hybrid 7.1 (2003)
        /// 
        /// </summary>
        public static readonly Numeric RPL_VCHANEXIST = new Numeric("RPL_VCHANEXIST", 276);

        /// <summary>
        /// Gone from hybrid 7.1 (2003)
        /// 
        /// </summary>
        public static readonly Numeric RPL_VCHANLIST = new Numeric("RPL_VCHANLIST", 277);

        /// <summary>
        /// Gone from hybrid 7.1 (2003)
        /// 
        /// </summary>
        public static readonly Numeric RPL_VCHANHELP = new Numeric("RPL_VCHANHELP", 278);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_GLIST = new Numeric("RPL_GLIST", 280);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ACCEPTLIST = new Numeric("RPL_ACCEPTLIST", 281);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDOFGLIST = new Numeric("RPL_ENDOFGLIST", 281);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_JUPELIST = new Numeric("RPL_JUPELIST", 282);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDOFACCEPT = new Numeric("RPL_ENDOFACCEPT", 282);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ALIST = new Numeric("RPL_ALIST", 388, 283);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDOFJUPELIST = new Numeric("RPL_ENDOFJUPELIST", 283);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDOFALIST = new Numeric("RPL_ENDOFALIST", 389, 284);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_FEATURE = new Numeric("RPL_FEATURE", 284);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANINFO_HANDLE = new Numeric("RPL_CHANINFO_HANDLE", 285);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_GLIST_HASH = new Numeric("RPL_GLIST_HASH", 285);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_NEWHOSTIS = new Numeric("RPL_NEWHOSTIS", 285);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANINFO_USERS = new Numeric("RPL_CHANINFO_USERS", 286);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHKHEAD = new Numeric("RPL_CHKHEAD", 286);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANUSER = new Numeric("RPL_CHANUSER", 287);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANINFO_CHOPS = new Numeric("RPL_CHANINFO_CHOPS", 287);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_PATCHHEAD = new Numeric("RPL_PATCHHEAD", 288);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANINFO_VOICES = new Numeric("RPL_CHANINFO_VOICES", 288);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_PATCHCON = new Numeric("RPL_PATCHCON", 289);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANINFO_AWAY = new Numeric("RPL_CHANINFO_AWAY", 289);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANINFO_OPERS = new Numeric("RPL_CHANINFO_OPERS", 290);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_HELPHDR = new Numeric("RPL_HELPHDR", 290);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_DATASTR = new Numeric("RPL_DATASTR", 290);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDOFCHECK = new Numeric("RPL_ENDOFCHECK", 291);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANINFO_BANNED = new Numeric("RPL_CHANINFO_BANNED", 291);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_HELPOP = new Numeric("RPL_HELPOP", 291);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_SEARCHNOMATCH = new Numeric("ERR_SEARCHNOMATCH", 292);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_HELPTLR = new Numeric("RPL_HELPTLR", 292);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANINFO_BANS = new Numeric("RPL_CHANINFO_BANS", 292);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_HELPHLP = new Numeric("RPL_HELPHLP", 293);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANINFO_INVITE = new Numeric("RPL_CHANINFO_INVITE", 293);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_HELPFWD = new Numeric("RPL_HELPFWD", 294);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANINFO_INVITES = new Numeric("RPL_CHANINFO_INVITES", 294);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_HELPIGN = new Numeric("RPL_HELPIGN", 295);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANINFO_KICK = new Numeric("RPL_CHANINFO_KICK", 295);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANINFO_KICKS = new Numeric("RPL_CHANINFO_KICKS", 296);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_END_CHANINFO = new Numeric("RPL_END_CHANINFO", 299);

        /// <summary>
        /// Dummy reply, supposedly only used for debugging/testing new features, however has appeared in production daemons.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_NONE = new Numeric("RPL_NONE", 300);

        /// <summary>
        /// <client> <nick> :<message>
        /// 
        /// Used in reply to a command directed at a user who is marked as away
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_AWAY = new Numeric("RPL_AWAY", 301);

        /// <summary>
        /// <client> :*1<reply> *( ' ' <reply> )
        /// 
        /// Reply used by USERHOST (see RFC)
        /// 
        /// </summary>
        public static readonly Numeric RPL_USERHOST = new Numeric("RPL_USERHOST", 302);

        /// <summary>
        /// <client> :*1<nick> *( ' ' <nick> )
        /// 
        /// Reply to the ISON command (see RFC)
        /// 
        /// </summary>
        public static readonly Numeric RPL_ISON = new Numeric("RPL_ISON", 303);

        /// <summary>
        /// Defined with the comment <code>// insp-specific</code>
        /// 
        /// </summary>
        public static readonly Numeric RPL_SYNTAX = new Numeric("RPL_SYNTAX", 304);

        /// <summary>
        /// <client> :<text>
        /// 
        /// Displays text to the user. This seems to have been defined in irc2.7h but never used. Servers generally use specific numerics or server notices instead of this. Unreal uses this numeric, but most others don't use it
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_TEXT = new Numeric("RPL_TEXT", 304);

        /// <summary>
        /// <client> :<info>
        /// 
        /// Reply from AWAY when no longer marked as away
        /// 
        /// </summary>
        public static readonly Numeric RPL_UNAWAY = new Numeric("RPL_UNAWAY", 305);

        /// <summary>
        /// <client> :<info>
        /// 
        /// Reply from AWAY when marked away
        /// 
        /// </summary>
        public static readonly Numeric RPL_NOWAWAY = new Numeric("RPL_NOWAWAY", 306);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_SUSERHOST = new Numeric("RPL_SUSERHOST", 307);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISREGNICK = new Numeric("RPL_WHOISREGNICK", 307);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_USERIP = new Numeric("RPL_USERIP", 307, 340);

        /// <summary>
        /// Also known as RPL_RULESTART (InspIRCd)
        /// 
        /// </summary>
        public static readonly Numeric RPL_RULESSTART = new Numeric("RPL_RULESSTART", 620, 308);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISADMIN = new Numeric("RPL_WHOISADMIN", 308);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_NOTIFYACTION = new Numeric("RPL_NOTIFYACTION", 308);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_NICKTRACE = new Numeric("RPL_NICKTRACE", 309);

        /// <summary>
        /// Also known as RPL_RULESEND (InspIRCd)
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFRULES = new Numeric("RPL_ENDOFRULES", 309, 622);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISHELPER = new Numeric("RPL_WHOISHELPER", 309);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISSADMIN = new Numeric("RPL_WHOISSADMIN", 309);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISHELPOP = new Numeric("RPL_WHOISHELPOP", 310);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISSERVICE = new Numeric("RPL_WHOISSERVICE", 310);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISSVCMSG = new Numeric("RPL_WHOISSVCMSG", 310);

        /// <summary>
        /// <client> <nick> <user> <host> * :<real_name>
        /// 
        /// Reply to WHOIS - Information about the user
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOISUSER = new Numeric("RPL_WHOISUSER", 311);

        /// <summary>
        /// <client> <nick> <server> :<server_info>
        /// 
        /// Reply to WHOIS - What server they're on
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOISSERVER = new Numeric("RPL_WHOISSERVER", 312);

        /// <summary>
        /// <client> <nick> :<privileges>
        /// 
        /// Reply to WHOIS - User has IRC Operator privileges
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOISOPERATOR = new Numeric("RPL_WHOISOPERATOR", 313);

        /// <summary>
        /// <client> <nick> <user> <host> * :<real_name>
        /// 
        /// Reply to WHOWAS - Information about the user
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOWASUSER = new Numeric("RPL_WHOWASUSER", 314);

        /// <summary>
        /// <client> <name> :<info>
        /// 
        /// Used to terminate a list of RPL_WHOREPLY replies
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFWHO = new Numeric("RPL_ENDOFWHO", 315);

        /// <summary>
        /// This numeric was reserved, but never actually used. The source code notes "redundant and not needed but reserved"
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOISCHANOP = new Numeric("RPL_WHOISCHANOP", 316);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISPRIVDEAF = new Numeric("RPL_WHOISPRIVDEAF", 316);

        /// <summary>
        /// <client> <nick> <seconds> :seconds idle
        /// 
        /// Reply to WHOIS - Idle information
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOISIDLE = new Numeric("RPL_WHOISIDLE", 317);

        /// <summary>
        /// <client> <nick> :<info>
        /// 
        /// Reply to WHOIS - End of list
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFWHOIS = new Numeric("RPL_ENDOFWHOIS", 318);

        /// <summary>
        /// <client> <nick> :*( ( '@' / '+' ) <channel> ' ' )
        /// 
        /// Reply to WHOIS - Channel list for user (See RFC)
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOISCHANNELS = new Numeric("RPL_WHOISCHANNELS", 319);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOIS_HIDDEN = new Numeric("RPL_WHOIS_HIDDEN", 320);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISVIRT = new Numeric("RPL_WHOISVIRT", 320);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISSPECIAL = new Numeric("RPL_WHOISSPECIAL", 320);

        /// <summary>
        /// <client> Channels :Users  Name
        /// 
        /// Channel list - Header
        /// 
        /// </summary>
        public static readonly Numeric RPL_LISTSTART = new Numeric("RPL_LISTSTART", 321);

        /// <summary>
        /// <client> <channel> <#_visible> :<topic>
        /// 
        /// Channel list - A channel
        /// 
        /// </summary>
        public static readonly Numeric RPL_LIST = new Numeric("RPL_LIST", 322);

        /// <summary>
        /// <client> :<info>
        /// 
        /// Channel list - End of list
        /// 
        /// </summary>
        public static readonly Numeric RPL_LISTEND = new Numeric("RPL_LISTEND", 323);

        /// <summary>
        /// <client> <channel> <mode> <mode_params>
        /// 
        /// </summary>
        public static readonly Numeric RPL_CHANNELMODEIS = new Numeric("RPL_CHANNELMODEIS", 324);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANNELPASSIS = new Numeric("RPL_CHANNELPASSIS", 325);

        /// <summary>
        /// <client> <channel> <nickname>
        /// 
        /// </summary>
        public static readonly Numeric RPL_UNIQOPIS = new Numeric("RPL_UNIQOPIS", 325);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISWEBIRC = new Numeric("RPL_WHOISWEBIRC", 325);

        /// <summary>
        /// <client> <nick> <channel> <modeletters> :is the current channel mode-lock
        /// 
        /// Defined in header file in charybdis, but never used. Also known as RPL_CHANNELMLOCK.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_CHANNELMLOCKIS = new Numeric("RPL_CHANNELMLOCKIS", 325);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_NOCHANPASS = new Numeric("RPL_NOCHANPASS", 326);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISHOST = new Numeric("RPL_WHOISHOST", 616, 378, 327);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHPASSUNKNOWN = new Numeric("RPL_CHPASSUNKNOWN", 327);

        /// <summary>
        /// Also known as RPL_CHANNELURL in charybdis
        /// 
        /// </summary>
        public static readonly Numeric RPL_CHANNEL_URL = new Numeric("RPL_CHANNEL_URL", 328);

        /// <summary>
        /// Also known as RPL_CHANNELCREATED (InspIRCd)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_CREATIONTIME = new Numeric("RPL_CREATIONTIME", 329);

        /// <summary>
        /// <client> <nick> <authname> :<info>
        /// 
        /// Also known as RPL_WHOISLOGGEDIN (ratbox?, charybdis)
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOISACCOUNT = new Numeric("RPL_WHOISACCOUNT", 330);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOWAS_TIME = new Numeric("RPL_WHOWAS_TIME", 330);

        /// <summary>
        /// <client> <channel> :<info>
        /// 
        /// Response to TOPIC when no topic is set. Also known as RPL_NOTOPICSET (InspIRCd)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_NOTOPIC = new Numeric("RPL_NOTOPIC", 331);

        /// <summary>
        /// <client> <channel> :<topic>
        /// 
        /// Response to TOPIC with the set topic. Also known as RPL_TOPICSET (InspIRCd)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_TOPIC = new Numeric("RPL_TOPIC", 332);

        /// <summary>
        /// Also known as RPL_TOPICTIME (InspIRCd).
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_TOPICWHOTIME = new Numeric("RPL_TOPICWHOTIME", 333);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_COMMANDSYNTAX = new Numeric("RPL_COMMANDSYNTAX", 334);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_LISTUSAGE = new Numeric("RPL_LISTUSAGE", 334);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_LISTSYNTAX = new Numeric("RPL_LISTSYNTAX", 334);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISACCOUNTONLY = new Numeric("RPL_WHOISACCOUNTONLY", 335);

        /// <summary>
        /// Since hybrid 8.2.0
        /// Before hybrid 8.2.0, for "User connected using a webirc gateway". Since charybdis 3.4.0 for "Underlying IPv4 is %s".
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOISTEXT = new Numeric("RPL_WHOISTEXT", 335, 337);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISBOT = new Numeric("RPL_WHOISBOT", 335, 617, 336);

        /// <summary>
        /// <client> :<channel>
        /// <client> <channel> <invitemask>
        /// 
        /// Since hybrid 8.2.0. Not to be confused with the more widely used 346.
        /// A "list of channels a client is invited to" sent with /INVITE
        /// 
        /// An invite mask for the invite mask list. Also known as RPL_INVEXLIST in hybrid 8.2.0
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_INVITELIST = new Numeric("RPL_INVITELIST", 346, 336);

        /// <summary>
        /// <client> :End of /INVITE list.
        /// <client> <channel> :<info>
        /// 
        /// Since hybrid 8.2.0. Not to be confused with the more widely used 347.
        /// 
        /// Termination of an RPL_INVITELIST list. Also known as RPL_ENDOFINVEXLIST in hybrid 8.2.0
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFINVITELIST = new Numeric("RPL_ENDOFINVITELIST", 347, 337);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CHANPASSOK = new Numeric("RPL_CHANPASSOK", 338);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISACTUALLY = new Numeric("RPL_WHOISACTUALLY", 338);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_BADCHANPASS = new Numeric("RPL_BADCHANPASS", 339);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISMARKS = new Numeric("RPL_WHOISMARKS", 339);

        /// <summary>
        /// <client> <nick> <channel>
        /// 
        /// Returned by the server to indicate that the attempted INVITE message was successful and is being passed onto the end client. Note that RFC1459 documents the parameters in the reverse order. The format given here is the format used on production servers, and should be considered the standard reply above that given by RFC1459.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_INVITING = new Numeric("RPL_INVITING", 341);

        /// <summary>
        /// <client> <user> :<info>
        /// 
        /// Returned by a server answering a SUMMON message to indicate that it is summoning that user
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_SUMMONING = new Numeric("RPL_SUMMONING", 342);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISKILL = new Numeric("RPL_WHOISKILL", 343);

        /// <summary>
        /// <client> <channel> <user being invited> <user issuing invite> :<user being invited> has been invited by <user issuing invite>
        /// 
        /// Sent to users on a channel when an INVITE command has been issued. Also known as RPL_ISSUEDINVITE (ircu)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_INVITED = new Numeric("RPL_INVITED", 345);

        /// <summary>
        /// <client> <channel> <exceptionmask> [<who> <set-ts>]
        /// 
        /// An exception mask for the exception mask list. Also known as RPL_EXLIST (Unreal, Ultimate). Bahamut calls this RPL_EXEMPTLIST and adds the last two optional params, <who> being either the nickmask of the client that set the exception or the server name, and <set-ts> being a unix timestamp representing when it was set.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_EXCEPTLIST = new Numeric("RPL_EXCEPTLIST", 348);

        /// <summary>
        /// <client> <channel> :<info>
        /// 
        /// Termination of an RPL_EXCEPTLIST list. Also known as RPL_ENDOFEXLIST (Unreal, Ultimate) or RPL_ENDOFEXEMPTLIST (Bahamut).
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFEXCEPTLIST = new Numeric("RPL_ENDOFEXCEPTLIST", 349);

        /// <summary>
        /// <client> <version>[.<debuglevel>] <server> :<comments>
        /// 
        /// Reply by the server showing its version details, however this format is not often adhered to
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_VERSION = new Numeric("RPL_VERSION", 351);

        /// <summary>
        /// <client> <channel> <user> <host> <server> <nick> <H|G>[*][@|+] :<hopcount> <real_name>
        /// 
        /// Reply to vanilla WHO (See RFC). This format can be very different if the 'WHOX' version of the command is used (see ircu).
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOREPLY = new Numeric("RPL_WHOREPLY", 352);

        /// <summary>
        /// <client> ( '=' / '*' / '@' ) <channel> ' ' : [ '@' / '+' ] <nick> *( ' ' [ '@' / '+' ] <nick> )
        /// 
        /// Reply to NAMES (See RFC)
        /// 
        /// </summary>
        public static readonly Numeric RPL_NAMREPLY = new Numeric("RPL_NAMREPLY", 353);

        /// <summary>
        /// Reply to WHO, however it is a 'special' reply because it is returned using a non-standard (non-RFC1459) format. The format is dictated by the command given by the user, and can vary widely. When this is used, the WHO command was invoked in its 'extended' form, as announced by the 'WHOX' ISUPPORT tag. Also known as RPL_RWHOREPLY (Bahamut).
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOSPCRPL = new Numeric("RPL_WHOSPCRPL", 354);

        /// <summary>
        /// <client> ( '=' / '*' / '@' ) <channel> ' ' : [ '@' / '+' ] <nick> *( ' ' [ '@' / '+' ] <nick> )
        /// 
        /// Reply to the \users (when the channel is set +D, QuakeNet relative). The proper define name for this numeric is unknown at this time. Also known as RPL_DELNAMREPLY (ircu)
        /// 
        /// 
        /// See also: RPL_NAMREPLY (353)
        /// 
        /// </summary>
        public static readonly Numeric RPL_NAMREPLY_ = new Numeric("RPL_NAMREPLY_", 355);

        /// <summary>
        /// Defined in header file, but never used. Initially introduced in charybdis 2.1 behind <code>#if 0</code>, with the other side using RPL_WHOISACTUALLY
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOWASREAL = new Numeric("RPL_WHOWASREAL", 360);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_KILLDONE = new Numeric("RPL_KILLDONE", 361);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CLOSING = new Numeric("RPL_CLOSING", 362);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_CLOSEEND = new Numeric("RPL_CLOSEEND", 363);

        /// <summary>
        /// <client> <mask> <server> :<hopcount> <server_info>
        /// 
        /// Reply to the LINKS command
        /// 
        /// </summary>
        public static readonly Numeric RPL_LINKS = new Numeric("RPL_LINKS", 364);

        /// <summary>
        /// <client> <mask> :<info>
        /// 
        /// Termination of an RPL_LINKS list
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFLINKS = new Numeric("RPL_ENDOFLINKS", 365);

        /// <summary>
        /// <client> <channel> :<info>
        /// 
        /// Termination of an RPL_NAMREPLY list
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFNAMES = new Numeric("RPL_ENDOFNAMES", 366);

        /// <summary>
        /// <client> <channel> <banid> [<time_left> :<reason>]
        /// 
        /// A ban-list item (See RFC); <time left> and <reason> are additions used by various servers.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_BANLIST = new Numeric("RPL_BANLIST", 367);

        /// <summary>
        /// <client> <channel> :<info>
        /// 
        /// Termination of an RPL_BANLIST list
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFBANLIST = new Numeric("RPL_ENDOFBANLIST", 368);

        /// <summary>
        /// <client> <nick> :<info>
        /// 
        /// Reply to WHOWAS - End of list
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFWHOWAS = new Numeric("RPL_ENDOFWHOWAS", 369);

        /// <summary>
        /// <client> :<string>
        /// 
        /// Reply to INFO
        /// 
        /// </summary>
        public static readonly Numeric RPL_INFO = new Numeric("RPL_INFO", 371);

        /// <summary>
        /// <client> :- <string>
        /// 
        /// Reply to MOTD
        /// Used by AustHex to 'force' the display of the MOTD, however is considered obsolete due to client/script awareness & ability to display the MOTD regardless.
        /// 
        /// 
        /// See also: RPL_MOTD (372)
        /// 
        /// </summary>
        public static readonly Numeric RPL_MOTD = new Numeric("RPL_MOTD", 378, 372);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_INFOSTART = new Numeric("RPL_INFOSTART", 373);

        /// <summary>
        /// <client> :<info>
        /// 
        /// Termination of an RPL_INFO list
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFINFO = new Numeric("RPL_ENDOFINFO", 374);

        /// <summary>
        /// <client> :- <server> Message of the day -
        /// 
        /// Start of an RPL_MOTD list
        /// 
        /// </summary>
        public static readonly Numeric RPL_MOTDSTART = new Numeric("RPL_MOTDSTART", 375);

        /// <summary>
        /// <client> :<info>
        /// 
        /// Termination of an RPL_MOTD list
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFMOTD = new Numeric("RPL_ENDOFMOTD", 376);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_KICKEXPIRED = new Numeric("RPL_KICKEXPIRED", 377);

        /// <summary>
        /// <client> :<text>
        /// 
        /// Used during the connection (after MOTD) to announce the network policy on spam and privacy. Supposedly now obsoleted in favour of using NOTICE.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_SPAM = new Numeric("RPL_SPAM", 377);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_BANEXPIRED = new Numeric("RPL_BANEXPIRED", 378);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOISMODES = new Numeric("RPL_WHOISMODES", 615, 379);

        /// <summary>
        /// <nick> :was connecting from <host>
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOWASIP = new Numeric("RPL_WHOWASIP", 379);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_KICKLINKED = new Numeric("RPL_KICKLINKED", 379);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_YOURHELPER = new Numeric("RPL_YOURHELPER", 380);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_BANLINKED = new Numeric("RPL_BANLINKED", 380);

        /// <summary>
        /// <client> :<info>
        /// 
        /// Successful reply from OPER. Also known asRPL_YOUAREOPER (InspIRCd)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_YOUREOPER = new Numeric("RPL_YOUREOPER", 381);

        /// <summary>
        /// <client> <config_file> :<info>
        /// 
        /// Successful reply from REHASH
        /// 
        /// </summary>
        public static readonly Numeric RPL_REHASHING = new Numeric("RPL_REHASHING", 382);

        /// <summary>
        /// <client> :You are service <service_name>
        /// 
        /// Sent upon successful registration of a service
        /// 
        /// </summary>
        public static readonly Numeric RPL_YOURESERVICE = new Numeric("RPL_YOURESERVICE", 383);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_MYPORTIS = new Numeric("RPL_MYPORTIS", 384);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_NOTOPERANYMORE = new Numeric("RPL_NOTOPERANYMORE", 385);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_IRCOPS = new Numeric("RPL_IRCOPS", 387, 386);

        /// <summary>
        /// :*
        /// 
        /// Used by Hybrid's old OpenSSL OPER CHALLENGE response. This has been obsoleted in favour of SSL cert fingerprinting in oper blocks
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_RSACHALLENGE = new Numeric("RPL_RSACHALLENGE", 386);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_QLIST = new Numeric("RPL_QLIST", 386);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_IRCOPSHEADER = new Numeric("RPL_IRCOPSHEADER", 386);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDOFQLIST = new Numeric("RPL_ENDOFQLIST", 387);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDOFIRCOPS = new Numeric("RPL_ENDOFIRCOPS", 387, 388);

        /// <summary>
        /// <client> <server> :<time string>
        /// <client> <server> <timestamp> <offset> :<time string>
        /// <client> <server> <timezone name> <microseconds> :<time string>
        /// <client> <server> <year> <month> <day> <hour> <minute> <second>
        /// 
        /// Response to the TIME command. The string format may vary greatly.
        /// 
        /// This extention adds the timestamp and timestamp-offet information for clients.
        /// 
        /// Timezone name is acronym style (eg. 'EST', 'PST' etc). The microseconds field is the number of microseconds since the UNIX epoch, however it is relative to the local timezone of the server. The timezone field is ambiguous, since it only appears to include American zones.
        /// 
        /// Yet another variation, including the time broken down into its components. Time is supposedly relative to UTC.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_TIME = new Numeric("RPL_TIME", 391);

        /// <summary>
        /// <client> :UserID   Terminal  Host
        /// 
        /// Start of an RPL_USERS list
        /// 
        /// </summary>
        public static readonly Numeric RPL_USERSSTART = new Numeric("RPL_USERSSTART", 392);

        /// <summary>
        /// <client> :<username> <ttyline> <hostname>
        /// 
        /// Response to the USERS command (See RFC)
        /// 
        /// </summary>
        public static readonly Numeric RPL_USERS = new Numeric("RPL_USERS", 393);

        /// <summary>
        /// <client> :<info>
        /// 
        /// Termination of an RPL_USERS list
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFUSERS = new Numeric("RPL_ENDOFUSERS", 394);

        /// <summary>
        /// <client> :<info>
        /// 
        /// Reply to USERS when nobody is logged in
        /// 
        /// </summary>
        public static readonly Numeric RPL_NOUSERS = new Numeric("RPL_NOUSERS", 395);

        /// <summary>
        /// Reply to a user when user mode +x (host masking) was set successfully
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_HOSTHIDDEN = new Numeric("RPL_HOSTHIDDEN", 396);

        /// <summary>
        /// <client> <hostname> :is now your visible host
        /// 
        /// Also known as RPL_YOURDISPLAYEDHOST (InspIRCd)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_VISIBLEHOST = new Numeric("RPL_VISIBLEHOST", 396);

        /// <summary>
        /// <client> <command> [<?>] :<info>
        /// 
        /// Sent when an error occured executing a command, but it is not specifically known why the command could not be executed.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_UNKNOWNERROR = new Numeric("ERR_UNKNOWNERROR", 400);

        /// <summary>
        /// <client> <nick> :<reason>
        /// 
        /// Used to indicate the nickname parameter supplied to a command is currently unused
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOSUCHNICK = new Numeric("ERR_NOSUCHNICK", 401);

        /// <summary>
        /// <client> <server> :<reason>
        /// 
        /// Used to indicate the server name given currently doesn't exist
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOSUCHSERVER = new Numeric("ERR_NOSUCHSERVER", 402);

        /// <summary>
        /// <client> <channel> :<reason>
        /// 
        /// Used to indicate the given channel name is invalid, or does not exist
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOSUCHCHANNEL = new Numeric("ERR_NOSUCHCHANNEL", 403);

        /// <summary>
        /// <client> <channel> :<reason>
        /// 
        /// Sent to a user who does not have the rights to send a message to a channel
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_CANNOTSENDTOCHAN = new Numeric("ERR_CANNOTSENDTOCHAN", 404);

        /// <summary>
        /// <client> <channel> :<reason>
        /// 
        /// Sent to a user when they have joined the maximum number of allowed channels and they tried to join another channel
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_TOOMANYCHANNELS = new Numeric("ERR_TOOMANYCHANNELS", 405);

        /// <summary>
        /// <client> <nick> :<reason>
        /// 
        /// Returned by WHOWAS to indicate there was no history information for a given nickname
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_WASNOSUCHNICK = new Numeric("ERR_WASNOSUCHNICK", 406);

        /// <summary>
        /// <client> <target> :<reason>
        /// 
        /// The given target(s) for a command are ambiguous in that they relate to too many targets
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_TOOMANYTARGETS = new Numeric("ERR_TOOMANYTARGETS", 407);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOCOLORSONCHAN = new Numeric("ERR_NOCOLORSONCHAN", 408);

        /// <summary>
        /// <client> <channel> :You cannot use control codes on this channel. Not sent: <text>
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOCTRLSONCHAN = new Numeric("ERR_NOCTRLSONCHAN", 408);

        /// <summary>
        /// <client> <service_name> :<reason>
        /// 
        /// Returned to a client which is attempting to send an SQUERY (or other message) to a service which does not exist
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOSUCHSERVICE = new Numeric("ERR_NOSUCHSERVICE", 408);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// PING or PONG message missing the originator parameter which is required since these commands must work without valid prefixes
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOORIGIN = new Numeric("ERR_NOORIGIN", 409);

        /// <summary>
        /// <client> <badcmd> :Invalid CAP subcommand
        /// 
        /// Returned when a client sends a CAP subcommand which is invalid or otherwise issues an invalid CAP command. Also known as ERR_INVALIDCAPSUBCOMMAND (InspIRCd) or ERR_UNKNOWNCAPCMD (ircu)
        /// 
        /// http://ircv3.net/specs/core/capability-negotiation-3.1.html
        /// 
        /// </summary>
        public static readonly Numeric ERR_INVALIDCAPCMD = new Numeric("ERR_INVALIDCAPCMD", 410);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Returned when no recipient is given with a command
        /// 
        /// </summary>
        public static readonly Numeric ERR_NORECIPIENT = new Numeric("ERR_NORECIPIENT", 411);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Returned when NOTICE/PRIVMSG is used with no message given
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOTEXTTOSEND = new Numeric("ERR_NOTEXTTOSEND", 412);

        /// <summary>
        /// <client> <mask> :<reason>
        /// 
        /// Used when a message is being sent to a mask without being limited to a top-level domain (i.e. * instead of *.au)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOTOPLEVEL = new Numeric("ERR_NOTOPLEVEL", 413);

        /// <summary>
        /// <client> <mask> :<reason>
        /// 
        /// Used when a message is being sent to a mask with a wild-card for a top level domain (i.e. *.*)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_WILDTOPLEVEL = new Numeric("ERR_WILDTOPLEVEL", 414);

        /// <summary>
        /// <client> <mask> :<reason>
        /// 
        /// Used when a message is being sent to a mask with an invalid syntax
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_BADMASK = new Numeric("ERR_BADMASK", 415);

        /// <summary>
        /// <client> <command> [<mask>] :<info>
        /// 
        /// Returned when too many matches have been found for a command and the output has been truncated. An example would be the WHO command, where by the mask '*' would match everyone on the network! Ouch!
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_TOOMANYMATCHES = new Numeric("ERR_TOOMANYMATCHES", 416);

        /// <summary>
        /// Same as ERR_TOOMANYMATCHES
        /// 
        /// </summary>
        public static readonly Numeric ERR_QUERYTOOLONG = new Numeric("ERR_QUERYTOOLONG", 416);

        /// <summary>
        /// Returned when an input line is longer than the server can process (512 bytes), to let the client know this line was dropped (rather than being truncated)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_INPUTTOOLONG = new Numeric("ERR_INPUTTOOLONG", 417);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_LENGTHTRUNCATED = new Numeric("ERR_LENGTHTRUNCATED", 419);

        /// <summary>
        /// <client> <command> :<reason>
        /// 
        /// Returned when the given command is unknown to the server (or hidden because of lack of access rights)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_UNKNOWNCOMMAND = new Numeric("ERR_UNKNOWNCOMMAND", 421);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Sent when there is no MOTD to send the client
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOMOTD = new Numeric("ERR_NOMOTD", 422);

        /// <summary>
        /// <client> <server> :<reason>
        /// 
        /// Returned by a server in response to an ADMIN request when no information is available. RFC1459 mentions this in the list of numerics. While it's not listed as a valid reply in section 4.3.7 ('Admin command'), it's confirmed to exist in the real world.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOADMININFO = new Numeric("ERR_NOADMININFO", 423);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Generic error message used to report a failed file operation during the processing of a command
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_FILEERROR = new Numeric("ERR_FILEERROR", 424);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOOPERMOTD = new Numeric("ERR_NOOPERMOTD", 425);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_TOOMANYAWAY = new Numeric("ERR_TOOMANYAWAY", 429);

        /// <summary>
        /// Returned by NICK when the user is not allowed to change their nickname due to a channel event (channel mode +E)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_EVENTNICKCHANGE = new Numeric("ERR_EVENTNICKCHANGE", 430);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Returned when a nickname parameter expected for a command isn't found
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NONICKNAMEGIVEN = new Numeric("ERR_NONICKNAMEGIVEN", 431);

        /// <summary>
        /// <client> <nick> :<reason>
        /// 
        /// Returned after receiving a NICK message which contains a nickname which is considered invalid, such as it's reserved ('anonymous') or contains characters considered invalid for nicknames. This numeric is misspelt, but remains with this name for historical reasons :)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_ERRONEUSNICKNAME = new Numeric("ERR_ERRONEUSNICKNAME", 432);

        /// <summary>
        /// <client> <nick> :<reason>
        /// 
        /// Returned by the NICK command when the given nickname is already in use
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NICKNAMEINUSE = new Numeric("ERR_NICKNAMEINUSE", 433);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_SERVICENAMEINUSE = new Numeric("ERR_SERVICENAMEINUSE", 434);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NORULES = new Numeric("ERR_NORULES", 434);

        /// <summary>
        /// Also known as ERR_BANNICKCHANGE (ratbox, charybdis)
        /// 
        /// </summary>
        public static readonly Numeric ERR_BANONCHAN = new Numeric("ERR_BANONCHAN", 435);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_SERVICECONFUSED = new Numeric("ERR_SERVICECONFUSED", 435);

        /// <summary>
        /// <nick> :<reason>
        /// 
        /// Returned by a server to a client when it detects a nickname collision
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NICKCOLLISION = new Numeric("ERR_NICKCOLLISION", 436);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_BANNICKCHANGE = new Numeric("ERR_BANNICKCHANGE", 437);

        /// <summary>
        /// <client> <nick/channel/service> :<reason>
        /// 
        /// Return when the target is unable to be reached temporarily, eg. a delay mechanism in play, or a service being offline
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_UNAVAILRESOURCE = new Numeric("ERR_UNAVAILRESOURCE", 437);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_DEAD = new Numeric("ERR_DEAD", 438);

        /// <summary>
        /// Also known as ERR_NCHANGETOOFAST (Unreal, Ultimate)
        /// 
        /// </summary>
        public static readonly Numeric ERR_NICKTOOFAST = new Numeric("ERR_NICKTOOFAST", 438);

        /// <summary>
        /// Also known as many other things, RPL_INVTOOFAST, RPL_MSGTOOFAST, ERR_TARGETTOFAST (Bahamut), etc
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_TARGETTOOFAST = new Numeric("ERR_TARGETTOOFAST", 439);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_SERVICESDOWN = new Numeric("ERR_SERVICESDOWN", 440);

        /// <summary>
        /// <client> <nick> <channel> :<reason>
        /// 
        /// Returned by the server to indicate that the target user of the command is not on the given channel
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_USERNOTINCHANNEL = new Numeric("ERR_USERNOTINCHANNEL", 441);

        /// <summary>
        /// <client> <channel> :<reason>
        /// 
        /// Returned by the server whenever a client tries to perform a channel effecting command for which the client is not a member
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOTONCHANNEL = new Numeric("ERR_NOTONCHANNEL", 442);

        /// <summary>
        /// <client> <nick> <channel> [:<reason>]
        /// 
        /// Returned when a client tries to invite a user to a channel they're already on
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_USERONCHANNEL = new Numeric("ERR_USERONCHANNEL", 443);

        /// <summary>
        /// <client> <user> :<reason>
        /// 
        /// Returned by the SUMMON command if a given user was not logged in and could not be summoned
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOLOGIN = new Numeric("ERR_NOLOGIN", 444);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Returned by SUMMON when it has been disabled or not implemented
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_SUMMONDISABLED = new Numeric("ERR_SUMMONDISABLED", 445);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Returned by USERS when it has been disabled or not implemented
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_USERSDISABLED = new Numeric("ERR_USERSDISABLED", 446);

        /// <summary>
        /// This numeric is called ERR_CANTCHANGENICK in InspIRCd
        /// 
        /// </summary>
        public static readonly Numeric ERR_NONICKCHANGE = new Numeric("ERR_NONICKCHANGE", 447);

        /// <summary>
        /// <channel> :Channel is forbidden: <reason>
        /// 
        /// Returned when this channel name has been explicitly blocked and is not allowed to be used.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_FORBIDDENCHANNEL = new Numeric("ERR_FORBIDDENCHANNEL", 448);

        /// <summary>
        /// Unspecified
        /// 
        /// Returned when a requested feature is not implemented (and cannot be completed)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOTIMPLEMENTED = new Numeric("ERR_NOTIMPLEMENTED", 449);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Returned by the server to indicate that the client must be registered before the server will allow it to be parsed in detail
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOTREGISTERED = new Numeric("ERR_NOTREGISTERED", 451);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_IDCOLLISION = new Numeric("ERR_IDCOLLISION", 452);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NICKLOST = new Numeric("ERR_NICKLOST", 453);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_HOSTILENAME = new Numeric("ERR_HOSTILENAME", 455);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_ACCEPTFULL = new Numeric("ERR_ACCEPTFULL", 456);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_ACCEPTEXIST = new Numeric("ERR_ACCEPTEXIST", 457);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_ACCEPTNOT = new Numeric("ERR_ACCEPTNOT", 458);

        /// <summary>
        /// Not allowed to become an invisible operator?
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOHIDING = new Numeric("ERR_NOHIDING", 459);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOTFORHALFOPS = new Numeric("ERR_NOTFORHALFOPS", 460);

        /// <summary>
        /// <client> <command> :<reason>
        /// 
        /// Returned by the server by any command which requires more parameters than the number of parameters given
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NEEDMOREPARAMS = new Numeric("ERR_NEEDMOREPARAMS", 461);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Returned by the server to any link which attempts to register again
        /// Also known as ERR_ALREADYREGISTRED (sic) in ratbox/charybdis.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_ALREADYREGISTERED = new Numeric("ERR_ALREADYREGISTERED", 462);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Returned to a client which attempts to register with a server which has been configured to refuse connections from the client's host
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOPERMFORHOST = new Numeric("ERR_NOPERMFORHOST", 463);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Returned by the PASS command to indicate the given password was required and was either not given or was incorrect
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_PASSWDMISMATCH = new Numeric("ERR_PASSWDMISMATCH", 464);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Returned to a client after an attempt to register on a server configured to ban connections from that client
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_YOUREBANNEDCREEP = new Numeric("ERR_YOUREBANNEDCREEP", 465);

        /// <summary>
        /// Sent by a server to a user to inform that access to the server will soon be denied
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_YOUWILLBEBANNED = new Numeric("ERR_YOUWILLBEBANNED", 466);

        /// <summary>
        /// <client> <channel> :<reason>
        /// 
        /// Returned when the channel key for a channel has already been set
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_KEYSET = new Numeric("ERR_KEYSET", 467);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_INVALIDUSERNAME = new Numeric("ERR_INVALIDUSERNAME", 468);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOCODEPAGE = new Numeric("ERR_NOCODEPAGE", 468);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_ONLYSERVERSCANCHANGE = new Numeric("ERR_ONLYSERVERSCANCHANGE", 468);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_LINKSET = new Numeric("ERR_LINKSET", 469);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_KICKEDFROMCHAN = new Numeric("ERR_KICKEDFROMCHAN", 470);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_LINKCHANNEL = new Numeric("ERR_LINKCHANNEL", 470);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_7BIT = new Numeric("ERR_7BIT", 470);

        /// <summary>
        /// <client> <channel> :<reason>
        /// 
        /// Returned when attempting to join a channel which is set +l and is already full
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_CHANNELISFULL = new Numeric("ERR_CHANNELISFULL", 471);

        /// <summary>
        /// <client> <char> :<reason>
        /// 
        /// Returned when a given mode is unknown
        /// 
        /// </summary>
        public static readonly Numeric ERR_UNKNOWNMODE = new Numeric("ERR_UNKNOWNMODE", 472);

        /// <summary>
        /// <client> <channel> :<reason>
        /// 
        /// Returned when attempting to join a channel which is invite only without an invitation
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_INVITEONLYCHAN = new Numeric("ERR_INVITEONLYCHAN", 473);

        /// <summary>
        /// <client> <channel> :<reason>
        /// 
        /// Returned when attempting to join a channel a user is banned from
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_BANNEDFROMCHAN = new Numeric("ERR_BANNEDFROMCHAN", 474);

        /// <summary>
        /// <client> <channel> :<reason>
        /// 
        /// Returned when attempting to join a key-locked channel either without a key or with the wrong key
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_BADCHANNELKEY = new Numeric("ERR_BADCHANNELKEY", 475);

        /// <summary>
        /// <client> <channel> :<reason>
        /// 
        /// The given channel mask was invalid
        /// 
        /// </summary>
        public static readonly Numeric ERR_BADCHANMASK = new Numeric("ERR_BADCHANMASK", 476);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NEEDREGGEDNICK = new Numeric("ERR_NEEDREGGEDNICK", 477);

        /// <summary>
        /// <client> <channel> :<reason>
        /// 
        /// Returned when attempting to set a mode on a channel which does not support channel modes, or channel mode changes. Also known as ERR_MODELESS
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOCHANMODES = new Numeric("ERR_NOCHANMODES", 477);

        /// <summary>
        /// <client> <channel> <char> :<reason>
        /// 
        /// Returned when a channel access list (i.e. ban list etc) is full and cannot be added to
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_BANLISTFULL = new Numeric("ERR_BANLISTFULL", 478);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOCOLOR = new Numeric("ERR_NOCOLOR", 479);

        /// <summary>
        /// <client> <channel> :<reason>
        /// 
        /// Returned to indicate that a given channel name is not valid. Servers that implement this use it instead of `ERR_NOSUCHCHANNEL` where appropriate.
        /// 
        /// 
        /// See also: ERR_NOSUCHCHANNEL (403)
        /// 
        /// </summary>
        public static readonly Numeric ERR_BADCHANNAME = new Numeric("ERR_BADCHANNAME", 479);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_LINKFAIL = new Numeric("ERR_LINKFAIL", 479);

        /// <summary>
        /// <nick> <channel> :Cannot join channel
        /// 
        /// </summary>
        public static readonly Numeric ERR_THROTTLE = new Numeric("ERR_THROTTLE", 480);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOWALLOP = new Numeric("ERR_NOWALLOP", 480);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOULINE = new Numeric("ERR_NOULINE", 480);

        /// <summary>
        /// Moved to 489 to match other servers.
        /// 
        /// 
        /// See also: ERR_SECUREONLYCHAN (489)
        /// 
        /// </summary>
        public static readonly Numeric ERR_SSLONLYCHAN = new Numeric("ERR_SSLONLYCHAN", 480);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_CANNOTKNOCK = new Numeric("ERR_CANNOTKNOCK", 480);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Returned by any command requiring special privileges (eg. IRC operator) to indicate the operation was unsuccessful
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOPRIVILEGES = new Numeric("ERR_NOPRIVILEGES", 481);

        /// <summary>
        /// <client> <channel> :<reason>
        /// 
        /// Returned by any command requiring special channel privileges (eg. channel operator) to indicate the operation was unsuccessful. InspIRCd also uses this numeric "for other things like trying to kick a uline"
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_CHANOPRIVSNEEDED = new Numeric("ERR_CHANOPRIVSNEEDED", 482);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Returned by KILL to anyone who tries to kill a server
        /// 
        /// </summary>
        public static readonly Numeric ERR_CANTKILLSERVER = new Numeric("ERR_CANTKILLSERVER", 483);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Sent by the server to a user upon connection to indicate the restricted nature of the connection (i.e. usermode +r)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_RESTRICTED = new Numeric("ERR_RESTRICTED", 484);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_ATTACKDENY = new Numeric("ERR_ATTACKDENY", 484);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_DESYNC = new Numeric("ERR_DESYNC", 484);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_ISCHANSERVICE = new Numeric("ERR_ISCHANSERVICE", 484);

        /// <summary>
        /// <client> <channel> :Cannot join channel (<reason>)
        /// 
        /// </summary>
        public static readonly Numeric ERR_CHANBANREASON = new Numeric("ERR_CHANBANREASON", 485);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Any mode requiring 'channel creator' privileges returns this error if the client is attempting to use it while not a channel creator on the given channel
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_UNIQOPRIVSNEEDED = new Numeric("ERR_UNIQOPRIVSNEEDED", 485);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_ISREALSERVICE = new Numeric("ERR_ISREALSERVICE", 485);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_KILLDENY = new Numeric("ERR_KILLDENY", 485);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_CANTKICKADMIN = new Numeric("ERR_CANTKICKADMIN", 485);

        /// <summary>
        /// Defined in header file, but never used.
        /// 
        /// </summary>
        public static readonly Numeric ERR_BANNEDNICK = new Numeric("ERR_BANNEDNICK", 485);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_RLINED = new Numeric("ERR_RLINED", 486);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NONONREG = new Numeric("ERR_NONONREG", 486);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_ACCOUNTONLY = new Numeric("ERR_ACCOUNTONLY", 486);

        /// <summary>
        /// Unreal 3.2 uses 488 as the ERR_HTMDISABLED numeric instead
        /// 
        /// </summary>
        public static readonly Numeric ERR_HTMDISABLED = new Numeric("ERR_HTMDISABLED", 486, 488);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_CHANTOORECENT = new Numeric("ERR_CHANTOORECENT", 487);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_MSGSERVICES = new Numeric("ERR_MSGSERVICES", 487);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOTFORUSERS = new Numeric("ERR_NOTFORUSERS", 487);

        /// <summary>
        /// <client> <channel> :SSL Only channel (+S), You must connect using SSL to join this channel.
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOSSL = new Numeric("ERR_NOSSL", 488);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_TSLESSCHAN = new Numeric("ERR_TSLESSCHAN", 488);

        /// <summary>
        /// Also known as ERR_SSLONLYCHAN.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_SECUREONLYCHAN = new Numeric("ERR_SECUREONLYCHAN", 489);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_VOICENEEDED = new Numeric("ERR_VOICENEEDED", 489);

        /// <summary>
        /// <client> :<nick> does not accept private messages containing swearing.
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOSWEAR = new Numeric("ERR_NOSWEAR", 490);

        /// <summary>
        /// <client> <channel> :all members of the channel must be connected via SSL
        /// 
        /// </summary>
        public static readonly Numeric ERR_ALLMUSTSSL = new Numeric("ERR_ALLMUSTSSL", 490);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Returned by OPER to a client who cannot become an IRC operator because the server has been configured to disallow the client's host
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOOPERHOST = new Numeric("ERR_NOOPERHOST", 491);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOSERVICEHOST = new Numeric("ERR_NOSERVICEHOST", 492);

        /// <summary>
        /// <client> :You cannot send CTCPs to this channel. Not sent: <message>
        /// 
        /// Notifies the user that a message they have sent to a channel has been rejected as it contains CTCPs, and they cannot send messages containing CTCPs to this channel. Also known as ERR_NOCTCPALLOWED (InspIRCd).
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOCTCP = new Numeric("ERR_NOCTCP", 492);

        /// <summary>
        /// <client> :Cannot send to user <nick> (<reason>)
        /// 
        /// </summary>
        public static readonly Numeric ERR_CANNOTSENDTOUSER = new Numeric("ERR_CANNOTSENDTOUSER", 492);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOFEATURE = new Numeric("ERR_NOFEATURE", 493);

        /// <summary>
        /// <client> :You cannot message that person because you do not share a common channel with them.
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOSHAREDCHAN = new Numeric("ERR_NOSHAREDCHAN", 493);

        /// <summary>
        /// <client> <nick> :cannot answer you while you are <mode>, your message was not sent
        /// 
        /// Used for mode +g (CALLERID) in charybdis.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_OWNMODE = new Numeric("ERR_OWNMODE", 494);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_BADFEATVALUE = new Numeric("ERR_BADFEATVALUE", 494);

        /// <summary>
        /// <channel> :You cannot rejoin this channel yet after being kicked (+J)
        /// 
        /// This numeric is marked as "we should use 'resource temporarily unavailable' from ircnet/ratbox or whatever"
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_DELAYREJOIN = new Numeric("ERR_DELAYREJOIN", 495);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_BADLOGTYPE = new Numeric("ERR_BADLOGTYPE", 495);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_BADLOGSYS = new Numeric("ERR_BADLOGSYS", 496);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_BADLOGVALUE = new Numeric("ERR_BADLOGVALUE", 497);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_ISOPERLCHAN = new Numeric("ERR_ISOPERLCHAN", 498);

        /// <summary>
        /// Works just like ERR_CHANOPRIVSNEEDED except it indicates that owner status (+q) is needed.
        /// 
        /// 
        /// See also: ERR_CHANOPRIVSNEEDED (482)
        /// 
        /// </summary>
        public static readonly Numeric ERR_CHANOWNPRIVNEEDED = new Numeric("ERR_CHANOWNPRIVNEEDED", 499);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOREHASHPARAM = new Numeric("ERR_NOREHASHPARAM", 500);

        /// <summary>
        /// <client> <string> :Too many join requests. Please wait a while and try again.
        /// 
        /// </summary>
        public static readonly Numeric ERR_TOOMANYJOINS = new Numeric("ERR_TOOMANYJOINS", 500);

        /// <summary>
        /// <client> <snomask> :is unknown mode char to me
        /// 
        /// </summary>
        public static readonly Numeric ERR_UNKNOWNSNOMASK = new Numeric("ERR_UNKNOWNSNOMASK", 501);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Returned by the server to indicate that a MODE message was sent with a nickname parameter and that the mode flag sent was not recognised.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_UMODEUNKNOWNFLAG = new Numeric("ERR_UMODEUNKNOWNFLAG", 501);

        /// <summary>
        /// <client> :<reason>
        /// 
        /// Error sent to any user trying to view or change the user mode for a user other than themselves
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_USERSDONTMATCH = new Numeric("ERR_USERSDONTMATCH", 502);

        /// <summary>
        /// <client> :Message could not be delivered to <target>
        /// 
        /// </summary>
        public static readonly Numeric ERR_GHOSTEDCLIENT = new Numeric("ERR_GHOSTEDCLIENT", 503);

        /// <summary>
        /// <client> :<warning_text>
        /// 
        /// Warning about Virtual-World being turned off. Obsoleted in favour for RPL_MODECHANGEWARN
        /// 
        /// 
        /// See also: RPL_MODECHANGEWARN (662)
        /// 
        /// </summary>
        public static readonly Numeric ERR_VWORLDWARN = new Numeric("ERR_VWORLDWARN", 503);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_USERNOTONSERV = new Numeric("ERR_USERNOTONSERV", 504);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_SILELISTFULL = new Numeric("ERR_SILELISTFULL", 511);

        /// <summary>
        /// Also known as ERR_NOTIFYFULL (aircd), I presume they are the same
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_TOOMANYWATCH = new Numeric("ERR_TOOMANYWATCH", 512);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOSUCHGLINE = new Numeric("ERR_NOSUCHGLINE", 512, 521);

        /// <summary>
        /// Also known as ERR_NEEDPONG (Unreal/Ultimate) for use during registration, however it is not used in Unreal (and might not be used in Ultimate either).
        /// Also known as ERR_WRONGPONG (Ratbox/charybdis)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_BADPING = new Numeric("ERR_BADPING", 513);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_INVALID_ERROR = new Numeric("ERR_INVALID_ERROR", 514);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOSUCHJUPE = new Numeric("ERR_NOSUCHJUPE", 514);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_TOOMANYDCC = new Numeric("ERR_TOOMANYDCC", 514);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_BADEXPIRE = new Numeric("ERR_BADEXPIRE", 515);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_DONTCHEAT = new Numeric("ERR_DONTCHEAT", 516);

        /// <summary>
        /// <client> <command> :<info/reason>
        /// 
        /// </summary>
        public static readonly Numeric ERR_DISABLED = new Numeric("ERR_DISABLED", 517);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOINVITE = new Numeric("ERR_NOINVITE", 518);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_LONGMASK = new Numeric("ERR_LONGMASK", 518);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_TOOMANYUSERS = new Numeric("ERR_TOOMANYUSERS", 519);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_ADMONLY = new Numeric("ERR_ADMONLY", 519);

        /// <summary>
        /// This is considered obsolete in favour of ERR_TOOMANYMATCHES, and should no longer be used.
        /// 
        /// 
        /// See also: ERR_TOOMANYMATCHES (416)
        /// 
        /// </summary>
        public static readonly Numeric ERR_WHOTRUNC = new Numeric("ERR_WHOTRUNC", 520);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_MASKTOOWIDE = new Numeric("ERR_MASKTOOWIDE", 520);

        /// <summary>
        /// :Cannot join channel (+O)
        /// 
        /// Also known as ERR_OPERONLYCHAN (Hybrid) and ERR_CANTJOINOPERSONLY (InspIRCd).
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_OPERONLY = new Numeric("ERR_OPERONLY", 520);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_LISTSYNTAX = new Numeric("ERR_LISTSYNTAX", 521);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_WHOSYNTAX = new Numeric("ERR_WHOSYNTAX", 522);

        /// <summary>
        /// <limit> :<command> search limit exceeded.
        /// 
        /// </summary>
        public static readonly Numeric ERR_WHOLIMEXCEED = new Numeric("ERR_WHOLIMEXCEED", 523);

        /// <summary>
        /// <term> :Help not found
        /// 
        /// </summary>
        public static readonly Numeric ERR_HELPNOTFOUND = new Numeric("ERR_HELPNOTFOUND", 524);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_QUARANTINED = new Numeric("ERR_QUARANTINED", 524);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_OPERSPVERIFY = new Numeric("ERR_OPERSPVERIFY", 524);

        /// <summary>
        /// <nickname> :<reason>
        /// 
        /// Proposed.
        /// http://www.hades.skumler.net/~ejb/draft-brocklesby-irc-usercmdpfx-00.txt
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_REMOTEPFX = new Numeric("ERR_REMOTEPFX", 525);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_INVALIDKEY = new Numeric("ERR_INVALIDKEY", 525);

        /// <summary>
        /// <nickname> :<reason>
        /// 
        /// Proposed.
        /// http://www.hades.skumler.net/~ejb/draft-brocklesby-irc-usercmdpfx-00.txt
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_PFXUNROUTABLE = new Numeric("ERR_PFXUNROUTABLE", 526);

        /// <summary>
        /// <nick> :You are not permitted to send private messages to this user
        /// 
        /// </summary>
        public static readonly Numeric ERR_CANTSENDTOUSER = new Numeric("ERR_CANTSENDTOUSER", 531);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_BADHOSTMASK = new Numeric("ERR_BADHOSTMASK", 550);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_HOSTUNAVAIL = new Numeric("ERR_HOSTUNAVAIL", 551);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_USINGSLINE = new Numeric("ERR_USINGSLINE", 552);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_STATSSLINE = new Numeric("ERR_STATSSLINE", 553);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOTLOWEROPLEVEL = new Numeric("ERR_NOTLOWEROPLEVEL", 560);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOTMANAGER = new Numeric("ERR_NOTMANAGER", 561);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_CHANSECURED = new Numeric("ERR_CHANSECURED", 562);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_UPASSSET = new Numeric("ERR_UPASSSET", 563);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_UPASSNOTSET = new Numeric("ERR_UPASSNOTSET", 564);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_NOMANAGER = new Numeric("ERR_NOMANAGER", 566);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_UPASS_SAME_APASS = new Numeric("ERR_UPASS_SAME_APASS", 567);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_NOOMOTD = new Numeric("RPL_NOOMOTD", 568);

        /// <summary>
        /// </summary>
        public static readonly Numeric ERR_LASTERROR = new Numeric("ERR_LASTERROR", 975, 568);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_REAWAY = new Numeric("RPL_REAWAY", 597);

        /// <summary>
        /// Used when adding users to their <code>WATCH</code> list.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_GONEAWAY = new Numeric("RPL_GONEAWAY", 598);

        /// <summary>
        /// Used when adding users to their <code>WATCH</code> list.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_NOTAWAY = new Numeric("RPL_NOTAWAY", 599);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_LOGON = new Numeric("RPL_LOGON", 600);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_LOGOFF = new Numeric("RPL_LOGOFF", 601);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WATCHOFF = new Numeric("RPL_WATCHOFF", 602);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WATCHSTAT = new Numeric("RPL_WATCHSTAT", 603);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_NOWON = new Numeric("RPL_NOWON", 604);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_NOWOFF = new Numeric("RPL_NOWOFF", 605);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WATCHLIST = new Numeric("RPL_WATCHLIST", 606);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDOFWATCHLIST = new Numeric("RPL_ENDOFWATCHLIST", 607);

        /// <summary>
        /// Also known as RPL_CLEARWATCH in Unreal
        /// 
        /// </summary>
        public static readonly Numeric RPL_WATCHCLEAR = new Numeric("RPL_WATCHCLEAR", 608);

        /// <summary>
        /// Returned when adding users to their <code>WATCH</code> list.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_NOWISAWAY = new Numeric("RPL_NOWISAWAY", 609);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ISOPER = new Numeric("RPL_ISOPER", 610);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ISLOCOP = new Numeric("RPL_ISLOCOP", 611);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ISNOTOPER = new Numeric("RPL_ISNOTOPER", 612);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDOFISOPER = new Numeric("RPL_ENDOFISOPER", 613);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_DCCSTATUS = new Numeric("RPL_DCCSTATUS", 617);

        /// <summary>
        /// <client> <nick> :has client certificate fingerprint <fingerprint>
        /// 
        /// See also: RPL_WHOISCERTFP (276)
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOISSSLFP = new Numeric("RPL_WHOISSSLFP", 617);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_DCCLIST = new Numeric("RPL_DCCLIST", 618);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDOFDCCLIST = new Numeric("RPL_ENDOFDCCLIST", 619);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_WHOWASHOST = new Numeric("RPL_WHOWASHOST", 619);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_DCCINFO = new Numeric("RPL_DCCINFO", 620);

        /// <summary>
        /// <client> :<text>
        /// 
        /// IRC Operator MOTD header, sent upon OPER command
        /// 
        /// </summary>
        public static readonly Numeric RPL_OMOTDSTART = new Numeric("RPL_OMOTDSTART", 624, 720);

        /// <summary>
        /// <client> :<text>
        /// 
        /// IRC Operator MOTD text (repeated, usually)
        /// 
        /// </summary>
        public static readonly Numeric RPL_OMOTD = new Numeric("RPL_OMOTD", 625, 721);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDOFO = new Numeric("RPL_ENDOFO", 626);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_SETTINGS = new Numeric("RPL_SETTINGS", 630);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_ENDOFSETTINGS = new Numeric("RPL_ENDOFSETTINGS", 631);

        /// <summary>
        /// Never actually used by Unreal - was defined however the feature that would have used this numeric was never created.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_DUMPING = new Numeric("RPL_DUMPING", 640);

        /// <summary>
        /// Never actually used by Unreal - was defined however the feature that would have used this numeric was never created.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_DUMPRPL = new Numeric("RPL_DUMPRPL", 641);

        /// <summary>
        /// Never actually used by Unreal - was defined however the feature that would have used this numeric was never created.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_EODUMP = new Numeric("RPL_EODUMP", 642);

        /// <summary>
        /// <client> <command> :Command processed, but a copy has been sent to ircops for evaluation (anti-spam) purposes. [<reason>]
        /// 
        /// Used to let a client know that a copy of their command has been passed to operators and the reason for it.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_SPAMCMDFWD = new Numeric("RPL_SPAMCMDFWD", 659);

        /// <summary>
        /// <client> :STARTTLS successful, proceed with TLS handshake
        /// 
        /// Indicates that the client may begin the TLS handshake
        /// 
        /// </summary>
        public static readonly Numeric RPL_STARTTLS = new Numeric("RPL_STARTTLS", 670);

        /// <summary>
        /// <client> <nick> :is using a secure connection
        /// 
        /// The text in the last parameter may change. Also known as RPL_WHOISSSL (Nefarious).
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOISSECURE = new Numeric("RPL_WHOISSECURE", 671);

        /// <summary>
        /// <client> <nick> :is actually from <ip>
        /// 
        /// Returns the real IP address of a client connected from a CGIIRC host, this has the real IP address of the client. This message is only sent to themselves or to IRC operators who perform a WHOIS on the user.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOISREALIP = new Numeric("RPL_WHOISREALIP", 672);

        /// <summary>
        /// <modes> :<info>
        /// 
        /// Returns a full list of modes that are unknown when a client issues a MODE command (rather than one numeric per mode)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_UNKNOWNMODES = new Numeric("RPL_UNKNOWNMODES", 672);

        /// <summary>
        /// <modes> :<info>
        /// 
        /// Returns a full list of modes that cannot be set when a client issues a MODE command
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_CANNOTSETMODES = new Numeric("RPL_CANNOTSETMODES", 673);

        /// <summary>
        /// </summary>
        public static readonly Numeric RPL_LANGUAGES = new Numeric("RPL_LANGUAGES", 690);

        /// <summary>
        /// <client> :STARTTLS failed (Wrong moon phase)
        /// 
        /// Indicates that a server-side error has occured
        /// 
        /// </summary>
        public static readonly Numeric ERR_STARTTLS = new Numeric("ERR_STARTTLS", 691);

        /// <summary>
        /// <client> :<command> <module name> <minimum parameters>
        /// 
        /// </summary>
        public static readonly Numeric RPL_COMMANDS = new Numeric("RPL_COMMANDS", 702);

        /// <summary>
        /// :End of COMMANDS list
        /// 
        /// </summary>
        public static readonly Numeric RPL_COMMANDSEND = new Numeric("RPL_COMMANDSEND", 703);

        /// <summary>
        /// <client> :<text>
        /// 
        /// Terminates MODLIST output
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFMODLIST = new Numeric("RPL_ENDOFMODLIST", 703);

        /// <summary>
        /// <client> <command> :<text>
        /// 
        /// Start of HELP command output
        /// 
        /// </summary>
        public static readonly Numeric RPL_HELPSTART = new Numeric("RPL_HELPSTART", 704);

        /// <summary>
        /// <client> <command> :<text>
        /// 
        /// Output from HELP command
        /// 
        /// </summary>
        public static readonly Numeric RPL_HELPTXT = new Numeric("RPL_HELPTXT", 705);

        /// <summary>
        /// <client> <command> :<text>
        /// 
        /// End of HELP command output
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFHELP = new Numeric("RPL_ENDOFHELP", 706);

        /// <summary>
        /// <client> <target> :Targets changing too fast, message dropped
        /// 
        /// See doc/tgchange.txt in the charybdis source.
        /// 
        /// </summary>
        public static readonly Numeric ERR_TARGCHANGE = new Numeric("ERR_TARGCHANGE", 707);

        /// <summary>
        /// <?> <?> <?> <?> <?> <?> <?> :<?>
        /// 
        /// Output from 'extended' trace
        /// 
        /// </summary>
        public static readonly Numeric RPL_ETRACEFULL = new Numeric("RPL_ETRACEFULL", 708);

        /// <summary>
        /// <?> <?> <?> <?> <?> <?> :<?>
        /// 
        /// Output from 'extended' trace
        /// 
        /// </summary>
        public static readonly Numeric RPL_ETRACE = new Numeric("RPL_ETRACE", 709);

        /// <summary>
        /// <client> <channel> <nick>!<user>@<host> :<text>
        /// 
        /// Message delivered using KNOCK command
        /// 
        /// </summary>
        public static readonly Numeric RPL_KNOCK = new Numeric("RPL_KNOCK", 710);

        /// <summary>
        /// <client> <channel> :<text>
        /// 
        /// Message returned from using KNOCK command (KNOCK delivered)
        /// 
        /// </summary>
        public static readonly Numeric RPL_KNOCKDLVR = new Numeric("RPL_KNOCKDLVR", 711);

        /// <summary>
        /// <client> <channel> :<text>
        /// 
        /// Message returned when too many KNOCKs for a channel have been sent by a user
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_TOOMANYKNOCK = new Numeric("ERR_TOOMANYKNOCK", 712);

        /// <summary>
        /// <client> <channel> :<text>
        /// 
        /// Message returned from KNOCK when the channel can be freely joined by the user
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_CHANOPEN = new Numeric("ERR_CHANOPEN", 713);

        /// <summary>
        /// <client> <channel> :<text>
        /// 
        /// Message returned from KNOCK when the user has used KNOCK on a channel they have already joined
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_KNOCKONCHAN = new Numeric("ERR_KNOCKONCHAN", 714);

        /// <summary>
        /// <client> <channel> :Too many INVITEs (<channel/user>).
        /// 
        /// Sent to indicate an INVITE has been blocked. The last param is the literal string "channel" if this is because the channel has had too many INVITEs in a given time, and "user" if this is because the user has sent too many INVITEs in a given time
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_TOOMANYINVITE = new Numeric("ERR_TOOMANYINVITE", 715);

        /// <summary>
        /// <client> <nick> <channel> :You are inviting too fast, invite to <nick> for <channel> not sent.
        /// 
        /// Sent to indicate an INVITE has been blocked.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_INVITETHROTTLE = new Numeric("RPL_INVITETHROTTLE", 715);

        /// <summary>
        /// <client> :<text>
        /// 
        /// Returned from KNOCK when the command has been disabled
        /// 
        /// </summary>
        public static readonly Numeric ERR_KNOCKDISABLED = new Numeric("ERR_KNOCKDISABLED", 715);

        /// <summary>
        /// <nick> :<info>
        /// 
        /// Sent to indicate the given target is set +g (server-side ignore)
        /// Mentioned as RPL_TARGUMODEG in the CALLERID spec, ERR_TARGUMODEG in the ratbox/charybdis implementations.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_TARGUMODEG = new Numeric("RPL_TARGUMODEG", 716);

        /// <summary>
        /// <nick> :<info>
        /// 
        /// Sent following a PRIVMSG/NOTICE to indicate the target has been notified of an attempt to talk to them while they are set +g
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_TARGNOTIFY = new Numeric("RPL_TARGNOTIFY", 717);

        /// <summary>
        /// <client> <nick> <user>@<host> :<info>
        /// 
        /// Sent to a user who is +g to inform them that someone has attempted to talk to them (via PRIVMSG/NOTICE), and that they will need to be accepted (via the ACCEPT command) before being able to talk to them
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_UMODEGMSG = new Numeric("RPL_UMODEGMSG", 718);

        /// <summary>
        /// <client> :<text>
        /// 
        /// IRC operator MOTD footer
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFOMOTD = new Numeric("RPL_ENDOFOMOTD", 722);

        /// <summary>
        /// <client> <command> :<text>
        /// 
        /// Returned from an oper command when the IRC operator does not have the relevant operator privileges.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOPRIVS = new Numeric("ERR_NOPRIVS", 723);

        /// <summary>
        /// <client> <nick>!<user>@<host> <?> <?> :<text>
        /// 
        /// Reply from an oper command reporting how many users match a given user@host mask
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_TESTMASK = new Numeric("RPL_TESTMASK", 724);

        /// <summary>
        /// <client> <?> <?> <?> :<?>
        /// 
        /// Reply from an oper command reporting relevant I/K lines that will match a given user@host
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_TESTLINE = new Numeric("RPL_TESTLINE", 725);

        /// <summary>
        /// <client> <?> :<text>
        /// 
        /// Reply from oper command reporting no I/K lines match the given user@host
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_NOTESTLINE = new Numeric("RPL_NOTESTLINE", 726);

        /// <summary>
        /// <client> <lcount> <gcount> <nick>!<user>@<host> <gecos> :Local/remote clients match
        /// 
        /// From the m_testmask module, "Shows the number of matching local and global clients for a user@host mask"
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_TESTMASKGECOS = new Numeric("RPL_TESTMASKGECOS", 727);

        /// <summary>
        /// <client> <channel> <banid> q [<time_left> :<reason>]
        /// 
        /// Same thing as RPL_BANLIST, but for mode +q (quiet)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_QUIETLIST = new Numeric("RPL_QUIETLIST", 728);

        /// <summary>
        /// <client> <channel> q :<info>
        /// 
        /// Same thing as RPL_ENDOFBANLIST, but for mode +q (quiet)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFQUIETLIST = new Numeric("RPL_ENDOFQUIETLIST", 729);

        /// <summary>
        /// <client> :target[!user@host][,target[!user@host]]*
        /// 
        /// Used to indicate to a client that either a target has just become online, or that a target they have added to their monitor list is online
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_MONONLINE = new Numeric("RPL_MONONLINE", 730);

        /// <summary>
        /// <client> :target[,target2]*
        /// 
        /// Used to indicate to a client that either a target has just left the IRC network, or that a target they have added to their monitor list is offline
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_MONOFFLINE = new Numeric("RPL_MONOFFLINE", 731);

        /// <summary>
        /// <client> :target[,target2]*
        /// 
        /// Used to indicate to a client the list of targets they have in their monitor list
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_MONLIST = new Numeric("RPL_MONLIST", 732);

        /// <summary>
        /// <client> :End of MONITOR list
        /// 
        /// Used to indicate to a client the end of a monitor list
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFMONLIST = new Numeric("RPL_ENDOFMONLIST", 733);

        /// <summary>
        /// <client> <limit> <targets> :Monitor list is full.
        /// 
        /// Used to indicate to a client that their monitor list is full, so the MONITOR command failed
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_MONLISTFULL = new Numeric("ERR_MONLISTFULL", 734);

        /// <summary>
        /// <client> :<chal_line>
        /// 
        /// From the ratbox m_challenge module, to auth opers.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_RSACHALLENGE2 = new Numeric("RPL_RSACHALLENGE2", 740);

        /// <summary>
        /// <client> :End of CHALLENGE
        /// 
        /// From the ratbox m_challenge module, to auth opers.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_ENDOFRSACHALLENGE2 = new Numeric("RPL_ENDOFRSACHALLENGE2", 741);

        /// <summary>
        /// <channel> <modechar> <mlock> :MODE cannot be set due to channel having an active MLOCK restriction policy
        /// 
        /// </summary>
        public static readonly Numeric ERR_MLOCKRESTRICTED = new Numeric("ERR_MLOCKRESTRICTED", 742);

        /// <summary>
        /// <channel> <modechar> <mask> :Invalid ban mask
        /// 
        /// </summary>
        public static readonly Numeric ERR_INVALIDBAN = new Numeric("ERR_INVALIDBAN", 743);

        /// <summary>
        /// Defined in the Charybdis source code with the comment <code>/* inspircd */</code>
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_TOPICLOCK = new Numeric("ERR_TOPICLOCK", 744);

        /// <summary>
        /// <count> :matches
        /// 
        /// From the ratbox m_scan module.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_SCANMATCHED = new Numeric("RPL_SCANMATCHED", 750);

        /// <summary>
        /// <nick> <username> <host> <sockhost> <servname> <umodes> :<info>
        /// 
        /// From the ratbox m_scan module.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_SCANUMODES = new Numeric("RPL_SCANUMODES", 751);

        /// <summary>
        /// <Target> <Key> <Visibility> :<Value>
        /// 
        /// Reply to WHOIS - Metadata key/value associated with the target
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_WHOISKEYVALUE = new Numeric("RPL_WHOISKEYVALUE", 760);

        /// <summary>
        /// <Target> <Key> <Visibility>[ :<Value>]
        /// 
        /// Returned to show a currently set metadata key and its value, or a metadata key that has been cleared if no value is present in the response
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_KEYVALUE = new Numeric("RPL_KEYVALUE", 761);

        /// <summary>
        /// :end of metadata
        /// 
        /// Indicates the end of a list of metadata keys
        /// 
        /// </summary>
        public static readonly Numeric RPL_METADATAEND = new Numeric("RPL_METADATAEND", 762);

        /// <summary>
        /// <Target> :metadata limit reached
        /// 
        /// Used to indicate to a client that their metadata store is full, and they cannot add the requested key(s)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_METADATALIMIT = new Numeric("ERR_METADATALIMIT", 764);

        /// <summary>
        /// <Target> :invalid metadata target
        /// 
        /// Indicates to a client that the target of a sent METADATA command is invalid
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_TARGETINVALID = new Numeric("ERR_TARGETINVALID", 765);

        /// <summary>
        /// <Key> :no matching key
        /// 
        /// Indicates to a client that the requested metadata key does not exist
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NOMATCHINGKEY = new Numeric("ERR_NOMATCHINGKEY", 766);

        /// <summary>
        /// <Key> :invalid metadata key
        /// 
        /// Indicates to a client that the requested metadata key is not valid
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_KEYINVALID = new Numeric("ERR_KEYINVALID", 767);

        /// <summary>
        /// <Target> <Key> :key not set
        /// 
        /// Indicates to a client that the metadata key they requested to clear is not already set
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_KEYNOTSET = new Numeric("ERR_KEYNOTSET", 768);

        /// <summary>
        /// <Target> <Key> :permission denied
        /// 
        /// Indicates to a client that they do not have permission to set the requested metadata key
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_KEYNOPERMISSION = new Numeric("ERR_KEYNOPERMISSION", 769);

        /// <summary>
        /// Used to send 'eXtended info' to the client, a replacement for the STATS command to send a large variety of data and minimise numeric pollution.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_XINFO = new Numeric("RPL_XINFO", 771);

        /// <summary>
        /// Start of an RPL_XINFO list
        /// 
        /// </summary>
        public static readonly Numeric RPL_XINFOSTART = new Numeric("RPL_XINFOSTART", 773);

        /// <summary>
        /// Termination of an RPL_XINFO list
        /// 
        /// </summary>
        public static readonly Numeric RPL_XINFOEND = new Numeric("RPL_XINFOEND", 774);

        /// <summary>
        /// Used by the m_check module of InspIRCd.
        /// 
        /// </summary>
        public static readonly Numeric RPL_CHECK = new Numeric("RPL_CHECK", 802);

        /// <summary>
        /// <client> <nick>!<ident>@<host> <account> :You are now logged in as <user>
        /// 
        /// Sent when the user's account name is set (whether by SASL or otherwise)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_LOGGEDIN = new Numeric("RPL_LOGGEDIN", 900);

        /// <summary>
        /// <client> <nick>!<ident>@<host> :You are now logged out
        /// 
        /// Sent when the user's account name is unset (whether by SASL or otherwise)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_LOGGEDOUT = new Numeric("RPL_LOGGEDOUT", 901);

        /// <summary>
        /// <client> :You must use a nick assigned to you.
        /// 
        /// Sent when the SASL authentication fails because the account is currently locked out, held, or otherwise administratively made unavailable.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_NICKLOCKED = new Numeric("ERR_NICKLOCKED", 902);

        /// <summary>
        /// <client> :SASL authentication successful
        /// 
        /// Sent when the SASL authentication finishes successfully
        /// 
        /// 
        /// See also: RPL_LOGGEDIN (900)
        /// 
        /// </summary>
        public static readonly Numeric RPL_SASLSUCCESS = new Numeric("RPL_SASLSUCCESS", 903);

        /// <summary>
        /// <client> :SASL authentication failed
        /// 
        /// Sent when the SASL authentication fails because of invalid credentials or other errors not explicitly mentioned by other numerics
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_SASLFAIL = new Numeric("ERR_SASLFAIL", 904);

        /// <summary>
        /// <client> :SASL message too long
        /// 
        /// Sent when credentials are valid, but the SASL authentication fails because the client-sent AUTHENTICATE command was too long (i.e. the parameter longer than 400 bytes)
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_SASLTOOLONG = new Numeric("ERR_SASLTOOLONG", 905);

        /// <summary>
        /// <client> :SASL authentication aborted
        /// 
        /// Sent when the SASL authentication is aborted because the client sent an AUTHENTICATE command with * as the parameter
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_SASLABORTED = new Numeric("ERR_SASLABORTED", 906);

        /// <summary>
        /// <client> :You have already authenticated using SASL
        /// 
        /// Sent when the client attempts to initiate SASL authentication after it has already finished successfully for that connection.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_SASLALREADY = new Numeric("ERR_SASLALREADY", 907);

        /// <summary>
        /// <client> <mechanisms> :are available SASL mechanisms
        /// 
        /// Sent when the client requests a list of SASL mechanisms supported by the server (or network, services). The numeric contains a comma-separated list of mechanisms
        /// 
        /// 
        /// </summary>
        public static readonly Numeric RPL_SASLMECHS = new Numeric("RPL_SASLMECHS", 908);

        /// <summary>
        /// <channel> <message> :Your message contained a censored word, and was blocked
        /// 
        /// </summary>
        public static readonly Numeric ERR_WORDFILTERED = new Numeric("ERR_WORDFILTERED", 936);

        /// <summary>
        /// <client> <nick> :Nickname now unlocked.
        /// <client> <nick> :This user's nickname is not locked.
        /// 
        /// Used by InspIRCd's m_nicklock module.
        /// Used by InspIRCd's m_nicklock module.
        /// 
        /// </summary>
        public static readonly Numeric No_Name_Supplied = new Numeric("No_Name_Supplied", 946, 945);

        /// <summary>
        /// <client> <command> :<info>
        /// 
        /// Indicates that a command could not be performed for an arbitrary reason. For example, a halfop trying to kick an op.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_CANNOTDOCOMMAND = new Numeric("ERR_CANNOTDOCOMMAND", 972);

        /// <summary>
        /// <modulename> :Failed to unload module: <error>
        /// 
        /// </summary>
        public static readonly Numeric ERR_CANTUNLOADMODULE = new Numeric("ERR_CANTUNLOADMODULE", 972);

        /// <summary>
        /// <modulename> :Module successfully unloaded.
        /// 
        /// </summary>
        public static readonly Numeric RPL_UNLOADEDMODULE = new Numeric("RPL_UNLOADEDMODULE", 973);

        /// <summary>
        /// <channel> <mode> :<info>
        /// 
        /// Indicates that a channel mode could not be changeded for an arbitrary reason. For instance, trying to set OPER_ONLY when you are not an IRC operator.
        /// 
        /// 
        /// </summary>
        public static readonly Numeric ERR_CANNOTCHANGECHANMODE = new Numeric("ERR_CANNOTCHANGECHANMODE", 974);

        /// <summary>
        /// <modulename> :Failed to load module: <error>
        /// 
        /// </summary>
        public static readonly Numeric ERR_CANTLOADMODULE = new Numeric("ERR_CANTLOADMODULE", 974);

        /// <summary>
        /// <modulename> :Module successfully loaded.
        /// 
        /// </summary>
        public static readonly Numeric RPL_LOADEDMODULE = new Numeric("RPL_LOADEDMODULE", 975);

        /// <summary>
        /// Also known as ERR_NUMERICERR (Unreal) or ERR_LAST_ERR_MSG
        /// 
        /// </summary>
        public static readonly Numeric ERR_NUMERIC_ERR = new Numeric("ERR_NUMERIC_ERR", 999);
    }
}
