using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.Shared.Model;

namespace WordFly.AzureStorageAccessLayer.Entities
{
    /// <summary>
    /// Entity of the GameStoer abstraction
    /// </summary>
    public class GameStoreEntity
    {
        /// <summary>
        /// GameEntity 
        /// </summary>
        public GameEntity GameEntityObject;

        /// <summary>
        /// Game GameStateEntity
        /// </summary>
        public GameStateEntity GameStateEntityObject;
    }

    /// <summary>
    /// Entity of the GameState
    /// </summary>
   public class GameStateEntity
    {
        public string ParentGameID { get; set; }
        public List<GameAtomicState> GameStates;
    }
}
