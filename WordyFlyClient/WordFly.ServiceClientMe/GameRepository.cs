using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using WordFly.Shared.Model;
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
    }

    public class Gameplay
    {
        public string LogicalGroup { get; set; }
        public Guid ID { get; set; }
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


    public class GameRepository
    {
        /// <summary>
        /// TODO: COnfigurable
        /// </summary>
        private const string GameService = "http://c9035eadd5894f2b876da2ffa6b423cd.cloudapp.net/api/game";

        public static async Task<Rootobject> GetGame()
        {
            Rootobject game = new Rootobject();
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(new Uri(GameService)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();

                        game = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(content);
                    }
                }
            }
            return game;
        }
    }
}

