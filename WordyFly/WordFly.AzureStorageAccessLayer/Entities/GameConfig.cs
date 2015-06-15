using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.AzureStorageAccessLayer.Entities
{
    // Config of the Game
    public class GameConfig : StoreEntityBase
    {
        public string Key { get { return base.RowKey; } set { base.RowKey = value; } }
        public string Value { get; set; }
    }
}
