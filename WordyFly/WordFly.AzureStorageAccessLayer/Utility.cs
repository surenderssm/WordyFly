using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        public static string DatePartitionKey
        {
            get
            {

                return DateTime.UtcNow.Date.ToShortDateString();
            }
        }

        public static GameGeneratorConfig GetGameGeneratorConfig()
        {
            GameGeneratorConfig gameGeneratorConfig = null;

            string gameGeneratorKey = "GameGeneratorConfig";

            MiscStorgaeAccess storageAceess = new MiscStorgaeAccess();

            var config = storageAceess.GetGameConfig(gameGeneratorKey);

            try
            {
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
            gameConfig.Key = gameGeneratorKey;
            gameConfig.Value =JsonConvert.SerializeObject(config);
            storageAceess.SaveGameConfig(gameConfig);
        }
    }
}
