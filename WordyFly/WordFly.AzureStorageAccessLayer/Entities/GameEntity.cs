using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.AzureStorageAccessLayer.Entities
{

    public class GameEntity : StoreEntityBase
    {
        public string ID { get { return base.RowKey; } set { base.RowKey = value; } }
        public string GameType { get; set; }
        public long Duration { get; set; }

        // DateTime.MinValue can not be stored in tableStorage due to lack of support
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int NumberOfStates { get; set; }
        public int CurrentState { get; set; }
        public int SizeOfState { get; set; }
        public int SessionJumpCounter { get; set; }
        //JSON of Master ALpha
        public string MasterAlpha { get; set; }

        // JSON of States (up to 64KB in size) (Number of states * Max words <64 KB)
        // Blob path of the State stored
        public string States { get; set; }

        public int GameStatus { get; set; }

        // ID of the Source Game
        public string SourceRepoGameID { get; set; }
    }

    /// <summary>
    /// State of the Game in the Storage
    /// </summary>
    public enum GameEntityStatus
    {
        Undefined,
        NotPlayed,
        Played,
        Current,
        Invalid
    }
}
