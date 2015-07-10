using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.Common
{
    /// <summary>
    /// Logger for the System
    /// </summary>
    public static class Logger
    {
        static ConcurrentBag<string> Logs = new ConcurrentBag<string>();
        private const string eventLogSource = "WordyFly.Service";
        private const string eventLog = "WordFly";
        private static bool eventSourceAvailable = false;
       static Logger()
        {
            try
            {
                if (!EventLog.SourceExists(eventLogSource))
                    EventLog.CreateEventSource(eventLogSource, eventLog);
            }
            catch (Exception)
            {
                eventSourceAvailable = false;
                
            }
            
        }
        public static void Log(Object logMessage, LogTypes logType = LogTypes.Information)
        {
            string message = string.Format("DateTime : {0} | Type of Log : {1} | Message : {2}",DateTime.Now ,logType.ToString(), logMessage.ToString());

            Console.WriteLine(message);
            if (eventSourceAvailable)
            {
                EventLog.WriteEntry(eventLogSource, message, EventLogEntryType.Error);
            }
            else
            {
                string fileName = "WordyFly.Logs.txt";
                string runningBasePath = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
                var filePath = Path.Combine(Path.GetDirectoryName(runningBasePath), fileName);
                File.AppendAllText(filePath, message);
            }
            Trace.TraceInformation(message);
            Logs.Add(message);
        }

        /// <summary>
        /// DIfferent type of logs
        /// </summary>
        public enum LogTypes
        {
            Information,
            Warning,
            Error,
            Exception
        }
    }
}
