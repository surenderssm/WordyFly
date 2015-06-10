using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.Game.Model;

namespace WordFly.Game
{
    public static class GameFactory
    {
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
