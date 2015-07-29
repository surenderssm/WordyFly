
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordyFlyWPClient.DataModel;
using WordFly.ServiceClientMe;

namespace WordyFlyWPClient.Common
{
    public static class GameManager
    {
        /// <summary>
        /// Access the GameData
        /// </summary>
        public static GameManageData GameData;

        private static WordFly.ServiceClientMe.GameRepository.Profile DummyUserProfile;
        static GameManager()
        {
            GameData = new GameManageData();
            DummyUserProfile = new WordFly.ServiceClientMe.GameRepository.Profile { UserID = "72F4C94F-F9A6-4BD1-A4AE-5951FED7439E", Score = 100, NumberOfWords = 300, UserName = "John" };
        }

        public static async Task LoadGame()
        {
            var game = await GameRepository.GetGame();
            GameData.CurrentGame = game.GamePlay;
        }

        public static async Task GetLeaderBoard()
        {
            GameRepository.LeaderboardRequest request = new GameRepository.LeaderboardRequest();

            // TODO remvove
            if (GameData.UserProfile == null)
            {
                request.GameID = "dummy";
                request.GameProfile = DummyUserProfile;
            }
            else
            {
                request.GameID = GameData.CurrentGame.ID;
                request.GameProfile = GameData.UserProfile;
            }
            var board = await GameRepository.GetLeaderBoard(request);
            GameData.LeaderBoard = board;
        }

        public static async Task SubmitScore()
        {
            GameRepository.LeaderboardRequest request = new GameRepository.LeaderboardRequest();

            // TODO remvove
            if (GameData.UserProfile == null)
            {
                request.GameID = "dummy";
                request.GameProfile = DummyUserProfile;
            }
            else
            {
                request.GameID = GameData.CurrentGame.ID;
                request.GameProfile = GameData.UserProfile;
            }
            await GameRepository.SubmitScore(request);
        }
    }
}