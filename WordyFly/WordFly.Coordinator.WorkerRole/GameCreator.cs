using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.Game;
using Storage = WordFly.AzureStorageAccessLayer;
using WordFly.Shared.Model;
using WordFly.Common;
namespace WordFly.Coordinator.WorkerRole
{
    /// <summary>
    /// Create the Games
    /// </summary>
    public static class GameCreator
    {
        private static Storage.GameStorageAccess gameStorageAccess;


        public static void CreateGames()
        {
            try
            {
                gameStorageAccess = new Storage.GameStorageAccess(Storage.Constants.GameRepositoryTableName);

                var gameGeneratorConfig = Storage.StorageUtility.GetGameGeneratorConfig();
                for (int index = 0; index < gameGeneratorConfig.NumberOfGamesToCreate; index++)
                {
                    DateTime gameStartTime = gameGeneratorConfig.LastGameEndTime.AddSeconds(1);

                    GameSession gameSession = GameFactory.GetGame(GameType.Normal);
                    gameSession.StartTime = gameStartTime;
                    gameSession.GameDurationInSeconds = gameGeneratorConfig.GameDurationInSeconds;
                    gameSession.EndTime = gameStartTime.AddSeconds(gameGeneratorConfig.TotalSecondsBetweenTwoGames);
                    Storage.Entities.GameEntity game = Storage.StorageConverter.GetStorageGame(gameSession);
                    gameStorageAccess.SaveGame(game);

                    gameGeneratorConfig.LastGameEndTime = gameSession.EndTime;
                    // TODO: surender think about the performance
                    Storage.StorageUtility.SaveGameGeneratorConfig(gameGeneratorConfig);
                }

            }
            catch (Exception ex)
            {
                throw new Common.Exceptions.CreateGameFailedException("Not able to create Game !" + ex.ToString());
            }
        }
    }
}
