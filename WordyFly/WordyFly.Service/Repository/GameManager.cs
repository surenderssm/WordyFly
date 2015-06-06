using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WordFly.Game.Model;
using WordFly.GameManager;
namespace WordyFly.Service.Repository
{
    public static class RepositoryManager
    {
        static GameSession currentSession;
        static ManageGame mGame;

        static RepositoryManager()
        {
            mGame = new ManageGame();
        }
        public static GameSession GetGame()
        {
            // Logic to Poll from Cache for Active Games
            return mGame.GetGame();
        }
    }
}