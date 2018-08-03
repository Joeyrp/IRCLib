/******************************************************************************
*	File		-	IRCServerConnection.cs
*	Author		-	Joey Pollack
*	Date		-	11/19/2015 (m/d/y)
*	Mod Date	-	7/13/2017 (m/d/y)
*	Description	-	Maintains a connection to an irc server and allows for
*                   sending and receiving raw IRC protocol messages.
******************************************************************************/

using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace IRCLib
{ 
    class ConnectionInfo
    {
        public string serverAddress;
        public int port = 6667;
        public string nick = "";
        public string password = ""; 
    }

    /// <summary>
    /// Handles a connection to a single IRC server. PING messages are responded to automatically.
    /// </summary>
    class IRCServerConnection
    {
        TcpClient tcpClient = null;
        // StreamReader inputStream;
        IRCInputStream inputStream;
        StreamWriter outputStream;
        ConnectionInfo connectionInfo;
        volatile bool connected = false;
        int bufferLimit = 1000000;  // ~1MB
        string serverResponseBuffer = "";

        #region PROPERTIES
        public ConnectionInfo ConnectionInfo
        {
            get { return connectionInfo; }
            set { }
        }

        public bool IsConnected
        {
            get { return connected; }
            set { }
        }

        public string MessageBuffer
        {
            get { return serverResponseBuffer; }
            set { }
        }
        #endregion

        //Mutex connectionInfoMutex;
        //Mutex responseBuferMutex;
        //Mutex outputStreamMutex;
        //Thread connectionThread;

        /// <summary>
        /// Attempt to connect to the given server.
        /// </summary>
        /// <param name="_serverAddress">eg. irc.speedrunslive.com</param>
        /// <param name="_port">usually 6667</param>
        /// <param name="_nick">your nick</param>
        /// <param name="_password">your password. This can be empty if the server uses nickserv.</param>
        /// <returns>true if the connection is successful. If false if there is already a connection if the connection failed. 
        /// There may be more info in the log file.</returns>
        public bool Connect(string _serverAddress, int _port, string _nick, string _password, string name = "IRCLib_Default")
        {
            if (connected)
                return false;

            connectionInfo = new ConnectionInfo();
            connectionInfo.serverAddress = _serverAddress;
            connectionInfo.port = _port;
            connectionInfo.nick = _nick;
            connectionInfo.password = _password;


            tcpClient = new TcpClient(connectionInfo.serverAddress, connectionInfo.port);

            if (tcpClient == null || !tcpClient.Connected)
            {
                DebugLogger.LogLine("Could not create tcp client or connection failed.");
                return false;
            }

            inputStream = new IRCInputStream();
            inputStream.InitReader(new StreamReader(tcpClient.GetStream()));
            outputStream = new StreamWriter(tcpClient.GetStream());

            if (connectionInfo.password != "")
            { 
                outputStream.WriteLine("PASS " + connectionInfo.password);
                outputStream.Flush();
            }

            outputStream.WriteLine("NICK " + connectionInfo.nick);
            outputStream.Flush();
            outputStream.WriteLine("USER 0 " + connectionInfo.nick + " * :" + name);
            outputStream.Flush();

            connected = true;
            return true;
        }
        
        /// <summary>
        /// Peace out server.
        /// </summary>
        public void Disconnect(string quitMsg = "")
        {
            if (!connected)
                return;

            outputStream.WriteLine("QUIT " + quitMsg);
            outputStream.Flush();

          //  inputStream.Close();
          //  inputStream = null;
            tcpClient = null;
            connected = false;
        }

        /// <summary>
        /// Clears the buffer that stores responses from the server.
        /// </summary>
        public void ClearMessageBuffer()
        {
            DebugLogger.LogLine("ClearMessageBuffer called by " + new StackTrace().GetFrame(1));
            serverResponseBuffer = "";
        }

        /// <summary>
        /// Call this method to get messages from the server. This method should be called regularly so PING message are not missed.
        /// All server responses are stored in a buffer that can be read with MessageBuffer and cleared with ClearMessageBuffer().
        /// </summary>
        /// <returns>The oldest server message that hasn't been returned yet.</returns>
        public string PollServer()
        {
            if (!connected)
            {
                DebugLogger.LogLine("Attempt to poll the server but there is no connection.");
                return "";
            }
            
            string msg = "";
            if (inputStream.InputAvailable())
            {
                msg = inputStream.ReadLine();
            }
            else
            {
                return "";
            }

            // Handle PING
            if (msg.Contains("PING"))
            {
               // Console.WriteLine("PING ... PONG");

                string response = msg.Replace("PING", "PONG");
                outputStream.WriteLine(response);
                outputStream.Flush();
            }

            // Clear out first half of buffer if it is at the limit
            if (serverResponseBuffer.Length >= bufferLimit)
            {
                // find end of first line at the half way point
                int halfway = serverResponseBuffer.Length / 2;
                int firstCharAfterLine = halfway;
                while (serverResponseBuffer[firstCharAfterLine] != '\n')
                {
                    firstCharAfterLine++;
                }

                string oldBuffer = serverResponseBuffer;
                serverResponseBuffer = "";
                for (int i = firstCharAfterLine; i < oldBuffer.Length; i++)
                {
                    serverResponseBuffer += oldBuffer[i];
                }
            }

            serverResponseBuffer += msg;

            return msg;
        }

        /// <summary>
        /// Sends a message to the server if there is a connection.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        public void SendMessage(string message)
        {
            if (!connected)
                return;

            outputStream.WriteLine(message);
            outputStream.Flush();
        }

        ~IRCServerConnection()
        {
            if (inputStream != null)
                inputStream.Close();
        }
    }
}
