using System;
using System.Collections.Generic;
using System.Text;
using WordFly.ServiceClientMe;
using WordyFlyWPClient.Common;

namespace WordyFlyWPClient.DataModel
{
    public class GameManageData
    {
        public GameRepository.Profile UserProfile { get; set; }

        public Gameplay  CurrentGame { get; set; }

        public GameRepository.LeaderboardResponse LeaderBoard { get; set; }

        public GameStatus CurrentStatus { get; set; }
        public GameManageData()
        {
        }
    }
    public enum GameStatus
    {
        Undefined,
        GameInPlay,
        GameInWordDisplay,
        GameInLeaderBoard,
    }
}
