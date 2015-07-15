//

namespace WordFly.Shared.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// GameResponse 
    /// </summary>
    public class GameResponse
    {
        /// <summary>
        /// Going to Contain the Game (Current or Next)
        /// </summary>
        public GameSession GamePlay;

        /// <summary>
        /// Leader of the Game
        /// </summary>
        public LeaderBoard GameLeaderBoard;

        /// <summary>
        /// Status of the Game
        /// If GameStatus: LeaderBoard : > GamePlay going to contain the New Game
        /// </summary>
        public GameStatus StatusGamePlay;

        public int ResponseStatus { get; set; }

        public DateTime? ServerUTC { get; set; }
    }
}