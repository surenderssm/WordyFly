using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WordFly.Shared.Model;

using Storage = WordFly.AzureStorageAccessLayer;
namespace WordyFly.Service.Repository
{
    public class GameManager
    {
        private GameSession currentGame;
        //private GameSession nextGame;

        public static GameManager GameMangerObject = new GameManager();
        private Storage.GameStorageAccess gameStorageAccess = null;

        private DateTime lastGameCreated;

        public GameManager()
        {
            
        }

        /// <summary>
        /// Get the Game
        /// </summary>
        /// <returns></returns>
        public GameSession GetGame()
        {
            DateTime timeStamp = DateTime.UtcNow;

            // Logic to Poll from Cache for Active Games

            if (currentGame == null)
            {
                currentGame = new GameSession();

                lock (currentGame)
                {
                    currentGame = GetGameFromTimeStamp(timeStamp);
                }
            }
            else if (currentGame.EndTime < timeStamp)
            {
                lock (currentGame)
                {
                    currentGame = GetGameFromTimeStamp(timeStamp);
                }

            }
            return currentGame;
        }

        private GameSession GetGameFromTimeStamp(DateTime timeStamp)
        {
            try
            {
                gameStorageAccess = new Storage.GameStorageAccess(Storage.Constants.GameRepositoryTableName);
                var gameStoreEntity = gameStorageAccess.GetCurrentGame(timeStamp);
                var gameSession = Storage.StorageConverter.GetGameSession(gameStoreEntity);
                return gameSession;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}