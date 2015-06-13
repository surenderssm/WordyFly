using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.AzureStorageAccessLayer
{
    /// <summary>
    /// COnfiguration of the GameGenerator
    /// </summary>
    public class GameGeneratorConfig
    {
        public DateTime LastGameEndTime { get; set; }
        public int NumberOfGamesToCreate { get; set; }
        public long GameDurationInSeconds { get; set; }

        public long GapBetweenTwoGamesInSeconds { get; set; }
        public long TotalSecondsBetweenTwoGames { get; set; }

    }
}
