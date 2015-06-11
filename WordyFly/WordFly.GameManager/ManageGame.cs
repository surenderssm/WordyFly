using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.Game;
using WordFly.Game.Model;
namespace WordFly.GameManager
{
    // Mange the Game
    public class ManageGame
    {
        private GameSession currentSession;

        private DateTime lastGameCreated;

        public ManageGame()
        {
            currentSession = null;
        }
        public GameSession GetGame()
        {
            //TODO: hack new game after every two minutes remove later
            if (currentSession == null || DateTime.UtcNow.Subtract(lastGameCreated).Minutes > 2)
            {
                currentSession = GameFactory.GetGame(GameType.Basic);
                lastGameCreated = DateTime.UtcNow;
            }
            return currentSession;
        }
    }
}
