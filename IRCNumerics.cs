/******************************************************************************
*	File		-	IRCNumerics.cs
*	Author		-	Joey Pollack
*	Date		-	7/24/2017 (m/d/y)
*	Mod Date	-	7/24/2017 (m/d/y)
*	Description	-	Definitions for IRC Numerics
*	                Some numerics have different values on different
*	                Networks. This isn't currently supported. Only the lowest
*	                numeric value is supported, the rest are commented out
*	                and marked as DUPE.
******************************************************************************/

namespace IRCLib
{
    public static class IRCNumerics
    {
        /// <summary>
        /// <client> :Welcome to the Internet Relay Network <nick>!<user>@<host>
        /// The first message sent after client registration. The text used varies widely
        /// </summary>
        public const int RPL_WELCOME = 001;

        /// <summary>
        /// <client> :Your host is <servername>, running version <version>
        /// Part of the post-registration greeting. Text varies widely. Also known as RPL_YOURHOSTIS (InspIRCd)
        /// </summary>
        public const int RPL_YOURHOST = 002;

        /// <summary>
        /// <client> :This server was created <date>
        /// Part of the post-registration greeting. Text varies widely and &lt;date&gt; is returned in a human-readable format. Also known as RPL_SERVERCREATED (InspIRCd)
        /// </summary>
        public const int RPL_CREATED = 003;

        /// <summary>
        /// <client> <server_name> <version> <usermodes> <chanmodes> [chanmodes_with_a_parameter]
        /// Part of the post-registration greeting. Also known as RPL_SERVERVERSION (InspIRCd)
        /// </summary>
        public const int RPL_MYINFO = 004;

        /// <summary>
        /// <client> :Try server <server_name>, port <port_number>
        /// Sent by the server to a user to suggest an alternative server, sometimes used when the connection is refused because the server is already full. Also known as RPL_SLINE (AustHex), and RPL_REDIR
        /// RPL_BOUNCE (010)
        /// </summary>
        public const int RPL_BOUNCE = 005;

        /// <summary>
        /// <client> <1-13 tokens> :are supported by this server
        /// Advertises features, limits, and protocol options that clients should be aware of. Also known as RPL_PROTOCTL (Bahamut, Unreal, Ultimate)
        /// http://modern.ircdocs.horse/#rplisupport-005
        /// RPL_REMOTEISUPPORT (105)
        /// </summary>
        public const int RPL_ISUPPORT = 005;

        /// <summary>
        /// </summary>
        public const int RPL_MAP = 006;

        /// <summary>
        /// Also known as RPL_ENDMAP (InspIRCd)
        /// </summary>
        public const int RPL_MAPEND = 007;

        /// <summary>
        /// Server notice mask (hex). Also known as RPL_SNOMASKIS (InspIRCd)
        /// </summary>
        public const int RPL_SNOMASK = 008;

        /// <summary>
        /// </summary>
        public const int RPL_STATMEMTOT = 009;

        /// <summary>
        /// <client> <hostname> <port> :<info>
        /// Sent to the client to redirect it to another server. Also known as RPL_REDIR
        /// </summary>
        // DUPE:
        //public const int RPL_BOUNCE = 010;

        /// <summary>
        /// </summary>
        public const int RPL_STATMEM = 010;

        /// <summary>
        /// </summary>
        public const int RPL_YOURCOOKIE = 014;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_MAP = 015;

        /// <summary>
        /// </summary>
        public const int RPL_MAPMORE = 016;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_MAPEND = 017;

        /// <summary>
        /// <client> :<info>
        /// Used by Rusnet to send the initial "Please wait while we process your connection" message, rather than a server-sent NOTICE.
        /// </summary>
        public const int RPL_HELLO = 020;

        /// <summary>
        /// </summary>
        public const int RPL_APASSWARN_SET = 030;

        /// <summary>
        /// </summary>
        public const int RPL_APASSWARN_SECRET = 031;

        /// <summary>
        /// </summary>
        public const int RPL_APASSWARN_CLEAR = 032;

        /// <summary>
        /// Also known as RPL_YOURUUID (InspIRCd)
        /// </summary>
        public const int RPL_YOURID = 042;

        /// <summary>
        /// <client> <newnick> :<info>
        /// Sent to the client when their nickname was forced to change due to a collision
        /// </summary>
        public const int RPL_SAVENICK = 043;

        /// <summary>
        /// </summary>
        public const int RPL_ATTEMPTINGJUNC = 050;

        /// <summary>
        /// </summary>
        public const int RPL_ATTEMPTINGREROUTE = 051;

        /// <summary>
        /// Same format as RPL_ISUPPORT, but returned when the client is requesting information from a remote server instead of the server it is currently connected to
        /// http://www.irc.org/tech_docs/005.html
        /// RPL_ISUPPORT (005)
        /// </summary>
        public const int RPL_REMOTEISUPPORT = 105;

        /// <summary>
        /// <client> Link <version>[.<debug_level>] <destination> <next_server> [V<protocol_version> <link_uptime_in_seconds> <backstream_sendq> <upstream_sendq>]
        /// See RFC/// </summary>
        public const int RPL_TRACELINK = 200;

        /// <summary>
        /// <client> Try. <class> <server>
        /// See RFC/// </summary>
        public const int RPL_TRACECONNECTING = 201;

        /// <summary>
        /// <client> H.S. <class> <server>
        /// See RFC/// </summary>
        public const int RPL_TRACEHANDSHAKE = 202;

        /// <summary>
        /// <client> ???? <class> [<connection_address>]
        /// See RFC/// </summary>
        public const int RPL_TRACEUNKNOWN = 203;

        /// <summary>
        /// <client> Oper <class> <nick>
        /// See RFC/// </summary>
        public const int RPL_TRACEOPERATOR = 204;

        /// <summary>
        /// <client> User <class> <nick>
        /// See RFC/// </summary>
        public const int RPL_TRACEUSER = 205;

        /// <summary>
        /// <client> Serv <class> <int>S <int>C <server> <nick!user|*!*>@<host|server> [V<protocol_version>]
        /// See RFC/// </summary>
        public const int RPL_TRACESERVER = 206;

        /// <summary>
        /// <client> Service <class> <name> <type> <active_type>
        /// See RFC/// </summary>
        public const int RPL_TRACESERVICE = 207;

        /// <summary>
        /// <client> <newtype> 0 <client_name>
        /// See RFC/// </summary>
        public const int RPL_TRACENEWTYPE = 208;

        /// <summary>
        /// <client> Class <class> <count>
        /// See RFC/// </summary>
        public const int RPL_TRACECLASS = 209;

        /// <summary>
        /// </summary>
        public const int RPL_TRACERECONNECT = 210;

        /// <summary>
        /// Used instead of having multiple stats numerics/// </summary>
        public const int RPL_STATS = 210;

        /// <summary>
        /// Used to send lists of stats flags and other help information./// </summary>
        public const int RPL_STATSHELP = 210;

        /// <summary>
        /// <client> <linkname> <sendq> <sent_msgs> <sent_bytes> <recvd_msgs> <rcvd_bytes> <time_open>
        /// Reply to STATS (See RFC)/// </summary>
        public const int RPL_STATSLINKINFO = 211;

        /// <summary>
        /// <client> <command> <count> [<byte_count> <remote_count>]
        /// Reply to STATS (See RFC)/// </summary>
        public const int RPL_STATSCOMMANDS = 212;

        /// <summary>
        /// <client> C <host> * <name> <port> <class>
        /// Reply to STATS (See RFC)/// </summary>
        public const int RPL_STATSCLINE = 213;

        /// <summary>
        /// <client> N <host> * <name> <port> <class>
        /// Reply to STATS (See RFC), Also known as RPL_STATSOLDNLINE (ircu, Unreal)
        /// </summary>
        public const int RPL_STATSNLINE = 214;

        /// <summary>
        /// <client> I <host> * <host> <port> <class>
        /// Reply to STATS (See RFC)/// </summary>
        public const int RPL_STATSILINE = 215;

        /// <summary>
        /// <client> K <host> * <username> <port> <class>
        /// Reply to STATS (See RFC)
        /// </summary>
        public const int RPL_STATSKLINE = 216;

        /// <summary>
        /// </summary>
        public const int RPL_STATSQLINE = 217;

        /// <summary>
        /// </summary>
        public const int RPL_STATSPLINE = 217;

        /// <summary>
        /// <client> Y <class> <ping_freq> <connect_freq> <max_sendq>
        /// Reply to STATS (See RFC)/// </summary>
        public const int RPL_STATSYLINE = 218;

        /// <summary>
        /// <client> <query> :<info>
        /// End of RPL_STATS* list./// </summary>
        public const int RPL_ENDOFSTATS = 219;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_STATSPLINE = 220;

        /// <summary>
        /// </summary>
        public const int RPL_STATSBLINE = 220;

        /// <summary>
        /// </summary>
        public const int RPL_STATSWLINE = 220;

        /// <summary>
        /// <client> <user_modes> [<user_mode_params>]
        /// Information about a user's own modes. Some daemons have extended the mode command and certain modes take parameters (like channel modes).
        /// </summary>
        public const int RPL_UMODEIS = 221;

        /// <summary>
        /// </summary>
        public const int RPL_MODLIST = 222;

        /// <summary>
        /// </summary>
        public const int RPL_SQLINE_NICK = 222;

        /// <summary>
        /// </summary>
         // DUPE:
        //public const int RPL_STATSBLINE = 222;

        /// <summary>
        /// </summary>
        public const int RPL_STATSJLINE = 222;

        /// <summary>
        /// </summary>
        public const int RPL_CODEPAGE = 222;

        /// <summary>
        /// </summary>
        public const int RPL_STATSELINE = 223;

        /// <summary>
        /// </summary>
        public const int RPL_STATSGLINE = 223;

        /// <summary>
        /// </summary>
        public const int RPL_CHARSET = 223;

        /// <summary>
        /// </summary>
        public const int RPL_STATSFLINE = 224;

        /// <summary>
        /// </summary>
        public const int RPL_STATSTLINE = 224;

        /// <summary>
        /// </summary>
        public const int RPL_STATSDLINE = 225;

        /// <summary>
        /// </summary>
        public const int RPL_STATSCLONE = 225;

        /// <summary>
        /// </summary>
        public const int RPL_STATSZLINE = 225;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_STATSELINE = 225;

        /// <summary>
        /// </summary>
        public const int RPL_STATSCOUNT = 226;

        /// <summary>
        /// </summary>
        public const int RPL_STATSALINE = 226;

        /// <summary>
        /// </summary>
         // DUPE:
        //public const int RPL_STATSNLINE = 226;

        /// <summary>
        /// </summary>
         // DUPE:
        //public const int RPL_STATSGLINE = 227;

        /// <summary>
        /// </summary>
        public const int RPL_STATSVLINE = 227;

        /// <summary>
        /// Returns details about active DNS blacklists and hits.
        /// </summary>
         // DUPE:
        //public const int RPL_STATSBLINE = 227;

        /// <summary>
        /// </summary>
         // DUPE:
        //public const int RPL_STATSQLINE = 228;

        /// <summary>
        /// </summary>
        public const int RPL_STATSBANVER = 228;

        /// <summary>
        /// </summary>
        public const int RPL_STATSSPAMF = 229;

        /// <summary>
        /// </summary>
        public const int RPL_STATSEXCEPTTKL = 230;

        /// <summary>
        /// </summary>
        public const int RPL_SERVICEINFO = 231;

        /// <summary>
        /// </summary>
        public const int RPL_ENDOFSERVICES = 232;

        /// <summary>
        /// </summary>
        public const int RPL_RULES = 232;

        /// <summary>
        /// </summary>
        public const int RPL_SERVICE = 233;

        /// <summary>
        /// <client> <name> <server> <mask> <type> <hopcount> <info>
        /// A service entry in the service list/// </summary>
        public const int RPL_SERVLIST = 234;

        /// <summary>
        /// <client> <mask> <type> :<info>
        /// Termination of an RPL_SERVLIST list/// </summary>
        public const int RPL_SERVLISTEND = 235;

        /// <summary>
        /// Verbose server list?/// </summary>
        public const int RPL_STATSVERBOSE = 236;

        /// <summary>
        /// Engine name?/// </summary>
        public const int RPL_STATSENGINE = 237;

        /// <summary>
        /// Feature lines?/// </summary>
        // DUPE:
        // public const int RPL_STATSFLINE = 238;

        /// <summary>
        /// </summary>
        public const int RPL_STATSIAUTH = 239;

        /// <summary>
        /// </summary>
         // DUPE:
        //public const int RPL_STATSVLINE = 240;

        /// <summary>
        /// </summary>
        public const int RPL_STATSXLINE = 240;

        /// <summary>
        /// <client> L <hostmask> * <servername> <maxdepth>
        /// Reply to STATS (See RFC)/// </summary>
        public const int RPL_STATSLLINE = 241;

        /// <summary>
        /// <client> :Server Up <days> days <hours>:<minutes>:<seconds>
        /// Reply to STATS (See RFC)/// </summary>
        public const int RPL_STATSUPTIME = 242;

        /// <summary>
        /// <client> O <hostmask> * <nick> [:<info>]
        /// Reply to STATS (See RFC); The info field is an extension found in some IRC daemons, which returns info such as an e-mail address or the name/job of an operator
        /// </summary>
        public const int RPL_STATSOLINE = 243;

        /// <summary>
        /// <client> H <hostmask> * <servername>
        /// Reply to STATS (See RFC)/// </summary>
        public const int RPL_STATSHLINE = 244;

        /// <summary>
        /// </summary>
        public const int RPL_STATSSLINE = 245;

        /// <summary>
        /// </summary>
         // DUPE:
        //public const int RPL_STATSTLINE = 245;

        /// <summary>
        /// </summary>
        public const int RPL_STATSPING = 246;

        /// <summary>
        /// </summary>
        public const int RPL_STATSSERVICE = 246;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_STATSTLINE = 246;

        /// <summary>
        /// </summary>
        public const int RPL_STATSULINE = 246;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_STATSBLINE = 247;

        /// <summary>
        /// </summary>
         // DUPE:
        //public const int RPL_STATSXLINE = 247;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_STATSGLINE = 247;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_STATSULINE = 248;

        /// <summary>
        /// </summary>
        public const int RPL_STATSDEFINE = 248;

        /// <summary>
        /// Extension to RFC1459?/// </summary>
         // DUPE:
        //public const int RPL_STATSULINE = 249;

        /// <summary>
        /// </summary>
        public const int RPL_STATSDEBUG = 249;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_STATSDLINE = 250;

        /// <summary>
        /// </summary>
        public const int RPL_STATSCONN = 250;

        /// <summary>
        /// <client> :There are <int> users and <int> invisible on <int> servers
        /// Reply to LUSERS command, other versions exist (eg. RFC2812); Text may vary.
        /// </summary>
        public const int RPL_LUSERCLIENT = 251;

        /// <summary>
        /// <client> <int> :operator(s) online
        /// Reply to LUSERS command - Number of IRC operators online/// </summary>
        public const int RPL_LUSEROP = 252;

        /// <summary>
        /// <client> <int> :unknown connection(s)
        /// Reply to LUSERS command - Number of connections in an unknown/unregistered state
        /// </summary>
        public const int RPL_LUSERUNKNOWN = 253;

        /// <summary>
        /// <client> <int> :channels formed
        /// Reply to LUSERS command - Number of channels formed/// </summary>
        public const int RPL_LUSERCHANNELS = 254;

        /// <summary>
        /// <client> :I have <int> clients and <int> servers
        /// Reply to LUSERS command - Information about local connections; Text may vary.
        /// </summary>
        public const int RPL_LUSERME = 255;

        /// <summary>
        /// <client> <server> :Administrative info
        /// Start of an RPL_ADMIN* reply. In practise, the server parameter is often never given, and instead the last param contains the text 'Administrative info about <server>'. Newer daemons seem to follow the RFC and output the server's hostname in the last parameter, but also output the server name in the text as per traditional daemons.
        /// </summary>
        public const int RPL_ADMINME = 256;

        /// <summary>
        /// <client> :<admin_location>
        /// Reply to ADMIN command (Location, first line)/// </summary>
        public const int RPL_ADMINLOC1 = 257;

        /// <summary>
        /// <client> :<admin_location>
        /// Reply to ADMIN command (Location, second line)/// </summary>
        public const int RPL_ADMINLOC2 = 258;

        /// <summary>
        /// <client> :<email_address>
        /// Reply to ADMIN command (E-mail address of administrator)/// </summary>
        public const int RPL_ADMINEMAIL = 259;

        /// <summary>
        /// <client> File <logfile> <debug_level>
        /// See RFC/// </summary>
        public const int RPL_TRACELOG = 261;

        /// <summary>
        /// Extension to RFC1459?/// </summary>
        public const int RPL_TRACEPING = 262;

        /// <summary>
        /// <client> <server_name> <version>[.<debug_level>] :<info>
        /// Used to terminate a list of RPL_TRACE* replies. Also known as RPL_ENDOFTRACE
        /// </summary>
        public const int RPL_TRACEEND = 262;

        /// <summary>
        /// <client> <command> :Please wait a while and try again.
        /// When a server drops a command without processing it, it MUST use this reply. The last param text changes, and commonly provides the client with more information about why the command could not be processed (such as rate-limiting). Also known as RPL_LOAD_THROTTLED and RPL_LOAD2HI, I'm presuming they do the same thing.
        /// </summary>
        public const int RPL_TRYAGAIN = 263;

        /// <summary>
        /// </summary>
        public const int RPL_USINGSSL = 264;

        /// <summary>
        /// <client> [<u> <m>] :Current local users <u>, max <m>
        /// Returns the number of clients currently and the maximum number of clients that have been connected directly to this server at one time, respectively. The two optional parameters are not always provided. Also known as RPL_CURRENT_LOCAL
        /// </summary>
        public const int RPL_LOCALUSERS = 265;

        /// <summary>
        /// <client> [<u> <m>] :Current global users <u>, max <m>
        /// Returns the number of clients currently connected to the network, and the maximum number of clients ever connected to the network at one time, respectively. Also known as RPL_CURRENT_GLOBAL
        /// </summary>
        public const int RPL_GLOBALUSERS = 266;

        /// <summary>
        /// </summary>
        public const int RPL_START_NETSTAT = 267;

        /// <summary>
        /// </summary>
        public const int RPL_NETSTAT = 268;

        /// <summary>
        /// </summary>
        public const int RPL_END_NETSTAT = 269;

        /// <summary>
        /// </summary>
        public const int RPL_PRIVS = 270;

        /// <summary>
        /// <client> :<count> servers and <count> users, average <average count> users per server
        /// </summary>
        public const int RPL_MAPUSERS = 270;

        /// <summary>
        /// </summary>
        public const int RPL_SILELIST = 271;

        /// <summary>
        /// </summary>
        public const int RPL_ENDOFSILELIST = 272;

        /// <summary>
        /// </summary>
        public const int RPL_NOTIFY = 273;

        /// <summary>
        /// </summary>
        public const int RPL_ENDNOTIFY = 274;

        /// <summary>
        /// </summary>
        public const int RPL_STATSDELTA = 274;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_STATSDLINE = 275;

        /// <summary>
        /// <client> <nick> :is using a secure connection (SSL)
        /// </summary>
        // DUPE:
        // public const int RPL_USINGSSL = 275;

        /// <summary>
        /// <nick> :has client certificate fingerprint <fingerprint>
        /// Shows the SSL/TLS certificate fingerprint used by the client with the given nickname. Only sent when users <code>WHOIS</code> themselves or when an operator sends the <code>WHOIS</code>. Also adopted by hybrid 8.1 and charybdis 3.2
        /// </summary>
        public const int RPL_WHOISCERTFP = 276;

        /// <summary>
        /// </summary>
        public const int RPL_STATSRLINE = 276;

        /// <summary>
        /// Gone from hybrid 7.1 (2003)/// </summary>
        public const int RPL_VCHANEXIST = 276;

        /// <summary>
        /// Gone from hybrid 7.1 (2003)/// </summary>
        public const int RPL_VCHANLIST = 277;

        /// <summary>
        /// Gone from hybrid 7.1 (2003)/// </summary>
        public const int RPL_VCHANHELP = 278;

        /// <summary>
        /// </summary>
        public const int RPL_GLIST = 280;

        /// <summary>
        /// </summary>
        public const int RPL_ENDOFGLIST = 281;

        /// <summary>
        /// </summary>
        public const int RPL_ACCEPTLIST = 281;

        /// <summary>
        /// </summary>
        public const int RPL_ENDOFACCEPT = 282;

        /// <summary>
        /// </summary>
        public const int RPL_JUPELIST = 282;

        /// <summary>
        /// </summary>
        public const int RPL_ALIST = 283;

        /// <summary>
        /// </summary>
        public const int RPL_ENDOFJUPELIST = 283;

        /// <summary>
        /// </summary>
        public const int RPL_ENDOFALIST = 284;

        /// <summary>
        /// </summary>
        public const int RPL_FEATURE = 284;

        /// <summary>
        /// </summary>
        public const int RPL_GLIST_HASH = 285;

        /// <summary>
        /// </summary>
        public const int RPL_CHANINFO_HANDLE = 285;

        /// <summary>
        /// </summary>
        public const int RPL_NEWHOSTIS = 285;

        /// <summary>
        /// </summary>
        public const int RPL_CHANINFO_USERS = 286;

        /// <summary>
        /// </summary>
        public const int RPL_CHKHEAD = 286;

        /// <summary>
        /// </summary>
        public const int RPL_CHANINFO_CHOPS = 287;

        /// <summary>
        /// </summary>
        public const int RPL_CHANUSER = 287;

        /// <summary>
        /// </summary>
        public const int RPL_CHANINFO_VOICES = 288;

        /// <summary>
        /// </summary>
        public const int RPL_PATCHHEAD = 288;

        /// <summary>
        /// </summary>
        public const int RPL_CHANINFO_AWAY = 289;

        /// <summary>
        /// </summary>
        public const int RPL_PATCHCON = 289;

        /// <summary>
        /// </summary>
        public const int RPL_CHANINFO_OPERS = 290;

        /// <summary>
        /// </summary>
        public const int RPL_HELPHDR = 290;

        /// <summary>
        /// </summary>
        public const int RPL_DATASTR = 290;

        /// <summary>
        /// </summary>
        public const int RPL_CHANINFO_BANNED = 291;

        /// <summary>
        /// </summary>
        public const int RPL_HELPOP = 291;

        /// <summary>
        /// </summary>
        public const int RPL_ENDOFCHECK = 291;

        /// <summary>
        /// </summary>
        public const int RPL_CHANINFO_BANS = 292;

        /// <summary>
        /// </summary>
        public const int RPL_HELPTLR = 292;

        /// <summary>
        /// </summary>
        public const int ERR_SEARCHNOMATCH = 292;

        /// <summary>
        /// </summary>
        public const int RPL_CHANINFO_INVITE = 293;

        /// <summary>
        /// </summary>
        public const int RPL_HELPHLP = 293;

        /// <summary>
        /// </summary>
        public const int RPL_CHANINFO_INVITES = 294;

        /// <summary>
        /// </summary>
        public const int RPL_HELPFWD = 294;

        /// <summary>
        /// </summary>
        public const int RPL_CHANINFO_KICK = 295;

        /// <summary>
        /// </summary>
        public const int RPL_HELPIGN = 295;

        /// <summary>
        /// </summary>
        public const int RPL_CHANINFO_KICKS = 296;

        /// <summary>
        /// </summary>
        public const int RPL_END_CHANINFO = 299;

        /// <summary>
        /// Dummy reply, supposedly only used for debugging/testing new features, however has appeared in production daemons.
        /// </summary>
        public const int RPL_NONE = 300;

        /// <summary>
        /// <client> <nick> :<message>
        /// Used in reply to a command directed at a user who is marked as away
        /// </summary>
        public const int RPL_AWAY = 301;

        /// <summary>
        /// <client> :*1<reply> *( ' ' <reply> )
        /// Reply used by USERHOST (see RFC)/// </summary>
        public const int RPL_USERHOST = 302;

        /// <summary>
        /// <client> :*1<nick> *( ' ' <nick> )
        /// Reply to the ISON command (see RFC)/// </summary>
        public const int RPL_ISON = 303;

        /// <summary>
        /// <client> :<text>
        /// Displays text to the user. This seems to have been defined in irc2.7h but never used. Servers generally use specific numerics or server notices instead of this. Unreal uses this numeric, but most others don't use it
        /// </summary>
        public const int RPL_TEXT = 304;

        /// <summary>
        /// Defined with the comment <code>// insp-specific</code>/// </summary>
        public const int RPL_SYNTAX = 304;

        /// <summary>
        /// <client> :<info>
        /// Reply from AWAY when no longer marked as away/// </summary>
        public const int RPL_UNAWAY = 305;

        /// <summary>
        /// <client> :<info>
        /// Reply from AWAY when marked away/// </summary>
        public const int RPL_NOWAWAY = 306;

        /// <summary>
        /// </summary>
        public const int RPL_USERIP = 307;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISREGNICK = 307;

        /// <summary>
        /// </summary>
        public const int RPL_SUSERHOST = 307;

        /// <summary>
        /// </summary>
        public const int RPL_NOTIFYACTION = 308;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISADMIN = 308;

        /// <summary>
        /// Also known as RPL_RULESTART (InspIRCd)/// </summary>
        public const int RPL_RULESSTART = 308;

        /// <summary>
        /// </summary>
        public const int RPL_NICKTRACE = 309;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISSADMIN = 309;

        /// <summary>
        /// Also known as RPL_RULESEND (InspIRCd)/// </summary>
        public const int RPL_ENDOFRULES = 309;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISHELPER = 309;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISSVCMSG = 310;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISHELPOP = 310;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISSERVICE = 310;

        /// <summary>
        /// <client> <nick> <user> <host> * :<real_name>
        /// Reply to WHOIS - Information about the user/// </summary>
        public const int RPL_WHOISUSER = 311;

        /// <summary>
        /// <client> <nick> <server> :<server_info>
        /// Reply to WHOIS - What server they're on/// </summary>
        public const int RPL_WHOISSERVER = 312;

        /// <summary>
        /// <client> <nick> :<privileges>
        /// Reply to WHOIS - User has IRC Operator privileges/// </summary>
        public const int RPL_WHOISOPERATOR = 313;

        /// <summary>
        /// <client> <nick> <user> <host> * :<real_name>
        /// Reply to WHOWAS - Information about the user/// </summary>
        public const int RPL_WHOWASUSER = 314;

        /// <summary>
        /// <client> <name> :<info>
        /// Used to terminate a list of RPL_WHOREPLY replies/// </summary>
        public const int RPL_ENDOFWHO = 315;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISPRIVDEAF = 316;

        /// <summary>
        /// This numeric was reserved, but never actually used. The source code notes "redundant and not needed but reserved"
        /// </summary>
        public const int RPL_WHOISCHANOP = 316;

        /// <summary>
        /// <client> <nick> <seconds> :seconds idle
        /// Reply to WHOIS - Idle information/// </summary>
        public const int RPL_WHOISIDLE = 317;

        /// <summary>
        /// <client> <nick> :<info>
        /// Reply to WHOIS - End of list/// </summary>
        public const int RPL_ENDOFWHOIS = 318;

        /// <summary>
        /// <client> <nick> :*( ( '@' / '+' ) <channel> ' ' )
        /// Reply to WHOIS - Channel list for user (See RFC)/// </summary>
        public const int RPL_WHOISCHANNELS = 319;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISVIRT = 320;

        /// <summary>
        /// </summary>
        public const int RPL_WHOIS_HIDDEN = 320;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISSPECIAL = 320;

        /// <summary>
        /// <client> Channels :Users  Name
        /// Channel list - Header/// </summary>
        public const int RPL_LISTSTART = 321;

        /// <summary>
        /// <client> <channel> <#_visible> :<topic>
        /// Channel list - A channel/// </summary>
        public const int RPL_LIST = 322;

        /// <summary>
        /// <client> :<info>
        /// Channel list - End of list/// </summary>
        public const int RPL_LISTEND = 323;

        /// <summary>
        /// <client> <channel> <mode> <mode_params>
        /// </summary>
        public const int RPL_CHANNELMODEIS = 324;

        /// <summary>
        /// <client> <channel> <nickname>
        /// </summary>
        public const int RPL_UNIQOPIS = 325;

        /// <summary>
        /// </summary>
        public const int RPL_CHANNELPASSIS = 325;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISWEBIRC = 325;

        /// <summary>
        /// <client> <nick> <channel> <modeletters> :is the current channel mode-lock
        /// Defined in header file in charybdis, but never used. Also known as RPL_CHANNELMLOCK.
        /// </summary>
        public const int RPL_CHANNELMLOCKIS = 325;

        /// <summary>
        /// </summary>
        public const int RPL_NOCHANPASS = 326;

        /// <summary>
        /// </summary>
        public const int RPL_CHPASSUNKNOWN = 327;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISHOST = 327;

        /// <summary>
        /// Also known as RPL_CHANNELURL in charybdis/// </summary>
        public const int RPL_CHANNEL_URL = 328;

        /// <summary>
        /// Also known as RPL_CHANNELCREATED (InspIRCd)
        /// </summary>
        public const int RPL_CREATIONTIME = 329;

        /// <summary>
        /// </summary>
        public const int RPL_WHOWAS_TIME = 330;

        /// <summary>
        /// <client> <nick> <authname> :<info>
        /// Also known as RPL_WHOISLOGGEDIN (ratbox?, charybdis)/// </summary>
        public const int RPL_WHOISACCOUNT = 330;

        /// <summary>
        /// <client> <channel> :<info>
        /// Response to TOPIC when no topic is set. Also known as RPL_NOTOPICSET (InspIRCd)
        /// </summary>
        public const int RPL_NOTOPIC = 331;

        /// <summary>
        /// <client> <channel> :<topic>
        /// Response to TOPIC with the set topic. Also known as RPL_TOPICSET (InspIRCd)
        /// </summary>
        public const int RPL_TOPIC = 332;

        /// <summary>
        /// Also known as RPL_TOPICTIME (InspIRCd).
        /// </summary>
        public const int RPL_TOPICWHOTIME = 333;

        /// <summary>
        /// </summary>
        public const int RPL_LISTUSAGE = 334;

        /// <summary>
        /// </summary>
        public const int RPL_COMMANDSYNTAX = 334;

        /// <summary>
        /// </summary>
        public const int RPL_LISTSYNTAX = 334;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISBOT = 335;

        /// <summary>
        /// Since hybrid 8.2.0/// </summary>
        public const int RPL_WHOISTEXT = 335;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISACCOUNTONLY = 335;

        /// <summary>
        /// <client> :<channel>
        /// Since hybrid 8.2.0. Not to be confused with the more widely used 346.
        /// A "list of channels a client is invited to" sent with /INVITE
        /// </summary>
        public const int RPL_INVITELIST = 336;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_WHOISBOT = 336;

        /// <summary>
        /// <client> :End of /INVITE list.
        /// Since hybrid 8.2.0. Not to be confused with the more widely used 347.
        /// </summary>
        public const int RPL_ENDOFINVITELIST = 337;

        /// <summary>
        /// Before hybrid 8.2.0, for "User connected using a webirc gateway". Since charybdis 3.4.0 for "Underlying IPv4 is %s".
        /// </summary>
        // DUPE:
        // public const int RPL_WHOISTEXT = 337;

        /// <summary>
        /// </summary>
        public const int RPL_CHANPASSOK = 338;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISACTUALLY = 338;

        /// <summary>
        /// </summary>
        public const int RPL_BADCHANPASS = 339;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISMARKS = 339;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_USERIP = 340;

        /// <summary>
        /// <client> <nick> <channel>
        /// Returned by the server to indicate that the attempted INVITE message was successful and is being passed onto the end client. Note that RFC1459 documents the parameters in the reverse order. The format given here is the format used on production servers, and should be considered the standard reply above that given by RFC1459.
        /// </summary>
        public const int RPL_INVITING = 341;

        /// <summary>
        /// <client> <user> :<info>
        /// Returned by a server answering a SUMMON message to indicate that it is summoning that user
        /// </summary>
        public const int RPL_SUMMONING = 342;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISKILL = 343;

        /// <summary>
        /// <client> <channel> <user being invited> <user issuing invite> :<user being invited> has been invited by <user issuing invite>
        /// Sent to users on a channel when an INVITE command has been issued. Also known as RPL_ISSUEDINVITE (ircu)
        /// </summary>
        public const int RPL_INVITED = 345;

        /// <summary>
        /// <client> <channel> <invitemask>
        /// An invite mask for the invite mask list. Also known as RPL_INVEXLIST in hybrid 8.2.0
        /// </summary>
        // DUPE:
        // public const int RPL_INVITELIST = 346;

        /// <summary>
        /// <client> <channel> :<info>
        /// Termination of an RPL_INVITELIST list. Also known as RPL_ENDOFINVEXLIST in hybrid 8.2.0
        /// </summary>
        // DUPE:
        // public const int RPL_ENDOFINVITELIST = 347;

        /// <summary>
        /// <client> <channel> <exceptionmask> [<who> <set-ts>]
        /// An exception mask for the exception mask list. Also known as RPL_EXLIST (Unreal, Ultimate). Bahamut calls this RPL_EXEMPTLIST and adds the last two optional params, <who> being either the nickmask of the client that set the exception or the server name, and <set-ts> being a unix timestamp representing when it was set.
        /// </summary>
        public const int RPL_EXCEPTLIST = 348;

        /// <summary>
        /// <client> <channel> :<info>
        /// Termination of an RPL_EXCEPTLIST list. Also known as RPL_ENDOFEXLIST (Unreal, Ultimate) or RPL_ENDOFEXEMPTLIST (Bahamut).
        /// </summary>
        public const int RPL_ENDOFEXCEPTLIST = 349;

        /// <summary>
        /// <client> <version>[.<debuglevel>] <server> :<comments>
        /// Reply by the server showing its version details, however this format is not often adhered to
        /// </summary>
        public const int RPL_VERSION = 351;

        /// <summary>
        /// <client> <channel> <user> <host> <server> <nick> <H|G>[*][@|+] :<hopcount> <real_name>
        /// Reply to vanilla WHO (See RFC). This format can be very different if the 'WHOX' version of the command is used (see ircu).
        /// </summary>
        public const int RPL_WHOREPLY = 352;

        /// <summary>
        /// <client> ( '=' / '*' / '@' ) <channel> ' ' : [ '@' / '+' ] <nick> *( ' ' [ '@' / '+' ] <nick> )
        /// Reply to NAMES (See RFC)/// </summary>
        public const int RPL_NAMREPLY = 353;

        /// <summary>
        /// Reply to WHO, however it is a 'special' reply because it is returned using a non-standard (non-RFC1459) format. The format is dictated by the command given by the user, and can vary widely. When this is used, the WHO command was invoked in its 'extended' form, as announced by the 'WHOX' ISUPPORT tag. Also known as RPL_RWHOREPLY (Bahamut).
        /// </summary>
        public const int RPL_WHOSPCRPL = 354;

        /// <summary>
        /// <client> ( '=' / '*' / '@' ) <channel> ' ' : [ '@' / '+' ] <nick> *( ' ' [ '@' / '+' ] <nick> )
        /// Reply to the \users (when the channel is set +D, QuakeNet relative). The proper define name for this numeric is unknown at this time. Also known as RPL_DELNAMREPLY (ircu)
        /// RPL_NAMREPLY (353)
        /// </summary>
        public const int RPL_NAMREPLY_ = 355;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_MAP = 357;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_MAPMORE = 358;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_MAPEND = 359;

        /// <summary>
        /// Defined in header file, but never used. Initially introduced in charybdis 2.1 behind <code>#if 0</code>, with the other side using RPL_WHOISACTUALLY
        /// </summary>
        public const int RPL_WHOWASREAL = 360;

        /// <summary>
        /// </summary>
        public const int RPL_KILLDONE = 361;

        /// <summary>
        /// </summary>
        public const int RPL_CLOSING = 362;

        /// <summary>
        /// </summary>
        public const int RPL_CLOSEEND = 363;

        /// <summary>
        /// <client> <mask> <server> :<hopcount> <server_info>
        /// Reply to the LINKS command/// </summary>
        public const int RPL_LINKS = 364;

        /// <summary>
        /// <client> <mask> :<info>
        /// Termination of an RPL_LINKS list/// </summary>
        public const int RPL_ENDOFLINKS = 365;

        /// <summary>
        /// <client> <channel> :<info>
        /// Termination of an RPL_NAMREPLY list/// </summary>
        public const int RPL_ENDOFNAMES = 366;

        /// <summary>
        /// <client> <channel> <banid> [<time_left> :<reason>]
        /// A ban-list item (See RFC); <time left> and <reason> are additions used by various servers.
        /// </summary>
        public const int RPL_BANLIST = 367;

        /// <summary>
        /// <client> <channel> :<info>
        /// Termination of an RPL_BANLIST list/// </summary>
        public const int RPL_ENDOFBANLIST = 368;

        /// <summary>
        /// <client> <nick> :<info>
        /// Reply to WHOWAS - End of list/// </summary>
        public const int RPL_ENDOFWHOWAS = 369;

        /// <summary>
        /// <client> :<string>
        /// Reply to INFO/// </summary>
        public const int RPL_INFO = 371;

        /// <summary>
        /// <client> :- <string>
        /// Reply to MOTD/// </summary>
        public const int RPL_MOTD = 372;

        /// <summary>
        /// </summary>
        public const int RPL_INFOSTART = 373;

        /// <summary>
        /// <client> :<info>
        /// Termination of an RPL_INFO list/// </summary>
        public const int RPL_ENDOFINFO = 374;

        /// <summary>
        /// <client> :- <server> Message of the day -
        /// Start of an RPL_MOTD list/// </summary>
        public const int RPL_MOTDSTART = 375;

        /// <summary>
        /// <client> :<info>
        /// Termination of an RPL_MOTD list/// </summary>
        public const int RPL_ENDOFMOTD = 376;

        /// <summary>
        /// </summary>
        public const int RPL_KICKEXPIRED = 377;

        /// <summary>
        /// <client> :<text>
        /// Used during the connection (after MOTD) to announce the network policy on spam and privacy. Supposedly now obsoleted in favour of using NOTICE.
        /// </summary>
        public const int RPL_SPAM = 377;

        /// <summary>
        /// </summary>
        public const int RPL_BANEXPIRED = 378;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_WHOISHOST = 378;

        /// <summary>
        /// Used by AustHex to 'force' the display of the MOTD, however is considered obsolete due to client/script awareness & ability to display the MOTD regardless.
        /// RPL_MOTD (372)
        /// </summary>
        // DUPE:
        // public const int RPL_MOTD = 378;

        /// <summary>
        /// </summary>
        public const int RPL_KICKLINKED = 379;

        /// <summary>
        /// </summary>
        public const int RPL_WHOISMODES = 379;

        /// <summary>
        /// <nick> :was connecting from <host>
        /// </summary>
        public const int RPL_WHOWASIP = 379;

        /// <summary>
        /// </summary>
        public const int RPL_BANLINKED = 380;

        /// <summary>
        /// </summary>
        public const int RPL_YOURHELPER = 380;

        /// <summary>
        /// <client> :<info>
        /// Successful reply from OPER. Also known asRPL_YOUAREOPER (InspIRCd)
        /// </summary>
        public const int RPL_YOUREOPER = 381;

        /// <summary>
        /// <client> <config_file> :<info>
        /// Successful reply from REHASH/// </summary>
        public const int RPL_REHASHING = 382;

        /// <summary>
        /// <client> :You are service <service_name>
        /// Sent upon successful registration of a service/// </summary>
        public const int RPL_YOURESERVICE = 383;

        /// <summary>
        /// </summary>
        public const int RPL_MYPORTIS = 384;

        /// <summary>
        /// </summary>
        public const int RPL_NOTOPERANYMORE = 385;

        /// <summary>
        /// </summary>
        public const int RPL_QLIST = 386;

        /// <summary>
        /// </summary>
        public const int RPL_IRCOPS = 386;

        /// <summary>
        /// </summary>
        public const int RPL_IRCOPSHEADER = 386;

        /// <summary>
        /// :*
        /// Used by Hybrid's old OpenSSL OPER CHALLENGE response. This has been obsoleted in favour of SSL cert fingerprinting in oper blocks
        /// </summary>
        public const int RPL_RSACHALLENGE = 386;

        /// <summary>
        /// </summary>
        public const int RPL_ENDOFQLIST = 387;

        /// <summary>
        /// </summary>
        public const int RPL_ENDOFIRCOPS = 387;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_IRCOPS = 387;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_ALIST = 388;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_ENDOFIRCOPS = 388;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_ENDOFALIST = 389;

        /// <summary>
        /// <client> <server> :<time string>
        /// Response to the TIME command. The string format may vary greatly.
        /// </summary>
        public const int RPL_TIME = 391;

        /// <summary>
        /// <client> <server> <timestamp> <offset> :<time string>
        /// This extention adds the timestamp and timestamp-offet information for clients.
        /// </summary>
        // DUPE:
        // public const int RPL_TIME = 391;

        /// <summary>
        /// <client> <server> <timezone name> <microseconds> :<time string>
        /// Timezone name is acronym style (eg. 'EST', 'PST' etc). The microseconds field is the number of microseconds since the UNIX epoch, however it is relative to the local timezone of the server. The timezone field is ambiguous, since it only appears to include American zones.
        /// </summary>
        // DUPE:
        // public const int RPL_TIME = 391;

        /// <summary>
        /// <client> <server> <year> <month> <day> <hour> <minute> <second>
        /// Yet another variation, including the time broken down into its components. Time is supposedly relative to UTC.
        /// </summary>
        // DUPE:
        // public const int RPL_TIME = 391;

        /// <summary>
        /// <client> :UserID   Terminal  Host
        /// Start of an RPL_USERS list/// </summary>
        public const int RPL_USERSSTART = 392;

        /// <summary>
        /// <client> :<username> <ttyline> <hostname>
        /// Response to the USERS command (See RFC)/// </summary>
        public const int RPL_USERS = 393;

        /// <summary>
        /// <client> :<info>
        /// Termination of an RPL_USERS list/// </summary>
        public const int RPL_ENDOFUSERS = 394;

        /// <summary>
        /// <client> :<info>
        /// Reply to USERS when nobody is logged in/// </summary>
        public const int RPL_NOUSERS = 395;

        /// <summary>
        /// Reply to a user when user mode +x (host masking) was set successfully
        /// </summary>
        public const int RPL_HOSTHIDDEN = 396;

        /// <summary>
        /// <client> <hostname> :is now your visible host
        /// Also known as RPL_YOURDISPLAYEDHOST (InspIRCd)
        /// </summary>
        public const int RPL_VISIBLEHOST = 396;

        /// <summary>
        /// <client> <command> [<?>] :<info>
        /// Sent when an error occured executing a command, but it is not specifically known why the command could not be executed.
        /// </summary>
        public const int ERR_UNKNOWNERROR = 400;

        /// <summary>
        /// <client> <nick> :<reason>
        /// Used to indicate the nickname parameter supplied to a command is currently unused
        /// </summary>
        public const int ERR_NOSUCHNICK = 401;

        /// <summary>
        /// <client> <server> :<reason>
        /// Used to indicate the server name given currently doesn't exist
        /// </summary>
        public const int ERR_NOSUCHSERVER = 402;

        /// <summary>
        /// <client> <channel> :<reason>
        /// Used to indicate the given channel name is invalid, or does not exist
        /// </summary>
        public const int ERR_NOSUCHCHANNEL = 403;

        /// <summary>
        /// <client> <channel> :<reason>
        /// Sent to a user who does not have the rights to send a message to a channel
        /// </summary>
        public const int ERR_CANNOTSENDTOCHAN = 404;

        /// <summary>
        /// <client> <channel> :<reason>
        /// Sent to a user when they have joined the maximum number of allowed channels and they tried to join another channel
        /// </summary>
        public const int ERR_TOOMANYCHANNELS = 405;

        /// <summary>
        /// <client> <nick> :<reason>
        /// Returned by WHOWAS to indicate there was no history information for a given nickname
        /// </summary>
        public const int ERR_WASNOSUCHNICK = 406;

        /// <summary>
        /// <client> <target> :<reason>
        /// The given target(s) for a command are ambiguous in that they relate to too many targets
        /// </summary>
        public const int ERR_TOOMANYTARGETS = 407;

        /// <summary>
        /// <client> <service_name> :<reason>
        /// Returned to a client which is attempting to send an SQUERY (or other message) to a service which does not exist
        /// </summary>
        public const int ERR_NOSUCHSERVICE = 408;

        /// <summary>
        /// </summary>
        public const int ERR_NOCOLORSONCHAN = 408;

        /// <summary>
        /// <client> <channel> :You cannot use control codes on this channel. Not sent: <text>
        /// </summary>
        public const int ERR_NOCTRLSONCHAN = 408;

        /// <summary>
        /// <client> :<reason>
        /// PING or PONG message missing the originator parameter which is required since these commands must work without valid prefixes
        /// </summary>
        public const int ERR_NOORIGIN = 409;

        /// <summary>
        /// <client> <badcmd> :Invalid CAP subcommand
        /// Returned when a client sends a CAP subcommand which is invalid or otherwise issues an invalid CAP command. Also known as ERR_INVALIDCAPSUBCOMMAND (InspIRCd) or ERR_UNKNOWNCAPCMD (ircu)
        /// http://ircv3.net/specs/core/capability-negotiation-3.1.html
        /// </summary>
        public const int ERR_INVALIDCAPCMD = 410;

        /// <summary>
        /// <client> :<reason>
        /// Returned when no recipient is given with a command/// </summary>
        public const int ERR_NORECIPIENT = 411;

        /// <summary>
        /// <client> :<reason>
        /// Returned when NOTICE/PRIVMSG is used with no message given/// </summary>
        public const int ERR_NOTEXTTOSEND = 412;

        /// <summary>
        /// <client> <mask> :<reason>
        /// Used when a message is being sent to a mask without being limited to a top-level domain (i.e. * instead of *.au)
        /// </summary>
        public const int ERR_NOTOPLEVEL = 413;

        /// <summary>
        /// <client> <mask> :<reason>
        /// Used when a message is being sent to a mask with a wild-card for a top level domain (i.e. *.*)
        /// </summary>
        public const int ERR_WILDTOPLEVEL = 414;

        /// <summary>
        /// <client> <mask> :<reason>
        /// Used when a message is being sent to a mask with an invalid syntax
        /// </summary>
        public const int ERR_BADMASK = 415;

        /// <summary>
        /// <client> <command> [<mask>] :<info>
        /// Returned when too many matches have been found for a command and the output has been truncated. An example would be the WHO command, where by the mask '*' would match everyone on the network! Ouch!
        /// </summary>
        public const int ERR_TOOMANYMATCHES = 416;

        /// <summary>
        /// Same as ERR_TOOMANYMATCHES/// </summary>
        public const int ERR_QUERYTOOLONG = 416;

        /// <summary>
        /// Returned when an input line is longer than the server can process (512 bytes), to let the client know this line was dropped (rather than being truncated)
        /// </summary>
        public const int ERR_INPUTTOOLONG = 417;

        /// <summary>
        /// </summary>
        public const int ERR_LENGTHTRUNCATED = 419;

        /// <summary>
        /// <client> <command> :<reason>
        /// Returned when the given command is unknown to the server (or hidden because of lack of access rights)
        /// </summary>
        public const int ERR_UNKNOWNCOMMAND = 421;

        /// <summary>
        /// <client> :<reason>
        /// Sent when there is no MOTD to send the client/// </summary>
        public const int ERR_NOMOTD = 422;

        /// <summary>
        /// <client> <server> :<reason>
        /// Returned by a server in response to an ADMIN request when no information is available. RFC1459 mentions this in the list of numerics. While it's not listed as a valid reply in section 4.3.7 ('Admin command'), it's confirmed to exist in the real world.
        /// </summary>
        public const int ERR_NOADMININFO = 423;

        /// <summary>
        /// <client> :<reason>
        /// Generic error message used to report a failed file operation during the processing of a command
        /// </summary>
        public const int ERR_FILEERROR = 424;

        /// <summary>
        /// </summary>
        public const int ERR_NOOPERMOTD = 425;

        /// <summary>
        /// </summary>
        public const int ERR_TOOMANYAWAY = 429;

        /// <summary>
        /// Returned by NICK when the user is not allowed to change their nickname due to a channel event (channel mode +E)
        /// </summary>
        public const int ERR_EVENTNICKCHANGE = 430;

        /// <summary>
        /// <client> :<reason>
        /// Returned when a nickname parameter expected for a command isn't found
        /// </summary>
        public const int ERR_NONICKNAMEGIVEN = 431;

        /// <summary>
        /// <client> <nick> :<reason>
        /// Returned after receiving a NICK message which contains a nickname which is considered invalid, such as it's reserved ('anonymous') or contains characters considered invalid for nicknames. This numeric is misspelt, but remains with this name for historical reasons :)
        /// </summary>
        public const int ERR_ERRONEUSNICKNAME = 432;

        /// <summary>
        /// <client> <nick> :<reason>
        /// Returned by the NICK command when the given nickname is already in use
        /// </summary>
        public const int ERR_NICKNAMEINUSE = 433;

        /// <summary>
        /// </summary>
        public const int ERR_SERVICENAMEINUSE = 434;

        /// <summary>
        /// </summary>
        public const int ERR_NORULES = 434;

        /// <summary>
        /// </summary>
        public const int ERR_SERVICECONFUSED = 435;

        /// <summary>
        /// Also known as ERR_BANNICKCHANGE (ratbox, charybdis)/// </summary>
        public const int ERR_BANONCHAN = 435;

        /// <summary>
        /// <nick> :<reason>
        /// Returned by a server to a client when it detects a nickname collision
        /// </summary>
        public const int ERR_NICKCOLLISION = 436;

        /// <summary>
        /// <client> <nick/channel/service> :<reason>
        /// Return when the target is unable to be reached temporarily, eg. a delay mechanism in play, or a service being offline
        /// </summary>
        public const int ERR_UNAVAILRESOURCE = 437;

        /// <summary>
        /// </summary>
        public const int ERR_BANNICKCHANGE = 437;

        /// <summary>
        /// Also known as ERR_NCHANGETOOFAST (Unreal, Ultimate)/// </summary>
        public const int ERR_NICKTOOFAST = 438;

        /// <summary>
        /// </summary>
        public const int ERR_DEAD = 438;

        /// <summary>
        /// Also known as many other things, RPL_INVTOOFAST, RPL_MSGTOOFAST, ERR_TARGETTOFAST (Bahamut), etc
        /// </summary>
        public const int ERR_TARGETTOOFAST = 439;

        /// <summary>
        /// </summary>
        public const int ERR_SERVICESDOWN = 440;

        /// <summary>
        /// <client> <nick> <channel> :<reason>
        /// Returned by the server to indicate that the target user of the command is not on the given channel
        /// </summary>
        public const int ERR_USERNOTINCHANNEL = 441;

        /// <summary>
        /// <client> <channel> :<reason>
        /// Returned by the server whenever a client tries to perform a channel effecting command for which the client is not a member
        /// </summary>
        public const int ERR_NOTONCHANNEL = 442;

        /// <summary>
        /// <client> <nick> <channel> [:<reason>]
        /// Returned when a client tries to invite a user to a channel they're already on
        /// </summary>
        public const int ERR_USERONCHANNEL = 443;

        /// <summary>
        /// <client> <user> :<reason>
        /// Returned by the SUMMON command if a given user was not logged in and could not be summoned
        /// </summary>
        public const int ERR_NOLOGIN = 444;

        /// <summary>
        /// <client> :<reason>
        /// Returned by SUMMON when it has been disabled or not implemented
        /// </summary>
        public const int ERR_SUMMONDISABLED = 445;

        /// <summary>
        /// <client> :<reason>
        /// Returned by USERS when it has been disabled or not implemented
        /// </summary>
        public const int ERR_USERSDISABLED = 446;

        /// <summary>
        /// This numeric is called ERR_CANTCHANGENICK in InspIRCd/// </summary>
        public const int ERR_NONICKCHANGE = 447;

        /// <summary>
        /// <channel> :Channel is forbidden: <reason>
        /// Returned when this channel name has been explicitly blocked and is not allowed to be used.
        /// </summary>
        public const int ERR_FORBIDDENCHANNEL = 448;

        /// <summary>
        /// Unspecified
        /// Returned when a requested feature is not implemented (and cannot be completed)
        /// </summary>
        public const int ERR_NOTIMPLEMENTED = 449;

        /// <summary>
        /// <client> :<reason>
        /// Returned by the server to indicate that the client must be registered before the server will allow it to be parsed in detail
        /// </summary>
        public const int ERR_NOTREGISTERED = 451;

        /// <summary>
        /// </summary>
        public const int ERR_IDCOLLISION = 452;

        /// <summary>
        /// </summary>
        public const int ERR_NICKLOST = 453;

        /// <summary>
        /// </summary>
        public const int ERR_HOSTILENAME = 455;

        /// <summary>
        /// </summary>
        public const int ERR_ACCEPTFULL = 456;

        /// <summary>
        /// </summary>
        public const int ERR_ACCEPTEXIST = 457;

        /// <summary>
        /// </summary>
        public const int ERR_ACCEPTNOT = 458;

        /// <summary>
        /// Not allowed to become an invisible operator?/// </summary>
        public const int ERR_NOHIDING = 459;

        /// <summary>
        /// </summary>
        public const int ERR_NOTFORHALFOPS = 460;

        /// <summary>
        /// <client> <command> :<reason>
        /// Returned by the server by any command which requires more parameters than the number of parameters given
        /// </summary>
        public const int ERR_NEEDMOREPARAMS = 461;

        /// <summary>
        /// <client> :<reason>
        /// Returned by the server to any link which attempts to register again
        /// Also known as ERR_ALREADYREGISTRED(sic) in ratbox/charybdis.
        /// </summary>
        public const int ERR_ALREADYREGISTERED = 462;

        /// <summary>
        /// <client> :<reason>
        /// Returned to a client which attempts to register with a server which has been configured to refuse connections from the client's host
        /// </summary>
        public const int ERR_NOPERMFORHOST = 463;

        /// <summary>
        /// <client> :<reason>
        /// Returned by the PASS command to indicate the given password was required and was either not given or was incorrect
        /// </summary>
        public const int ERR_PASSWDMISMATCH = 464;

        /// <summary>
        /// <client> :<reason>
        /// Returned to a client after an attempt to register on a server configured to ban connections from that client
        /// </summary>
        public const int ERR_YOUREBANNEDCREEP = 465;

        /// <summary>
        /// Sent by a server to a user to inform that access to the server will soon be denied
        /// </summary>
        public const int ERR_YOUWILLBEBANNED = 466;

        /// <summary>
        /// <client> <channel> :<reason>
        /// Returned when the channel key for a channel has already been set
        /// </summary>
        public const int ERR_KEYSET = 467;

        /// <summary>
        /// </summary>
        public const int ERR_INVALIDUSERNAME = 468;

        /// <summary>
        /// </summary>
        public const int ERR_ONLYSERVERSCANCHANGE = 468;

        /// <summary>
        /// </summary>
        public const int ERR_NOCODEPAGE = 468;

        /// <summary>
        /// </summary>
        public const int ERR_LINKSET = 469;

        /// <summary>
        /// </summary>
        public const int ERR_LINKCHANNEL = 470;

        /// <summary>
        /// </summary>
        public const int ERR_KICKEDFROMCHAN = 470;

        /// <summary>
        /// </summary>
        public const int ERR_7BIT = 470;

        /// <summary>
        /// <client> <channel> :<reason>
        /// Returned when attempting to join a channel which is set +l and is already full
        /// </summary>
        public const int ERR_CHANNELISFULL = 471;

        /// <summary>
        /// <client> <char> :<reason>
        /// Returned when a given mode is unknown/// </summary>
        public const int ERR_UNKNOWNMODE = 472;

        /// <summary>
        /// <client> <channel> :<reason>
        /// Returned when attempting to join a channel which is invite only without an invitation
        /// </summary>
        public const int ERR_INVITEONLYCHAN = 473;

        /// <summary>
        /// <client> <channel> :<reason>
        /// Returned when attempting to join a channel a user is banned from
        /// </summary>
        public const int ERR_BANNEDFROMCHAN = 474;

        /// <summary>
        /// <client> <channel> :<reason>
        /// Returned when attempting to join a key-locked channel either without a key or with the wrong key
        /// </summary>
        public const int ERR_BADCHANNELKEY = 475;

        /// <summary>
        /// <client> <channel> :<reason>
        /// The given channel mask was invalid/// </summary>
        public const int ERR_BADCHANMASK = 476;

        /// <summary>
        /// <client> <channel> :<reason>
        /// Returned when attempting to set a mode on a channel which does not support channel modes, or channel mode changes. Also known as ERR_MODELESS
        /// </summary>
        public const int ERR_NOCHANMODES = 477;

        /// <summary>
        /// </summary>
        public const int ERR_NEEDREGGEDNICK = 477;

        /// <summary>
        /// <client> <channel> <char> :<reason>
        /// Returned when a channel access list (i.e. ban list etc) is full and cannot be added to
        /// </summary>
        public const int ERR_BANLISTFULL = 478;

        /// <summary>
        /// <client> <channel> :<reason>
        /// Returned to indicate that a given channel name is not valid. Servers that implement this use it instead of `ERR_NOSUCHCHANNEL` where appropriate.
        /// ERR_NOSUCHCHANNEL (403)
        /// </summary>
        public const int ERR_BADCHANNAME = 479;

        /// <summary>
        /// </summary>
        public const int ERR_LINKFAIL = 479;

        /// <summary>
        /// </summary>
        public const int ERR_NOCOLOR = 479;

        /// <summary>
        /// </summary>
        public const int ERR_NOULINE = 480;

        /// <summary>
        /// </summary>
        public const int ERR_CANNOTKNOCK = 480;

        /// <summary>
        /// <nick> <channel> :Cannot join channel
        /// </summary>
        public const int ERR_THROTTLE = 480;

        /// <summary>
        /// Moved to 489 to match other servers.
        /// ERR_SECUREONLYCHAN (489)
        /// </summary>
        public const int ERR_SSLONLYCHAN = 480;

        /// <summary>
        /// </summary>
        public const int ERR_NOWALLOP = 480;

        /// <summary>
        /// <client> :<reason>
        /// Returned by any command requiring special privileges (eg. IRC operator) to indicate the operation was unsuccessful
        /// </summary>
        public const int ERR_NOPRIVILEGES = 481;

        /// <summary>
        /// <client> <channel> :<reason>
        /// Returned by any command requiring special channel privileges (eg. channel operator) to indicate the operation was unsuccessful. InspIRCd also uses this numeric "for other things like trying to kick a uline"
        /// </summary>
        public const int ERR_CHANOPRIVSNEEDED = 482;

        /// <summary>
        /// <client> :<reason>
        /// Returned by KILL to anyone who tries to kill a server/// </summary>
        public const int ERR_CANTKILLSERVER = 483;

        /// <summary>
        /// <client> :<reason>
        /// Sent by the server to a user upon connection to indicate the restricted nature of the connection (i.e. usermode +r)
        /// </summary>
        public const int ERR_RESTRICTED = 484;

        /// <summary>
        /// </summary>
        public const int ERR_ISCHANSERVICE = 484;

        /// <summary>
        /// </summary>
        public const int ERR_DESYNC = 484;

        /// <summary>
        /// </summary>
        public const int ERR_ATTACKDENY = 484;

        /// <summary>
        /// <client> :<reason>
        /// Any mode requiring 'channel creator' privileges returns this error if the client is attempting to use it while not a channel creator on the given channel
        /// </summary>
        public const int ERR_UNIQOPRIVSNEEDED = 485;

        /// <summary>
        /// </summary>
        public const int ERR_KILLDENY = 485;

        /// <summary>
        /// </summary>
        public const int ERR_CANTKICKADMIN = 485;

        /// <summary>
        /// </summary>
        public const int ERR_ISREALSERVICE = 485;

        /// <summary>
        /// <client> <channel> :Cannot join channel (<reason>)
        /// </summary>
        public const int ERR_CHANBANREASON = 485;

        /// <summary>
        /// Defined in header file, but never used./// </summary>
        public const int ERR_BANNEDNICK = 485;

        /// <summary>
        /// </summary>
        public const int ERR_NONONREG = 486;

        /// <summary>
        /// Unreal 3.2 uses 488 as the ERR_HTMDISABLED numeric instead/// </summary>
        public const int ERR_HTMDISABLED = 486;

        /// <summary>
        /// </summary>
        public const int ERR_ACCOUNTONLY = 486;

        /// <summary>
        /// </summary>
        public const int ERR_RLINED = 486;

        /// <summary>
        /// </summary>
        public const int ERR_CHANTOORECENT = 487;

        /// <summary>
        /// </summary>
        public const int ERR_MSGSERVICES = 487;

        /// <summary>
        /// </summary>
        public const int ERR_NOTFORUSERS = 487;

        /// <summary>
        /// </summary>
        public const int ERR_TSLESSCHAN = 488;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int ERR_HTMDISABLED = 488;

        /// <summary>
        /// <client> <channel> :SSL Only channel (+S), You must connect using SSL to join this channel.
        /// </summary>
        public const int ERR_NOSSL = 488;

        /// <summary>
        /// Also known as ERR_SSLONLYCHAN.
        /// </summary>
        public const int ERR_SECUREONLYCHAN = 489;

        /// <summary>
        /// </summary>
        public const int ERR_VOICENEEDED = 489;

        /// <summary>
        /// <client> <channel> :all members of the channel must be connected via SSL
        /// </summary>
        public const int ERR_ALLMUSTSSL = 490;

        /// <summary>
        /// <client> :<nick> does not accept private messages containing swearing.
        /// </summary>
        public const int ERR_NOSWEAR = 490;

        /// <summary>
        /// <client> :<reason>
        /// Returned by OPER to a client who cannot become an IRC operator because the server has been configured to disallow the client's host
        /// </summary>
        public const int ERR_NOOPERHOST = 491;

        /// <summary>
        /// </summary>
        public const int ERR_NOSERVICEHOST = 492;

        /// <summary>
        /// <client> :You cannot send CTCPs to this channel. Not sent: <message>
        /// Notifies the user that a message they have sent to a channel has been rejected as it contains CTCPs, and they cannot send messages containing CTCPs to this channel. Also known as ERR_NOCTCPALLOWED (InspIRCd).
        /// </summary>
        public const int ERR_NOCTCP = 492;

        /// <summary>
        /// <client> :Cannot send to user <nick> (<reason>)
        /// </summary>
        public const int ERR_CANNOTSENDTOUSER = 492;

        /// <summary>
        /// <client> :You cannot message that person because you do not share a common channel with them.
        /// </summary>
        public const int ERR_NOSHAREDCHAN = 493;

        /// <summary>
        /// </summary>
        public const int ERR_NOFEATURE = 493;

        /// <summary>
        /// </summary>
        public const int ERR_BADFEATVALUE = 494;

        /// <summary>
        /// <client> <nick> :cannot answer you while you are <mode>, your message was not sent
        /// Used for mode +g (CALLERID) in charybdis.
        /// </summary>
        public const int ERR_OWNMODE = 494;

        /// <summary>
        /// </summary>
        public const int ERR_BADLOGTYPE = 495;

        /// <summary>
        /// <channel> :You cannot rejoin this channel yet after being kicked (+J)
        /// This numeric is marked as "we should use 'resource temporarily unavailable' from ircnet/ratbox or whatever"
        /// </summary>
        public const int ERR_DELAYREJOIN = 495;

        /// <summary>
        /// </summary>
        public const int ERR_BADLOGSYS = 496;

        /// <summary>
        /// </summary>
        public const int ERR_BADLOGVALUE = 497;

        /// <summary>
        /// </summary>
        public const int ERR_ISOPERLCHAN = 498;

        /// <summary>
        /// Works just like ERR_CHANOPRIVSNEEDED except it indicates that owner status (+q) is needed.
        /// ERR_CHANOPRIVSNEEDED (482)
        /// </summary>
        public const int ERR_CHANOWNPRIVNEEDED = 499;

        /// <summary>
        /// <client> <string> :Too many join requests. Please wait a while and try again.
        /// </summary>
        public const int ERR_TOOMANYJOINS = 500;

        /// <summary>
        /// </summary>
        public const int ERR_NOREHASHPARAM = 500;

        /// <summary>
        /// <client> :<reason>
        /// Returned by the server to indicate that a MODE message was sent with a nickname parameter and that the mode flag sent was not recognised.
        /// </summary>
        public const int ERR_UMODEUNKNOWNFLAG = 501;

        /// <summary>
        /// <client> <snomask> :is unknown mode char to me
        /// </summary>
        public const int ERR_UNKNOWNSNOMASK = 501;

        /// <summary>
        /// <client> :<reason>
        /// Error sent to any user trying to view or change the user mode for a user other than themselves
        /// </summary>
        public const int ERR_USERSDONTMATCH = 502;

        /// <summary>
        /// <client> :Message could not be delivered to <target>
        /// </summary>
        public const int ERR_GHOSTEDCLIENT = 503;

        /// <summary>
        /// <client> :<warning_text>
        /// Warning about Virtual-World being turned off. Obsoleted in favour for RPL_MODECHANGEWARN
        /// RPL_MODECHANGEWARN (662)
        /// </summary>
        public const int ERR_VWORLDWARN = 503;

        /// <summary>
        /// </summary>
        public const int ERR_USERNOTONSERV = 504;

        /// <summary>
        /// </summary>
        public const int ERR_SILELISTFULL = 511;

        /// <summary>
        /// Also known as ERR_NOTIFYFULL (aircd), I presume they are the same
        /// </summary>
        public const int ERR_TOOMANYWATCH = 512;

        /// <summary>
        /// </summary>
        public const int ERR_NOSUCHGLINE = 512;

        /// <summary>
        /// Also known as ERR_NEEDPONG (Unreal/Ultimate) for use during registration, however it is not used in Unreal (and might not be used in Ultimate either).
        /// Also known as ERR_WRONGPONG(Ratbox/charybdis)
        /// </summary>
        public const int ERR_BADPING = 513;

        /// <summary>
        /// </summary>
        public const int ERR_TOOMANYDCC = 514;

        /// <summary>
        /// </summary>
        public const int ERR_NOSUCHJUPE = 514;

        /// <summary>
        /// </summary>
        public const int ERR_INVALID_ERROR = 514;

        /// <summary>
        /// </summary>
        public const int ERR_BADEXPIRE = 515;

        /// <summary>
        /// </summary>
        public const int ERR_DONTCHEAT = 516;

        /// <summary>
        /// <client> <command> :<info/reason>
        /// </summary>
        public const int ERR_DISABLED = 517;

        /// <summary>
        /// </summary>
        public const int ERR_NOINVITE = 518;

        /// <summary>
        /// </summary>
        public const int ERR_LONGMASK = 518;

        /// <summary>
        /// </summary>
        public const int ERR_ADMONLY = 519;

        /// <summary>
        /// </summary>
        public const int ERR_TOOMANYUSERS = 519;

        /// <summary>
        /// :Cannot join channel (+O)
        /// Also known as ERR_OPERONLYCHAN (Hybrid) and ERR_CANTJOINOPERSONLY (InspIRCd).
        /// </summary>
        public const int ERR_OPERONLY = 520;

        /// <summary>
        /// </summary>
        public const int ERR_MASKTOOWIDE = 520;

        /// <summary>
        /// This is considered obsolete in favour of ERR_TOOMANYMATCHES, and should no longer be used.
        /// ERR_TOOMANYMATCHES (416)
        /// </summary>
        public const int ERR_WHOTRUNC = 520;

        /// <summary>
        /// </summary>
        public const int ERR_LISTSYNTAX = 521;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int ERR_NOSUCHGLINE = 521;

        /// <summary>
        /// </summary>
        public const int ERR_WHOSYNTAX = 522;

        /// <summary>
        /// <limit> :<command> search limit exceeded.
        /// </summary>
        public const int ERR_WHOLIMEXCEED = 523;

        /// <summary>
        /// </summary>
        public const int ERR_QUARANTINED = 524;

        /// <summary>
        /// </summary>
        public const int ERR_OPERSPVERIFY = 524;

        /// <summary>
        /// <term> :Help not found
        /// </summary>
        public const int ERR_HELPNOTFOUND = 524;

        /// <summary>
        /// </summary>
        public const int ERR_INVALIDKEY = 525;

        /// <summary>
        /// <nickname> :<reason>
        /// Proposed./// http://www.hades.skumler.net/~ejb/draft-brocklesby-irc-usercmdpfx-00.txt

        /// </summary>
        public const int ERR_REMOTEPFX = 525;

        /// <summary>
        /// <nickname> :<reason>
        /// Proposed./// http://www.hades.skumler.net/~ejb/draft-brocklesby-irc-usercmdpfx-00.txt

        /// </summary>
        public const int ERR_PFXUNROUTABLE = 526;

        /// <summary>
        /// <nick> :You are not permitted to send private messages to this user
        /// </summary>
        public const int ERR_CANTSENDTOUSER = 531;

        /// <summary>
        /// </summary>
        public const int ERR_BADHOSTMASK = 550;

        /// <summary>
        /// </summary>
        public const int ERR_HOSTUNAVAIL = 551;

        /// <summary>
        /// </summary>
        public const int ERR_USINGSLINE = 552;

        /// <summary>
        /// </summary>
        public const int ERR_STATSSLINE = 553;

        /// <summary>
        /// </summary>
        public const int ERR_NOTLOWEROPLEVEL = 560;

        /// <summary>
        /// </summary>
        public const int ERR_NOTMANAGER = 561;

        /// <summary>
        /// </summary>
        public const int ERR_CHANSECURED = 562;

        /// <summary>
        /// </summary>
        public const int ERR_UPASSSET = 563;

        /// <summary>
        /// </summary>
        public const int ERR_UPASSNOTSET = 564;

        /// <summary>
        /// </summary>
        public const int ERR_NOMANAGER = 566;

        /// <summary>
        /// </summary>
        public const int ERR_UPASS_SAME_APASS = 567;

        /// <summary>
        /// </summary>
        public const int ERR_LASTERROR = 568;

        /// <summary>
        /// </summary>
        public const int RPL_NOOMOTD = 568;

        /// <summary>
        /// </summary>
        public const int RPL_REAWAY = 597;

        /// <summary>
        /// Used when adding users to their <code>WATCH</code> list.
        /// </summary>
        public const int RPL_GONEAWAY = 598;

        /// <summary>
        /// Used when adding users to their <code>WATCH</code> list.
        /// </summary>
        public const int RPL_NOTAWAY = 599;

        /// <summary>
        /// </summary>
        public const int RPL_LOGON = 600;

        /// <summary>
        /// </summary>
        public const int RPL_LOGOFF = 601;

        /// <summary>
        /// </summary>
        public const int RPL_WATCHOFF = 602;

        /// <summary>
        /// </summary>
        public const int RPL_WATCHSTAT = 603;

        /// <summary>
        /// </summary>
        public const int RPL_NOWON = 604;

        /// <summary>
        /// </summary>
        public const int RPL_NOWOFF = 605;

        /// <summary>
        /// </summary>
        public const int RPL_WATCHLIST = 606;

        /// <summary>
        /// </summary>
        public const int RPL_ENDOFWATCHLIST = 607;

        /// <summary>
        /// Also known as RPL_CLEARWATCH in Unreal/// </summary>
        public const int RPL_WATCHCLEAR = 608;

        /// <summary>
        /// Returned when adding users to their <code>WATCH</code> list.
        /// </summary>
        public const int RPL_NOWISAWAY = 609;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_MAPMORE = 610;

        /// <summary>
        /// </summary>
        public const int RPL_ISOPER = 610;

        /// <summary>
        /// </summary>
        public const int RPL_ISLOCOP = 611;

        /// <summary>
        /// </summary>
        public const int RPL_ISNOTOPER = 612;

        /// <summary>
        /// </summary>
        public const int RPL_ENDOFISOPER = 613;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_MAPMORE = 615;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_WHOISMODES = 615;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_WHOISHOST = 616;

        /// <summary>
        /// <client> <nick> :has client certificate fingerprint <fingerprint>
        /// RPL_WHOISCERTFP (276)
        /// </summary>
        public const int RPL_WHOISSSLFP = 617;

        /// <summary>
        /// </summary>
        public const int RPL_DCCSTATUS = 617;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_WHOISBOT = 617;

        /// <summary>
        /// </summary>
        public const int RPL_DCCLIST = 618;

        /// <summary>
        /// </summary>
        public const int RPL_ENDOFDCCLIST = 619;

        /// <summary>
        /// </summary>
        public const int RPL_WHOWASHOST = 619;

        /// <summary>
        /// </summary>
        public const int RPL_DCCINFO = 620;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_RULESSTART = 620;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_RULES = 621;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_ENDOFRULES = 622;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int RPL_MAPMORE = 623;

        /// <summary>
        /// </summary>
        public const int RPL_OMOTDSTART = 624;

        /// <summary>
        /// </summary>
        public const int RPL_OMOTD = 625;

        /// <summary>
        /// </summary>
        public const int RPL_ENDOFO_MOTD = 626;

        /// <summary>
        /// </summary>
        public const int RPL_SETTINGS = 630;

        /// <summary>
        /// </summary>
        public const int RPL_ENDOFSETTINGS = 631;

        /// <summary>
        /// Never actually used by Unreal - was defined however the feature that would have used this numeric was never created.
        /// </summary>
        public const int RPL_DUMPING = 640;

        /// <summary>
        /// Never actually used by Unreal - was defined however the feature that would have used this numeric was never created.
        /// </summary>
        public const int RPL_DUMPRPL = 641;

        /// <summary>
        /// Never actually used by Unreal - was defined however the feature that would have used this numeric was never created.
        /// </summary>
        public const int RPL_EODUMP = 642;

        /// <summary>
        /// <client> <command> :Command processed, but a copy has been sent to ircops for evaluation (anti-spam) purposes. [<reason>]
        /// Used to let a client know that a copy of their command has been passed to operators and the reason for it.
        /// </summary>
        public const int RPL_SPAMCMDFWD = 659;

        /// <summary>
        /// <client> :STARTTLS successful, proceed with TLS handshake
        /// Indicates that the client may begin the TLS handshake/// </summary>
        public const int RPL_STARTTLS = 670;

        /// <summary>
        /// <client> <nick> :is using a secure connection
        /// The text in the last parameter may change. Also known as RPL_WHOISSSL (Nefarious).
        /// </summary>
        public const int RPL_WHOISSECURE = 671;

        /// <summary>
        /// <modes> :<info>
        /// Returns a full list of modes that are unknown when a client issues a MODE command (rather than one numeric per mode)
        /// </summary>
        public const int RPL_UNKNOWNMODES = 672;

        /// <summary>
        /// <client> <nick> :is actually from <ip>
        /// Returns the real IP address of a client connected from a CGIIRC host, this has the real IP address of the client. This message is only sent to themselves or to IRC operators who perform a WHOIS on the user.
        /// </summary>
        public const int RPL_WHOISREALIP = 672;

        /// <summary>
        /// <modes> :<info>
        /// Returns a full list of modes that cannot be set when a client issues a MODE command
        /// </summary>
        public const int RPL_CANNOTSETMODES = 673;

        /// <summary>
        /// </summary>
        public const int RPL_LANGUAGES = 690;

        /// <summary>
        /// <client> :STARTTLS failed (Wrong moon phase)
        /// Indicates that a server-side error has occured/// </summary>
        public const int ERR_STARTTLS = 691;

        /// <summary>
        /// <?> 0x<?> <?> <?>
        /// Output from the MODLIST command/// </summary>
       // DUPE:
        //  public const int RPL_MODLIST = 702;

        /// <summary>
        /// <client> :<command> <module name> <minimum parameters>
        /// </summary>
        public const int RPL_COMMANDS = 702;

        /// <summary>
        /// <client> :<text>
        /// Terminates MODLIST output/// </summary>
        public const int RPL_ENDOFMODLIST = 703;

        /// <summary>
        /// :End of COMMANDS list
        /// </summary>
        public const int RPL_COMMANDSEND = 703;

        /// <summary>
        /// <client> <command> :<text>
        /// Start of HELP command output/// </summary>
        public const int RPL_HELPSTART = 704;

        /// <summary>
        /// <client> <command> :<text>
        /// Output from HELP command/// </summary>
        public const int RPL_HELPTXT = 705;

        /// <summary>
        /// <client> <command> :<text>
        /// End of HELP command output/// </summary>
        public const int RPL_ENDOFHELP = 706;

        /// <summary>
        /// <client> <target> :Targets changing too fast, message dropped
        /// See doc/tgchange.txt in the charybdis source./// </summary>
        public const int ERR_TARGCHANGE = 707;

        /// <summary>
        /// <?> <?> <?> <?> <?> <?> <?> :<?>
        /// Output from 'extended' trace/// </summary>
        public const int RPL_ETRACEFULL = 708;

        /// <summary>
        /// <?> <?> <?> <?> <?> <?> :<?>
        /// Output from 'extended' trace/// </summary>
        public const int RPL_ETRACE = 709;

        /// <summary>
        /// <client> <channel> <nick>!<user>@<host> :<text>
        /// Message delivered using KNOCK command/// </summary>
        public const int RPL_KNOCK = 710;

        /// <summary>
        /// <client> <channel> :<text>
        /// Message returned from using KNOCK command (KNOCK delivered)/// </summary>
        public const int RPL_KNOCKDLVR = 711;

        /// <summary>
        /// <client> <channel> :<text>
        /// Message returned when too many KNOCKs for a channel have been sent by a user
        /// </summary>
        public const int ERR_TOOMANYKNOCK = 712;

        /// <summary>
        /// <client> <channel> :<text>
        /// Message returned from KNOCK when the channel can be freely joined by the user
        /// </summary>
        public const int ERR_CHANOPEN = 713;

        /// <summary>
        /// <client> <channel> :<text>
        /// Message returned from KNOCK when the user has used KNOCK on a channel they have already joined
        /// </summary>
        public const int ERR_KNOCKONCHAN = 714;

        /// <summary>
        /// <client> :<text>
        /// Returned from KNOCK when the command has been disabled/// </summary>
        public const int ERR_KNOCKDISABLED = 715;

        /// <summary>
        /// <client> <channel> :Too many INVITEs (<channel/user>).
        /// Sent to indicate an INVITE has been blocked. The last param is the literal string "channel" if this is because the channel has had too many INVITEs in a given time, and "user" if this is because the user has sent too many INVITEs in a given time
        /// </summary>
        public const int ERR_TOOMANYINVITE = 715;

        /// <summary>
        /// <client> <nick> <channel> :You are inviting too fast, invite to <nick> for <channel> not sent.
        /// Sent to indicate an INVITE has been blocked.
        /// </summary>
        public const int RPL_INVITETHROTTLE = 715;

        /// <summary>
        /// <nick> :<info>
        /// Sent to indicate the given target is set +g (server-side ignore)
        /// Mentioned as RPL_TARGUMODEG in the CALLERID spec, ERR_TARGUMODEG in the ratbox/charybdis implementations.
        /// </summary>
        public const int RPL_TARGUMODEG = 716;

        /// <summary>
        /// <nick> :<info>
        /// Sent following a PRIVMSG/NOTICE to indicate the target has been notified of an attempt to talk to them while they are set +g
        /// </summary>
        public const int RPL_TARGNOTIFY = 717;

        /// <summary>
        /// <client> <nick> <user>@<host> :<info>
        /// Sent to a user who is +g to inform them that someone has attempted to talk to them (via PRIVMSG/NOTICE), and that they will need to be accepted (via the ACCEPT command) before being able to talk to them
        /// </summary>
        public const int RPL_UMODEGMSG = 718;

        /// <summary>
        /// <client> :<text>
        /// IRC Operator MOTD header, sent upon OPER command/// </summary>
        // DUPE:
        // public const int RPL_OMOTDSTART = 720;

        /// <summary>
        /// <client> :<text>
        /// IRC Operator MOTD text (repeated, usually)/// </summary>
        // DUPE:
        // public const int RPL_OMOTD = 721;

        /// <summary>
        /// <client> :<text>
        /// IRC operator MOTD footer/// </summary>
        public const int RPL_ENDOFOMOTD = 722;

        /// <summary>
        /// <client> <command> :<text>
        /// Returned from an oper command when the IRC operator does not have the relevant operator privileges.
        /// </summary>
        public const int ERR_NOPRIVS = 723;

        /// <summary>
        /// <client> <nick>!<user>@<host> <?> <?> :<text>
        /// Reply from an oper command reporting how many users match a given user@host mask
        /// </summary>
        public const int RPL_TESTMASK = 724;

        /// <summary>
        /// <client> <?> <?> <?> :<?>
        /// Reply from an oper command reporting relevant I/K lines that will match a given user@host
        /// </summary>
        public const int RPL_TESTLINE = 725;

        /// <summary>
        /// <client> <?> :<text>
        /// Reply from oper command reporting no I/K lines match the given user@host
        /// </summary>
        public const int RPL_NOTESTLINE = 726;

        /// <summary>
        /// <client> <lcount> <gcount> <nick>!<user>@<host> <gecos> :Local/remote clients match
        /// From the m_testmask module, "Shows the number of matching local and global clients for a user@host mask"
        /// </summary>
        public const int RPL_TESTMASKGECOS = 727;

        /// <summary>
        /// <client> <channel> <banid> q [<time_left> :<reason>]
        /// Same thing as RPL_BANLIST, but for mode +q (quiet)
        /// </summary>
        public const int RPL_QUIETLIST = 728;

        /// <summary>
        /// <client> <channel> q :<info>
        /// Same thing as RPL_ENDOFBANLIST, but for mode +q (quiet)
        /// </summary>
        public const int RPL_ENDOFQUIETLIST = 729;

        /// <summary>
        /// <client> :target[!user@host][,target[!user@host]]*
        /// Used to indicate to a client that either a target has just become online, or that a target they have added to their monitor list is online
        /// </summary>
        public const int RPL_MONONLINE = 730;

        /// <summary>
        /// <client> :target[,target2]*
        /// Used to indicate to a client that either a target has just left the IRC network, or that a target they have added to their monitor list is offline
        /// </summary>
        public const int RPL_MONOFFLINE = 731;

        /// <summary>
        /// <client> :target[,target2]*
        /// Used to indicate to a client the list of targets they have in their monitor list
        /// </summary>
        public const int RPL_MONLIST = 732;

        /// <summary>
        /// <client> :End of MONITOR list
        /// Used to indicate to a client the end of a monitor list
        /// </summary>
        public const int RPL_ENDOFMONLIST = 733;

        /// <summary>
        /// <client> <limit> <targets> :Monitor list is full.
        /// Used to indicate to a client that their monitor list is full, so the MONITOR command failed
        /// </summary>
        public const int ERR_MONLISTFULL = 734;

        /// <summary>
        /// <client> :<chal_line>
        /// From the ratbox m_challenge module, to auth opers.
        /// </summary>
        public const int RPL_RSACHALLENGE2 = 740;

        /// <summary>
        /// <client> :End of CHALLENGE
        /// From the ratbox m_challenge module, to auth opers.
        /// </summary>
        public const int RPL_ENDOFRSACHALLENGE2 = 741;

        /// <summary>
        /// <channel> <modechar> <mlock> :MODE cannot be set due to channel having an active MLOCK restriction policy
        /// </summary>
        public const int ERR_MLOCKRESTRICTED = 742;

        /// <summary>
        /// <channel> <modechar> <mask> :Invalid ban mask
        /// </summary>
        public const int ERR_INVALIDBAN = 743;

        /// <summary>
        /// Defined in the Charybdis source code with the comment <code>/* inspircd */</code>
        /// </summary>
        public const int ERR_TOPICLOCK = 744;

        /// <summary>
        /// <count> :matches
        /// From the ratbox m_scan module.
        /// </summary>
        public const int RPL_SCANMATCHED = 750;

        /// <summary>
        /// <nick> <username> <host> <sockhost> <servname> <umodes> :<info>
        /// From the ratbox m_scan module.
        /// </summary>
        public const int RPL_SCANUMODES = 751;

        /// <summary>
        /// <Target> <Key> <Visibility> :<Value>
        /// Reply to WHOIS - Metadata key/value associated with the target
        /// </summary>
        public const int RPL_WHOISKEYVALUE = 760;

        /// <summary>
        /// <Target> <Key> <Visibility>[ :<Value>]
        /// Returned to show a currently set metadata key and its value, or a metadata key that has been cleared if no value is present in the response
        /// </summary>
        public const int RPL_KEYVALUE = 761;

        /// <summary>
        /// :end of metadata
        /// Indicates the end of a list of metadata keys/// </summary>
        public const int RPL_METADATAEND = 762;

        /// <summary>
        /// <Target> :metadata limit reached
        /// Used to indicate to a client that their metadata store is full, and they cannot add the requested key(s)
        /// </summary>
        public const int ERR_METADATALIMIT = 764;

        /// <summary>
        /// <Target> :invalid metadata target
        /// Indicates to a client that the target of a sent METADATA command is invalid
        /// </summary>
        public const int ERR_TARGETINVALID = 765;

        /// <summary>
        /// <Key> :no matching key
        /// Indicates to a client that the requested metadata key does not exist
        /// </summary>
        public const int ERR_NOMATCHINGKEY = 766;

        /// <summary>
        /// <Key> :invalid metadata key
        /// Indicates to a client that the requested metadata key is not valid
        /// </summary>
        public const int ERR_KEYINVALID = 767;

        /// <summary>
        /// <Target> <Key> :key not set
        /// Indicates to a client that the metadata key they requested to clear is not already set
        /// </summary>
        public const int ERR_KEYNOTSET = 768;

        /// <summary>
        /// <Target> <Key> :permission denied
        /// Indicates to a client that they do not have permission to set the requested metadata key
        /// </summary>
        public const int ERR_KEYNOPERMISSION = 769;

        /// <summary>
        /// Used to send 'eXtended info' to the client, a replacement for the STATS command to send a large variety of data and minimise numeric pollution.
        /// </summary>
        public const int RPL_XINFO = 771;

        /// <summary>
        /// Start of an RPL_XINFO list/// </summary>
        public const int RPL_XINFOSTART = 773;

        /// <summary>
        /// Termination of an RPL_XINFO list/// </summary>
        public const int RPL_XINFOEND = 774;

        /// <summary>
        /// Used by the m_check module of InspIRCd./// </summary>
        public const int RPL_CHECK = 802;

        /// <summary>
        /// <client> <nick>!<ident>@<host> <account> :You are now logged in as <user>
        /// Sent when the user's account name is set (whether by SASL or otherwise)
        /// </summary>
        public const int RPL_LOGGEDIN = 900;

        /// <summary>
        /// <client> <nick>!<ident>@<host> :You are now logged out
        /// Sent when the user's account name is unset (whether by SASL or otherwise)
        /// </summary>
        public const int RPL_LOGGEDOUT = 901;

        /// <summary>
        /// <client> :You must use a nick assigned to you.
        /// Sent when the SASL authentication fails because the account is currently locked out, held, or otherwise administratively made unavailable.
        /// </summary>
        public const int ERR_NICKLOCKED = 902;

        /// <summary>
        /// <client> :SASL authentication successful
        /// Sent when the SASL authentication finishes successfully
        /// RPL_LOGGEDIN (900)
        /// </summary>
        public const int RPL_SASLSUCCESS = 903;

        /// <summary>
        /// <client> :SASL authentication failed
        /// Sent when the SASL authentication fails because of invalid credentials or other errors not explicitly mentioned by other numerics
        /// </summary>
        public const int ERR_SASLFAIL = 904;

        /// <summary>
        /// <client> :SASL message too long
        /// Sent when credentials are valid, but the SASL authentication fails because the client-sent AUTHENTICATE command was too long (i.e. the parameter longer than 400 bytes)
        /// </summary>
        public const int ERR_SASLTOOLONG = 905;

        /// <summary>
        /// <client> :SASL authentication aborted
        /// Sent when the SASL authentication is aborted because the client sent an AUTHENTICATE command with * as the parameter
        /// </summary>
        public const int ERR_SASLABORTED = 906;

        /// <summary>
        /// <client> :You have already authenticated using SASL
        /// Sent when the client attempts to initiate SASL authentication after it has already finished successfully for that connection.
        /// </summary>
        public const int ERR_SASLALREADY = 907;

        /// <summary>
        /// <client> <mechanisms> :are available SASL mechanisms
        /// Sent when the client requests a list of SASL mechanisms supported by the server (or network, services). The numeric contains a comma-separated list of mechanisms
        /// </summary>
        public const int RPL_SASLMECHS = 908;

        /// <summary>
        /// <channel> <message> :Your message contained a censored word, and was blocked
        /// </summary>
        public const int ERR_WORDFILTERED = 936;

        /// <summary>
        /// <client> <nick> :Nickname now unlocked.
        /// Used by InspIRCd's m_nicklock module./// </summary>
        public const int No_Name_Supplied = 945;

        /// <summary>
        /// <client> <nick> :This user's nickname is not locked.
        /// Used by InspIRCd's m_nicklock module./// </summary>
        public const int No_Name_Supplied2 = 946;

        /// <summary>
        /// <client> <command> :<info>
        /// Indicates that a command could not be performed for an arbitrary reason. For example, a halfop trying to kick an op.
        /// </summary>
        public const int ERR_CANNOTDOCOMMAND = 972;

        /// <summary>
        /// <modulename> :Failed to unload module: <error>
        /// </summary>
        public const int ERR_CANTUNLOADMODULE = 972;

        /// <summary>
        /// <modulename> :Module successfully unloaded.
        /// </summary>
        public const int RPL_UNLOADEDMODULE = 973;

        /// <summary>
        /// <channel> <mode> :<info>
        /// Indicates that a channel mode could not be changeded for an arbitrary reason. For instance, trying to set OPER_ONLY when you are not an IRC operator.
        /// </summary>
        public const int ERR_CANNOTCHANGECHANMODE = 974;

        /// <summary>
        /// <modulename> :Failed to load module: <error>
        /// </summary>
        public const int ERR_CANTLOADMODULE = 974;

        /// <summary>
        /// <modulename> :Module successfully loaded.
        /// </summary>
        public const int RPL_LOADEDMODULE = 975;

        /// <summary>
        /// </summary>
        // DUPE:
        // public const int ERR_LASTERROR = 975;

        /// <summary>
        /// Also known as ERR_NUMERICERR (Unreal) or ERR_LAST_ERR_MSG/// </summary>
        public const int ERR_NUMERIC_ERR = 999;
    }
}
