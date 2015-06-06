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

        public ManageGame()
        {
            currentSession = null;
        }
        public GameSession GetGame()
        {
            if (currentSession == null)
            {
                currentSession = GameFactory.GetNew();
            }

            return currentSession;
        }

        // NEw Game
        // ManageGame
    }
}
