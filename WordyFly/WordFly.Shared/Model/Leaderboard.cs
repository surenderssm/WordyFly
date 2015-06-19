//

namespace WordFly.Shared.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Handle the Profile of the User in Game Play
    /// </summary>
    public class LeaderBoard
    {
        /// <summary>
        /// GameID of the Game whose LeaderBoard is Presented
        /// </summary>
        public Guid GameID { get; set; }

        /// <summary>
        /// List of Profiles participated in the Game
        /// </summary>
        List<Profile> GameProfiles { get;set;}
    }
}
