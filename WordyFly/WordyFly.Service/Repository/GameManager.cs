using System;
using System.Collections.Concurrent;
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
        private LeaderBoard currentLeaderBoard;
        private bool isLeaderboardComplete=false;
        //private GameSession nextGame;

        public static GameManager GameMangerObject = new GameManager();
        private Storage.GameStorageAccess gameStorageAccess = null;
        private Storage.GameTransactionManager gameTransactionManager = null;

        private DateTime lastGameCreated;

        public GameManager()
        {
            GetGame();
        }
        public void GameInit()
        {
            currentGame = new GameSession();
            currentLeaderBoard = new LeaderBoard();
            isLeaderboardComplete = false;
        }

        /// <summary>
        /// Get the Game
        /// </summary>
        /// <returns></returns>
        public GameSession GetGame()
        {
            DateTime timeStamp = DateTime.UtcNow;

            // Logic to Poll from Cache for Active Games

            if (currentGame == null || currentGame.EndTime < timeStamp)
            {
                GameInit();
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
        public void SubmitScore(LeaderboardRequest request)
        {
            if(GameMangerObject.currentGame.ID.Equals(request.GameID))
            {
                if (GameMangerObject.currentLeaderBoard.Profiles == null)
                {
                    GameMangerObject.currentLeaderBoard.Profiles = new ConcurrentBag<Profile>();
                }

                GameMangerObject.currentLeaderBoard.Profiles.Add(request.GameProfile);
            }
        }
        public void CalculateRank()
        {
            GameMangerObject.currentLeaderBoard.Profiles = new ConcurrentBag<Profile>(GameMangerObject.currentLeaderBoard.Profiles.OrderByDescending(profile => profile.Score));
            for(int i=0;i<GameMangerObject.currentLeaderBoard.Profiles.Count;i++)
            {
                GameMangerObject.currentLeaderBoard.Profiles.ElementAt(i).Rank = i + 1;
            }
        }
        public LeaderboardResponse GetLeaderboard(LeaderboardRequest request)
        {
            if (!GameMangerObject.isLeaderboardComplete)
            {
                CalculateRank();
                GameMangerObject.isLeaderboardComplete = false;
            }
            LeaderboardResponse response = new LeaderboardResponse();
            if (GameMangerObject.currentGame.ID.Equals(request.GameID))
            {
                response.LeaderBoard = GameMangerObject.currentLeaderBoard;
                if (GameMangerObject.currentLeaderBoard.Profiles.Where(profile => profile.UserID.Equals(request.GameProfile.UserID)).Count() > 0)
                {
                    response.UserProfile = GameMangerObject.currentLeaderBoard.Profiles.Where(profile => profile.UserID.Equals(request.GameProfile.UserID)).FirstOrDefault();
                }
                else
                {
                    response.UserProfile = new Profile();
                    response.UserProfile.UserID = request.GameProfile.UserID;
                    response.UserProfile.UserName = request.GameProfile.UserName;
                    response.UserProfile.NumberOfWords = request.GameProfile.NumberOfWords;
                    response.UserProfile.Score = request.GameProfile.Score;
                    response.UserProfile.Rank = GameMangerObject.currentLeaderBoard.Profiles.Count + 1;
                }
            }
            return response;
        }
    }
}