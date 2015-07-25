using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.Shared.Model
{
    public class LeaderboardRequest
    {
        public string GameID { get; set; }
        public Profile GameProfile { get; set; }
    }
}
