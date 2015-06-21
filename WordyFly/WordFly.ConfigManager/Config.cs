using Microsoft.Azure;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Linq;


namespace WordFly.ConfigManager
{
    /// <summary>
    /// Reads the Configuration from the Hosted environment (cloudconfig or app.config)
    /// </summary>
    public static class Config
    {
        private static ConcurrentDictionary<string, string> configCache = new ConcurrentDictionary<string, string>(); //Local Cache

        //static Config()
        //{
        //    //if (RoleEnvironment.IsAvailable)
        //    //    RoleEnvironment.Changed += delegate(object sender, RoleEnvironmentChangedEventArgs e)
        //    //    {
        //    //        foreach (RoleEnvironmentConfigurationSettingChange setting in e.Changes.OfType<RoleEnvironmentConfigurationSettingChange>())
        //    //        {
        //    //            if (configCache.ContainsKey(setting.ConfigurationSettingName))
        //    //            {
        //    //                string tempVal;
        //    //                configCache.TryRemove(setting.ConfigurationSettingName, out tempVal);
        //    //            }
        //    //        }
        //    //    };
        //}

        /// <summary>
        /// Gets the Value of the Key from CloudConfig or AppConfig
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string key)
        {
            try
            {
                string value = string.Empty;
                if (configCache.TryGetValue(key, out value))
                    return value;

                if (RoleEnvironment.IsAvailable)
                    value = CloudConfigurationManager.GetSetting(key);
                else
                    value = ConfigurationManager.AppSettings.Get(key);

                configCache.TryAdd(key, value);
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading Key - " + key + " Error: " + ex.Message);
            }
        }

        public static string QueueConnectionString
        {
            get { return Get("QueueConnection"); }
        }
        public static string StorageAccounntConnectionString
        {
            get { return "DefaultEndpointsProtocol=https;AccountName=wordflydev;AccountKey=fCsMgCogj8fAP6wxAve4CjzOnV36rrJyX5ObNyZJ8j93i5ZjhAatpososTFFSMuj5tF0LRtK8Zwe3+f6hd31NA=="; }
            //get { return Get("TableConnection"); }
        }

        public static string BlobConnectionString
        {
            get { return Get("BlobConnection"); }
        }

        public static int ArchiveTimeGapInMinutes
        {
            get
            {
                int interval = 4 * 60;
                int.TryParse(Get("ArchiveTimeGapInMinutes"), out interval);
                return interval;
            }
        }

        public static int ArchiveRunIntervalInMinutes
        {
            get
            {
                int interval = 10;
                int.TryParse(Get("ArchiveRunIntervalInMinutes"), out interval);
                return interval;
            }
        }

        public static string LiveEmail
        {
            get { return Get("LiveEmail"); }
        }

        public static string LiveEmailPassword
        {
            get { return Get("LiveEmailPassword"); }
        }

        public static int TimeToResetCacheInMinutes
        {
            get
            {
                int interval = 240;
                int.TryParse(Get("TimeToResetCacheInMinutes"), out interval);
                return interval;
            }
        }


        public static string AppInsightsInstrumentationKey
        {
            get { return Get("AppInsightsKey"); }
        }

        public static string AppInsightsEnvironment
        {
            get { return Get("AppInsightsTag"); }
        }

        public static int NumberOfGamesToCreate
        {
            get
            {
                //int count = 1000;
                int count = 10;
              //  int.TryParse(Get("NumberOfGamesToCreate"), out count);
                return count;
            }
        }
        public static int TimeToCreateGamesInMinutes
        {
            get
            {
                int count = 10;
                //int count = 20 * 60;
             //   int.TryParse(Get("TimeToCreateGamesInMinutes"), out count);
                return count;
            }
        }

        public static int GameDurationInSeconds
        {
            get
            {
                int value = 2 * 60;
                //int count = 20 * 60;
             ///   int.TryParse(Get("GameDurationInSeconds"), out value);
                return value;
            }
        }

        public static int GapBetweenTwoGamesInSeconds
        {
            get
            {
                int value = 40;
                //int count = 20 * 60;
                //int.TryParse(Get("GapBetweenTwoGamesInSeconds"), out value);
                return value;
            }
        }

    }
}
