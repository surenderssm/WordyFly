using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using WordFly.Shared.Model;
namespace WordFly.ServiceClientMe
{

    public class GameRepository
    {
        /// <summary>
        /// TODO: COnfigurable
        /// </summary>
        private const string GameService = "http://c9035eadd5894f2b876da2ffa6b423cd.cloudapp.net/api/game";

        public static async Task<GameResponse> GetGame()
        {
            GameResponse game = new GameResponse();
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(new Uri(GameService)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                  
                        game = Newtonsoft.Json.JsonConvert.DeserializeObject<GameResponse>(content);

                        var firstAlpha = game.GamePlay.MasterAlpha[0].DisplayName;


                    }
                }
            }
            return game;
        }
    }
}

