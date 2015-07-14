using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WordFly.AzureStorageAccessLayer.Entities;

namespace WordFly.AzureStorageAccessLayer
{
    /// <summary>
    /// SRP of handling all the Utitlies functions and properties
    /// </summary>
    public static class StorageUtility
    {
        /// <summary>
        /// Date basis partiniton key
        /// </summary>
        public static string DayPartitionKey
        {
            get
            {

                return DateTime.UtcNow.Date.Day.ToString();
            }
        }
        public static string GetDayPartitionKey(DateTime datetime)
        {
            return datetime.Date.Day.ToString();
        }

        public static string GetRowKeyFromTicks(DateTime datetime)
        {
            return string.Format("{0:D19}", DateTime.MaxValue.Ticks - datetime.Ticks);
        }

        public static GameGeneratorConfig GetGameGeneratorConfig()
        {
            GameGeneratorConfig gameGeneratorConfig = null;

            string gameGeneratorKey = "GameGeneratorConfig";

            MiscStorgaeAccess storageAceess = new MiscStorgaeAccess();

            try
            {
                GameConfig config = storageAceess.GetGameConfig(gameGeneratorKey);
                gameGeneratorConfig = JsonConvert.DeserializeObject<GameGeneratorConfig>(config.Value);
            }
            catch
            {
                // IF fails save a new GameGeneratorCOnfig
                gameGeneratorConfig = new GameGeneratorConfig();

                gameGeneratorConfig.GameDurationInSeconds = ConfigManager.Config.GameDurationInSeconds;
                gameGeneratorConfig.GapBetweenTwoGamesInSeconds = ConfigManager.Config.GapBetweenTwoGamesInSeconds;
                gameGeneratorConfig.NumberOfGamesToCreate = ConfigManager.Config.NumberOfGamesToCreate;
                gameGeneratorConfig.LastGameEndTime = DateTime.UtcNow;
                SaveGameGeneratorConfig(gameGeneratorConfig);
            }
            return gameGeneratorConfig;


        }

        /// <summary>
        /// Saves the GameGenerator COnfig
        /// </summary>
        /// <param name="config"></param>
        public static void SaveGameGeneratorConfig(GameGeneratorConfig config)
        {
            string gameGeneratorKey = "GameGeneratorConfig";

            MiscStorgaeAccess storageAceess = new MiscStorgaeAccess();

            Entities.GameConfig gameConfig = new Entities.GameConfig();
            gameConfig.PartitionKey = "TBD";
            gameConfig.Key = gameGeneratorKey;
            gameConfig.Value = JsonConvert.SerializeObject(config);
            storageAceess.SaveGameConfig(gameConfig);
        }
    }
}
