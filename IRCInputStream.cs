/******************************************************************************
*	File		-	IRCInputStream.cs
*	Author		-	Joey Pollack
*	Date		-	11/23/2015 (m/d/y)
*	Mod Date	-	7/13/2017 (m/d/y)
*	Description	-	Listens for input on a given stream reader 
*	                This is not actually specific to IRC and could be used
*	                with any stream reader that is waiting on network responses.
******************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IRCLib
{
    class IRCInputStream
    {
       // StreamWriter log = new StreamWriter("ReaderLog.txt");

        StreamReader reader = null;
        string buffer;
        const int bufferLimit = 1000000;    // ~1MB
       // bool open = false;

        Mutex bufferMutex = new Mutex();
        Thread readThread;

        public void InitReader(StreamReader r)
        {
            reader = r;
            buffer = "";
            readThread = new Thread(read);
            readThread.Start();
            DebugLogger.AddLogFile("ReaderLog.txt");
            DebugLogger.LogLine("IRC Input Stream initialized", "ReaderLog.txt");
        }

        //~IRCInputStream()
        public void Close()
        {
            if (bufferMutex.WaitOne(1000))
            {
                reader.Close();

                bufferMutex.ReleaseMutex();
            }
            else
            {
                reader.Close();
            }

            DebugLogger.LogLine("IRC Input Stream closed", "ReaderLog.txt");
        }

        public bool InputAvailable()
        {
            if (null == reader)
                return false;

            if (bufferMutex.WaitOne(250))
            {
                if (buffer != "")
                {
                    bufferMutex.ReleaseMutex();
                    return true;
                }

                bufferMutex.ReleaseMutex();
            }
            return false;
        }

        public string ReadLine()
        {
            if (!bufferMutex.WaitOne(500))
            {
                DebugLogger.LogLine("Could not aquire mutex", "ReaderLog.txt");
                return "";
            }

            if ("" == buffer)
            {

                bufferMutex.ReleaseMutex();
                return "";
            }

            if (!buffer.Contains('\n'))
            {
                string line = buffer;
                buffer = "";

                bufferMutex.ReleaseMutex();
                return line;
            }

            DebugLogger.LogLine(buffer + "\n\n\n\n", "ReaderLog.txt");
            string[] lines = buffer.Split('\n');

            buffer = "";
            for (int i = 1; i < lines.Length; i++)
            {
                if (i + 1 == lines.Length)
                    buffer += lines[i];
                else
                    buffer += lines[i] + '\n';
            }

            bufferMutex.ReleaseMutex();
            return lines[0];
        }

        private void read()
        {
            // This while condition appears to work fine but I don't remember why...
            while (!reader.EndOfStream /*&& open*/)
            {
                if (bufferMutex.WaitOne(1000))
                {
                    if (buffer != "")
                        buffer += '\n';
                    buffer += reader.ReadLine();

                    // discard old lines if the buffer is at the limit.
                    while (buffer.Length >= bufferLimit)
                    {
                        DebugLogger.LogLine("IRCInputStream: buffer at the limit", "ReaderLog.txt");

                        string[] lines = buffer.Split('\n');

                        buffer = "";
                        for (int i = 1; i < lines.Length; i++)
                        {
                            if (i + 1 == lines.Length)
                                buffer += lines[i];
                            else
                                buffer += lines[i] + '\n';
                        }
                    }

                    bufferMutex.ReleaseMutex();
                }
               
            }
        }
    }
}
