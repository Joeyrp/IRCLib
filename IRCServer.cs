/***************************************************************
*   IRCLib.cs - Maintains a connection to an irc server and 
*               provides acces to the raw data coming from                                                
*               the server.
****************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IRCLib
{ 
    class ConnectionInfo
    {
        public string serverAddress;
        public int port = 6667;
        public string nick = ""; 
    }

    class IRCServer
    {
        TcpClient tcpClient = null;
        StreamReader inputStream;
        StreamWriter outputStream;
        ConnectionInfo connectionInfo;
        volatile bool connected = false;
        string serverResponseBuffer = "";

        #region PROPERTIES
        /// <summary>
        /// May Block. Use TryGetConnectionInfo to avoid blocking if the ConnectionInfo is unavailable.
        /// </summary>
        public ConnectionInfo ConnectionInfo
        {
            get { return connectionInfo; }
            set { }
        }
        #endregion

        Mutex connectionInfoMutex;
        Mutex responseBuferMutex;
        Mutex outputStreamMutex;
        Thread connectionThread;

        public bool Connect(string _serverAddress, int _port, string _nick)
        {
            if (connected)
                return false;
            
            connectionInfo = new ConnectionInfo();
            connectionInfo.serverAddress = _serverAddress;
            connectionInfo.port = _port;
            connectionInfo.nick = _nick;

            connectionInfoMutex = new Mutex();
            responseBuferMutex = new Mutex();
            outputStreamMutex = new Mutex();
            connectionThread = new Thread(Connection);

            try
            {
                connectionThread.Start();
            }
            catch (Exception e)
            {
                DebugLogger.LogLine("Error in the Connection thread: " + e.Message);
            }

            connected = true;
            return true;
        }
        


        /// <summary>
        /// Runs the server connection loop on its own thread.
        /// </summary>
        private void Connection()
        {
            if (connectionInfoMutex.WaitOne(15000))
            {
                tcpClient = new TcpClient(connectionInfo.serverAddress, connectionInfo.port);

                connectionInfoMutex.ReleaseMutex();
            }
            else
            {
                throw new Exception("ConnectionInfoMutex unavailable");
            }


            if (!tcpClient.Connected)
            {

                throw new Exception("Could not create tcp client.");
                //if (ConnectionEvent != null)
                //{
                //    ConnectionEvent(this, IRCEvent.IRC_CONNECTION_FAILED);
                //}
                return;
            }


        }
    }
}
