using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.Game.Model;

namespace WordFly.Game
{
    // Co-ordinate the Game
    public class GameCoordinator
    {
        GameSession currentGame;

        public GameCoordinator()
        {
            // TODO:surender remove only for POC
            currentGame = new GameSession();
        }

        private GameSession CreateGame()
        {

            return currentGame;
        }

    }
}
