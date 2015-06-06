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
        public static GameSession GetNew()
        {
            GameSession gSession = new GameSession();
            return gSession;
        }
    }
}
