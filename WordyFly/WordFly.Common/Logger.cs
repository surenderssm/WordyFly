using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.Common
{
    /// <summary>
    /// Logger for the System
    /// </summary>
    public static class Logger
    {
        public static void Log(LogTypes logType, Object logMessage)
        {
            Console.WriteLine(string.Format("Type of Log : {0} , Message : {1}", logType.ToString(), logMessage.ToString()));
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
