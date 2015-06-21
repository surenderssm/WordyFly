using System;
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
        public GameResponse Get()
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
                return gameResponse;
            }
            catch (Exception ex)
            {
                gameResponse.ResponseStatus = 1;
                WordFly.Common.Logger.Log(ex.ToString(), WordFly.Common.Logger.LogTypes.Exception);
                Trace.TraceInformation(ex.ToString());
                // TODO:   
            }
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
