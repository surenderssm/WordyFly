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
        private Storage.GameTransactionManager gameTransactionManager = null;

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


        /// <summary>
        /// Gets the GameState of the given ID
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public GameState GetGameState(string gameID)
        {
            GameState state = null;
           
            if (currentGame != null && currentGame.ID.Equals(gameID, StringComparison.OrdinalIgnoreCase))
            {
                state = currentGame.States;
            }
            else
            {
              
                // TODO : Get from the DB
                // THis is just to fail safe for the devlopment purpose
                if (currentGame != null)
                {
                    state = currentGame.States;
                }
            }
            return state;
        }

        private GameSession GetGameFromTimeStamp(DateTime timeStamp)
        {
            try
            {
                gameTransactionManager = new Storage.GameTransactionManager();
                var gameStoreEntity = gameTransactionManager.GetGame(timeStamp);
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