using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.Game.Model;

namespace WordFly.Game
{
    /// <summary>
    /// Gets teh Game without Start,End and Duration parameter of the game
    /// </summary>
    public static class GameFactory
    {
        /// <summary>
        /// Get the same without Start,End,Duration
        /// </summary>
        /// <param name="gameType"></param>
        /// <returns></returns>
        public static GameSession GetGame(GameType gameType)
        {
            GameGenrator genrator = new GameGenrator();

            GameSession game = null;
            // TODO : types of Game
            switch (gameType)
            {
                default:
                    game = genrator.CreateNewGame(gameType);
                    break;
            }
            return game;
        }
    }
}
