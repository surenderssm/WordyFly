using System;
using System.Collections.Generic;
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
        public GameResponse Get()
        {
            GameResponse gameResponse = new GameResponse();

            // TODO: Playing
//            gameResponse.GamePlay = Repository.GameManager.GameMangerObject.GetGame();
            gameResponse.StatusGamePlay = GameStatus.GameInPlay;
            gameResponse.GameLeaderBoard = null;
            return gameResponse;

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
