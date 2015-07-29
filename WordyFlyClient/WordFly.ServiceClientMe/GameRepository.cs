using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using WordFly.Shared.Model;
using Windows.Web.Http.Headers;
namespace WordFly.ServiceClientMe
{
    public enum GameStatus
    {
        Undefined,
        GameInPlay,
        GameInWordDisplay,
        GameInLeaderBoard,
    }

    public class Rootobject
    {
        public Gameplay GamePlay { get; set; }
        public object GameLeaderBoard { get; set; }
        public GameStatus StatusGamePlay { get; set; }
        public int ResponseStatus { get; set; }
        public DateTime ServerUTC { get; set; }
    }

    public class Gameplay
    {
        public int baseTime { get; set; }
        public string LogicalGroup { get; set; }
        public string ID { get; set; }
        public object Name { get; set; }
        public int CurrentState { get; set; }
        public DateTime StartTime { get; set; }
        public int GameDurationInSeconds { get; set; }
        public DateTime EndTime { get; set; }
        public object States { get; set; }
        public int NumberOfStates { get; set; }
        public int SizeOfState { get; set; }
        public float VowelsProbability { get; set; }
        public int MaximumRawCharactersRequired { get; set; }
        public int SessionJumpCounter { get; set; }
        public Queue<Masteralpha> MasterAlpha { get; set; }
    }

    public class Masteralpha
    {
        public string Name { get; set; }
        public object DisplayName { get; set; }
        public int CodeValue { get; set; }
    }


    public static class GameRepository
    {

        /// <summary>
        /// TODO: COnfigurable
        /// </summary>
        private const string gameService = "http://devwordfly.cloudapp.net/api/game/getgame";
        private const string baseServiceURI = "http://devwordfly.cloudapp.net/api/";

        private const string getGameService = baseServiceURI + "game/getgame";
        private const string getLeaderBoardService = baseServiceURI + "game/GetLeaderboard";
        private const string postScoreService = baseServiceURI + "game/PostScore";


        //private static HttpClient httpClient;
        static GameRepository()
        {
            //httpClient = new HttpClient();
        }

        public static async Task<Rootobject> GetGame()
        {

            Rootobject game = new Rootobject();
            using (HttpClient client = new HttpClient())
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                using (HttpResponseMessage response = await client.GetAsync(new Uri(getGameService)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();

                        stopWatch.Stop();

                        game = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(content);

                        game.GamePlay.baseTime = (game.ServerUTC + new TimeSpan(stopWatch.ElapsedTicks / 2) - game.GamePlay.StartTime).Seconds;
                        if (game.GamePlay.baseTime > 120 || game.GamePlay.baseTime < 0)
                        {
                            game.GamePlay.baseTime = 0;
                        }

                        for (int i = 21; i <= game.GamePlay.baseTime; i = i + 2)
                        {
                            game.GamePlay.MasterAlpha.Dequeue();
                        }
                    }
                    else
                    {
                        game = null;
                    }
                }
            }
            return game;

        }


        public class LeaderboardResponse
        {
            public Profile UserProfile { get; set; }
            public LeaderBoard LeaderBoard { get; set; }

            public string TotalParticipant
            {
                get
                {
                    if (LeaderBoard != null && LeaderBoard.Profiles != null)
                    {
                        return LeaderBoard.Profiles.Count.ToString();
                    }
                    return string.Empty;
                }
            }

            public string PercentageRank
            {
                get
                {
                    if (LeaderBoard != null && LeaderBoard.Profiles != null && UserProfile != null)
                    {
                        var relativeRank = Math.Round((UserProfile.Rank / Convert.ToDouble(TotalParticipant)) * 100, 2);
                        return relativeRank.ToString();
                    }
                    return string.Empty;
                }
            }
        }

        public class Profile
        {


            public string UserName
            {
                get;
                set;

            }

            public string UserID
            {
                get;
                set;

            }

            public long Rank
            {
                get;
                set;

            }

            public long Score
            {
                get;
                set;

            }

            public long NumberOfWords
            {
                get;
                set;

            }
        }
        public class LeaderboardRequest
        {
            public string GameID { get; set; }
            public Profile GameProfile { get; set; }
        }


        public class LeaderBoard
        {
            /// <summary>
            /// GameID of the Game whose LeaderBoard is Presented
            /// </summary>
            public string GameID { get; set; }

            /// <summary>
            /// List of Profiles participated in the Game
            /// </summary>
            public List<Profile> Profiles { get; set; }
        }

        ////TODO: remove
        //private static LeaderboardResponse GetDummyLeaderBoard()
        //{
        //    LeaderboardResponse response = new LeaderboardResponse();

        //    var board = new LeaderBoard();
        //    var profiles = new List<Profile>();

        //    var randomGenerator = new Random();
        //    for (int i = 1; i <= 30; i++)
        //    {
        //        var randomScore = randomGenerator.Next(1, 1000);
        //        var value = Convert.ToString(i);
        //        var prof = new Profile { UserID = value, UserName = "User" + value, Score = randomScore, Rank = i, NumberOfWords = randomScore + i };
        //        profiles.Add(prof);
        //    }
        //    board.GameID = Guid.NewGuid().ToString();
        //    board.Profiles = profiles;
        //    response.LeaderBoard = board;
        //    response.UserProfile = profiles[3];
        //    return response;
        //}

        public static async Task<LeaderboardResponse> GetLeaderBoard(WordFly.ServiceClientMe.GameRepository.LeaderboardRequest request = null)
        {
            LeaderboardResponse leadResponse = new LeaderboardResponse();

            // TODO: remove
            if (request == null)
            {
                request = new LeaderboardRequest();
                request.GameID = "dummy";

                request.GameProfile = new Profile { UserID = "72F4C94F-F9A6-4BD1-A4AE-5951FED7439E" };
            }
            string requestValue = Newtonsoft.Json.JsonConvert.SerializeObject(request);

            try
            {
                var content = await HttpUtility.PostRequest(getLeaderBoardService, requestValue);
                leadResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LeaderboardResponse>(content);
            }
            catch (Exception)
            {

                throw;
            }

            return leadResponse;
        }

        public static async Task SubmitScore(WordFly.ServiceClientMe.GameRepository.LeaderboardRequest request = null)
        {
            LeaderboardResponse leadResponse = new LeaderboardResponse();
            string requestValue = Newtonsoft.Json.JsonConvert.SerializeObject(request);

            try
            {
                var content = await HttpUtility.PostRequest(postScoreService, requestValue);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}




