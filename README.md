This is a basic C# library for connecting to IRC networks.
It only has a bare-mimimum of features but it might be useful as a starting point or for learning.

Useful classes:

	IRCServerConnection 
		A raw connection to an IRC server. This does no parsing of server
		messages and only accepts raw messages to send back. The only extra 
		thing it does for you is respond to PING with PONG.
	
	IRCServer  
		Wrapper for IRCServerConnection, this will parse server messages and 
		send out events. It also provides methods for sending common messages 
		back to the server (And many more could be added). This probably the
		class you'll want to use.
		
	IRCNumerics
		Contains definitions for most (I think) of the common numeric values. 
		Information comes from numerics.yaml found in this project: 
		https://github.com/ircdocs/irc-defs
	

Check Program.cs for examples of how to use these classes.