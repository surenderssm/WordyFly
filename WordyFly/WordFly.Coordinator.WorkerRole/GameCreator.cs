using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.Game;
using Storage = WordFly.AzureStorageAccessLayer;
using WordFly.Shared.Model;
using WordFly.Common;
using WordFly.AzureStorageAccessLayer.Entities;
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

                    //GameSession gameSession = new GameSession();
                    GameSession gameSession = GameFactory.GetGame(GameType.Normal);
                    gameSession.LogicalGroup = Storage.StorageUtility.DayPartitionKey;
                    gameSession.StartTime = gameStartTime;
                    gameSession.GameDurationInSeconds = gameGeneratorConfig.GameDurationInSeconds;
                    gameSession.EndTime = gameStartTime.AddSeconds(gameGeneratorConfig.GameDurationInSeconds + gameGeneratorConfig.TotalSecondsBetweenTwoGames);

                    GameStoreEntity gameStoreEntity = Storage.StorageConverter.GetGameStoreEntity(gameSession);

                    while (true)
                    {
                        try
                        {
                            gameStorageAccess.SaveGame(gameStoreEntity);
                        }

                        catch (Exception ex)
                        {
                            Common.Logger.Log(index +  ex.ToString());
                            continue;
                        }
                        break;
                    }
                    gameGeneratorConfig.LastGameEndTime = (DateTime)gameSession.EndTime;
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
