using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.Shared.Model
{
    public class LeaderboardResponse
    {
        public Profile UserProfile { get; set; }
        public LeaderBoard LeaderBoard { get; set; }
    }
}
