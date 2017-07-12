﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace IRCLib
{
    class FileWriter
    {
        public string fileName = "";
        public StreamWriter streamWriter = null;
        public bool explicitOnly = false;
        
        public FileWriter(string _fileName, StreamWriter _writer, bool _explicit = false)
        {
            fileName = _fileName;
            streamWriter = _writer;
            explicitOnly = _explicit;
        }
    }

    static class DebugLogger
    {
        const string defaultLogFileName = "DebugLoggerDefaultLog.txt";

        static bool isInit = false;
        static string buffer;
        static bool defaultFile;
        static List<FileWriter> logFiles;

        /// <summary>
        /// Initializes the DebugLogger using the passed in options.
        /// </summary>
        /// <param name="_timeStamps">If true, will prefix each line with a timestamp from DateTime.Now</param>
        /// <param name="_defaultFile">If true, will always output log info to DebugLoggerDefaultLog.txt along with extra DebugLogger info.</param>
        /// <returns>False if the DebugLogger is already initialized.</returns>
        static public bool Initialize(bool _defaultFile = true)
        {
            if (isInit)
                return false;

            buffer = "";
            defaultFile = _defaultFile;
            logFiles = new List<FileWriter>();

            if (_defaultFile)
            {
                FileWriter def = new FileWriter(defaultLogFileName, new StreamWriter(defaultLogFileName, true));
                logFiles.Add(def);
                //Trace.Listeners.Add(new TextWriterTraceListener(defaultLogFileName));
                def.streamWriter.WriteLine("DebugLogger initialized - Called from: " + new StackTrace().GetFrame(1));
            }

            isInit = true;
            return true;
        }


        static public void AddLogFile(string filename, bool _explicitOnly = false)
        {
            logFiles.Add(new FileWriter(filename, new StreamWriter(filename, true), _explicitOnly));
            //Trace.Listeners.Add(new TextWriterTraceListener(filename));
        }


        static public void LogLine(string l, bool TimeStamp = true)
        {
            string line = "";

            if (TimeStamp)
            {
                line = "[" + DateTime.Now + "] ";
            }

            line += l;
            
            foreach (FileWriter file in logFiles)
            {
                if (file.explicitOnly)
                    continue;

                file.streamWriter.WriteLine(line);
                file.streamWriter.Flush();
            }

            buffer += line + '\n';
        }

        static public bool LogLine(string line, string file, bool TimeStamp  = true)
        {
            foreach (FileWriter fw in logFiles)
            {
                if (fw.fileName == file)
                {
                    string lineFinal = "";

                    if (TimeStamp)
                    {
                        lineFinal = "[" + DateTime.Now + "] ";
                    }

                    lineFinal += line;

                    fw.streamWriter.WriteLine(lineFinal);
                    fw.streamWriter.Flush();
                    return true;
                }
            }

            return false;
        }
    }
}
