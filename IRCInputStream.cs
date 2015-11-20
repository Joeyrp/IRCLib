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
        StreamWriter log = new StreamWriter("ReaderLog.txt");

        StreamReader reader = null;
        string buffer;
        const int bufferLimit = 1000000;    // ~1MB

        Mutex bufferMutex = new Mutex();
        Thread readThread;

        public void InitReader(StreamReader r)
        {
            reader = r;
            buffer = "";
            readThread = new Thread(read);
            readThread.Start();
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
                return "";

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

            log.Write(buffer + "\n\n\n\n");
            log.Flush();
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
            while (!reader.EndOfStream)
            {
                if (bufferMutex.WaitOne(1000))
                {
                    if (buffer != "")
                        buffer += '\n';
                    buffer += reader.ReadLine();

                    // discard old lines if the buffer is at the limit.
                    while (buffer.Length >= bufferLimit)
                    {
                        DebugLogger.LogLine("IRCInputStream: buffer at the limit");

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
