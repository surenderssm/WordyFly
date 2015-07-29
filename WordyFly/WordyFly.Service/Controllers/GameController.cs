using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WordFly.Game;
using WordFly.Shared.Model;

namespace WordyFly.Service.Controllers
{
    public class GameController : ApiController
    {

        public GameController()
        {

        }
        // GET api/values
        public GameResponse GetGame()
        {
            GameResponse gameResponse = new GameResponse();
            try
            {
                // TODO: Playing
                gameResponse.GamePlay = Repository.GameManager.GameMangerObject.GetGame();
                // Removing States from the Get game
                gameResponse.GamePlay.States = null;
                gameResponse.StatusGamePlay = GameStatus.GameInPlay;
                gameResponse.GameLeaderBoard = null;
                gameResponse.ResponseStatus = 0;
            }
            catch (Exception ex)
            {
                gameResponse.ResponseStatus = 1;
                WordFly.Common.Logger.Log(ex.ToString(), WordFly.Common.Logger.LogTypes.Exception);
                Trace.TraceInformation(ex.ToString());
                // TODO:   
            }
            gameResponse.ServerUTC = DateTime.UtcNow;
            return gameResponse;
        }

        /// <summary>
        /// Get Game States ie Valid Wordsd
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public GameState GetGameState(string gameID)
        {
            GameState gameStates = new GameState();
            try
            {
                if (string.IsNullOrWhiteSpace(gameID))
                {

                }
                gameStates = Repository.GameManager.GameMangerObject.GetGameState(gameID);


            }
            catch (Exception ex)
            {
                WordFly.Common.Logger.Log(ex.ToString(), WordFly.Common.Logger.LogTypes.Exception);
                Trace.TraceInformation(ex.ToString());
                // TODO:   
            }
            return gameStates;
        }


        [HttpPost]
        /// <summary>
        /// Submit the score of a user
        /// </summary>
        /// <param name="request"></param>
        public void PostScore([FromBody]LeaderboardRequest request)
        {
            try
            {
                Repository.GameManager.GameMangerObject.SubmitScore(request);

            }
            catch (Exception ex)
            {
                WordFly.Common.Logger.Log(ex.ToString(), WordFly.Common.Logger.LogTypes.Exception);
                Trace.TraceInformation(ex.ToString());
                // TODO:   
            }
        }

        [HttpPost]
        public LeaderboardResponse GetLeaderboard([FromBody]LeaderboardRequest request)
        {
            LeaderboardResponse response = new LeaderboardResponse();
            try
            {
                if (request.GameID.Equals("dummy", StringComparison.OrdinalIgnoreCase))
                {
                    return GetDummyLeaderBoard();
                }
                response = Repository.GameManager.GameMangerObject.GetLeaderboard(request);
            }
            catch (Exception ex)
            {
                WordFly.Common.Logger.Log(ex.ToString(), WordFly.Common.Logger.LogTypes.Exception);
                Trace.TraceInformation(ex.ToString());
                // TODO:   
            }
            return response;
        }

        //TODO: remove
        private static LeaderboardResponse GetDummyLeaderBoard()
        {
            LeaderboardResponse response = new LeaderboardResponse();

            var board = new LeaderBoard();
            var profiles = new ConcurrentBag<Profile>();

            var randomGenerator = new Random();
            for (int i = 1; i <= 30; i++)
            {
                var randomScore = randomGenerator.Next(1, 1000);
                var value = Convert.ToString(i);
                var prof = new Profile { UserID = value, UserName = "User" + value, Score = randomScore, Rank = i, NumberOfWords = randomScore + i };
                profiles.Add(prof);
            }
            board.GameID = Guid.NewGuid().ToString();
            board.Profiles = profiles;
            response.LeaderBoard = board;
            response.UserProfile = profiles.ElementAt(3);
            return response;
        }

        [HttpGet]
        public DateTime Ping()
        {
            return DateTime.UtcNow;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
