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
    public class Profile
    {
        public string UserName { get; set; }

        public string UserID { get; set; }

        public long Rank { get; set; }

        public long Score { get; set; }

        public long NumberOfWords { get; set; }
    }
}