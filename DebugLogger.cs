/******************************************************************************
*	File		-	DebugLogger.cs
*	Author		-	Joey Pollack
*	Date		-	11/19/2015 (m/d/y)
*	Mod Date	-	7/13/2017 (m/d/y)
*	Description	-	Static class for maintaining multiple debug log files
******************************************************************************/
using System;
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
        
        public FileWriter(string _fileName, StreamWriter _writer, bool _explicit = true)
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
                FileWriter def = new FileWriter(defaultLogFileName, new StreamWriter(defaultLogFileName, true), false);
                logFiles.Add(def);
                //Trace.Listeners.Add(new TextWriterTraceListener(defaultLogFileName));
                //def.streamWriter.WriteLine("DebugLogger initialized - Called from: " + new StackTrace().GetFrame(1));
                def.streamWriter.WriteLine("DebugLogger initialized - Called from: " + GetCallingMethod());
            }

            isInit = true;
            return true;
        }

        /// <summary>
        /// Add a new file to the list of log files
        /// </summary>
        /// <param name="filename">Name of the file to add</param>
        /// <param name="_explicitOnly">If true this file will only be written to when it's filename is passed to LogLine. 
        ///                             If false this file will receive every LogLine call</param>
        static public void AddLogFile(string filename, bool _explicitOnly = true)
        {
            logFiles.Add(new FileWriter(filename, new StreamWriter(filename, true), _explicitOnly));
            //Trace.Listeners.Add(new TextWriterTraceListener(filename));
            StackFrame sf = new StackTrace().GetFrame(1);

            // The following line never seems to give the correct info
            //LogLine("DebugLogger File Added - Called from: " + sf.GetFileName() + " - " + sf.GetMethod().Name + " line: " + sf.GetFileLineNumber(), defaultLogFileName);

            if (defaultFile)
                LogLine("DebugLogger File Added - Called from: " + GetCallingMethod(), defaultLogFileName, false);
        }


        /// <summary>
        /// Write a line to every non explicit-only file
        /// </summary>
        /// <param name="l">Line to write</param>
        /// <param name="TimeStamp">If true will add a time stamp to the start of the line</param>
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


                if (file.streamWriter.BaseStream != null && file.streamWriter.BaseStream.CanWrite)
                {

                    file.streamWriter.WriteLine(line);
                    file.streamWriter.Flush();
                    
                }
                else
                {
                    HandleLogFail(line, file.fileName);
                }
            }

            buffer += line + '\n';
        }

        /// <summary>
        /// Write a line to the given file and every non explicit-only file
        /// </summary>
        /// <param name="line">Line to write</param>
        /// <param name="file">The explicit-only file to write to</param>
        /// <param name="TimeStamp">If true will add a time stamp to the start of the line</param>
        /// <returns></returns>
        static public bool LogLine(string line, string file, bool TimeStamp  = true)
        {
            bool succeed = false;
            foreach (FileWriter fw in logFiles)
            {
                if (fw.fileName == file || !fw.explicitOnly)
                {
                    string lineFinal = "";

                    if (TimeStamp)
                    {
                        lineFinal = "[" + DateTime.Now + "] ";
                    }

                    lineFinal += line;

                    if (fw.streamWriter.BaseStream != null && fw.streamWriter.BaseStream.CanWrite)
                    {
                        fw.streamWriter.WriteLine(lineFinal);
                        fw.streamWriter.Flush();
                        
                        if (fw.explicitOnly)
                            succeed = true;
                    }
                    else
                    {
                        HandleLogFail(lineFinal, fw.fileName);
                    }
                }
            }

            return succeed;
        }

        static string GetCallingMethod()
        {
            string fullTrace = Environment.StackTrace;
            string[] spaceSplit = fullTrace.Split('\n');
            string calledFrom = "";

            int cnt = 0;
            for (int i = 0; i < spaceSplit.Length; i++)
            {
                if (spaceSplit[i].StartsWith("   at IRCLib.DebugLogger"))
                {
                    cnt++;

                    if (cnt == 2)
                    {
                        calledFrom = spaceSplit[i + 1].Split(')')[0] + ')';
                        break;
                    }
                }
            }

            char[] sep = { ' ' };
            calledFrom = calledFrom.TrimStart(' ').Split(sep, 2)[1];

            return calledFrom;
        }

        static void HandleLogFail(string l, string file)
        {
           StreamWriter sw = new StreamWriter("LOG_ERRORS.txt", true);
            
            if (sw.BaseStream.CanWrite)
            {
                sw.WriteLine("ERROR LOGGING LINE: " + l + " --- in file: " + file);
                sw.Close();
            }
            else
            {
                Console.Error.WriteLine("ERROR LOGGING LINE: " + l + " --- in file: " + file);
            }
        }
    }
}
