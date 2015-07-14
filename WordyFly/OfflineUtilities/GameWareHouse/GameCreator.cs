// WordFly

namespace GameWareHouse
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Storage = WordFly.AzureStorageAccessLayer;
    using WordFly.Shared.Model;
    using WordFly.AzureStorageAccessLayer.Entities;
    using WordFly.Game;
    using WordFly.Common;
    using System.Diagnostics;

    /// <summary>
    /// Create the Games
    /// </summary>
    public static class GameCreator
    {
        private static Storage.GameStorageAccess gameStorageAccess;

        /// <summary>
        /// Creates the Game in GameRepositoryTableName
        /// </summary>
        /// <param name="numberOfGames"></param>
        public static void CreateGames(int numberOfGames)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            try
            {
                gameStorageAccess = new Storage.GameStorageAccess(Storage.Constants.GameRepositoryTableName);
                // TODO: think for parallel approach
                for (int index = 0; index < numberOfGames; index++)
                {
                    Stopwatch gameWatch = new Stopwatch();
                    gameWatch.Start();
                    GameSession gameSession = GameFactory.GetGame(GameType.Normal);
                    gameSession.LogicalGroup = Storage.StorageUtility.DayPartitionKey;
                    GameStoreEntity gameStoreEntity = Storage.StorageConverter.GetGameStoreEntity(gameSession);

                    while (true)
                    {
                        try
                        {
                            gameStorageAccess.SaveGame(gameStoreEntity);
                        }

                        catch (Exception ex)
                        {
                            // TODO: 
                            Logger.Log(index + ex.ToString());
                            continue;
                        }
                        break;
                    }
                    gameWatch.Stop();
                    string message = string.Format("Game #{2} Created : ID : {0} , Time Taken : {1}", gameSession.ID, gameWatch.ElapsedMilliseconds, index);
                    Logger.Log(message);
                }

            }
            catch (Exception ex)
            {
                WordFly.Common.Logger.Log(ex.ToString(), Logger.LogTypes.Exception);
                throw new WordFly.Common.Exceptions.CreateGameFailedException("Not able to create Game !" + ex.ToString());
            }
            finally
            {
                watch.Stop();
                string message = string.Format("Time Taken to Create Games {0} : {1}", numberOfGames, watch.ElapsedMilliseconds);
                WordFly.Common.Logger.Log(message, Logger.LogTypes.Information);
            }
        }
    }
}
