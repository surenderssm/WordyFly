using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WordFly.Common
{
    /// <summary>
    /// Generic Utility Properties/Functions
    /// </summary>
    public static class CommonUtility
    {
        private static string wordSourceFileLocation;
        public static string WordSourceFileLocation
        {
            get
            {
                if (string.IsNullOrEmpty(wordSourceFileLocation))
                {
                    wordSourceFileLocation = GetAppSettings("WordSourceFileLocation");
                }
                return wordSourceFileLocation;
            }

        }

        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

    }
}