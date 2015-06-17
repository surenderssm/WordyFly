//

namespace WordFly.Shared.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Enum to notify the state of the Game
    /// </summary>
    public enum GameStatus
    {
        Undefined,
        GameInPlay,
        GameInWordDisplay,
        GameInLeaderBoard,
    }
}