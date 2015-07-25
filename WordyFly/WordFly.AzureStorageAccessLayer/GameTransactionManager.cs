// WordyFly

namespace WordFly.AzureStorageAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WordFly.AzureStorageAccessLayer.Entities;
    using WordFly.Common.Exceptions;


    public class GameTransactionManager
    {
        private GameStorageAccess gameRepoStorageAccess;
        private GameStorageAccess gameTranStorageAccess;
        private Dictionary<int, string> randomGameMap;
        private Random randomGenerator;

        public GameTransactionManager()
        {
            gameRepoStorageAccess = new GameStorageAccess(Constants.GameRepositoryTableName);
            gameTranStorageAccess = new GameStorageAccess(Constants.GameDayStoreTableName);
            randomGameMap = new Dictionary<int, string>();
            randomGenerator = new Random();
        }

        private void FillRandomGameMap()
        {
            var rowKeys = gameRepoStorageAccess.GetGameRowKeys();

            for (int index = 0; index < rowKeys.Count; index++)
            {
                randomGameMap.Add(index, rowKeys[index]);
            }
        }

        private List<string> GetRandomGameID(int count)
        {
            if (randomGameMap.Count == 0)
            {
                FillRandomGameMap();
            }

            List<string> randomIds = new List<string>();
            for (int index = 0; index < count; index++)
            {
               randomIds.Add(randomGameMap[randomGenerator.Next(0, randomGameMap.Count)]);    
            }
            return randomIds;
        }

        public GameStoreEntity GetGame(DateTime timeStamp)
        {
            try
            {
                var gEntity = gameTranStorageAccess.GetGameEntity(timeStamp);
                var baseGEntity = gameRepoStorageAccess.GetGameEntity(gEntity.SourceRepoGameID.ToString());
                gEntity.MasterAlpha = baseGEntity.MasterAlpha;
                var gState = gameTranStorageAccess.GetGameState(gEntity.SourceRepoGameID.ToString());
                GameStoreEntity gameStoreEntity = new GameStoreEntity { GameEntityObject = gEntity, GameStateEntityObject = gState };
                return gameStoreEntity;
            }
            catch (Exception ex)
            {
                throw new GameNotFoundException(ex.ToString());
            }
        }

        /// <summary>
        /// Get Random Entities form Repository Store
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<GameEntity> GetGameEntityFromStore(int count)
        {
            try
            {
                // TODO improve
                var rowKeys = GetRandomGameID(count);
                List<GameEntity> entities = new List<GameEntity>();
                rowKeys.ForEach(key => {
                    entities.Add(gameRepoStorageAccess.GetGameEntity(key));
                });

                return entities;
            }
            catch (Exception ex)
            {
                throw new GameNotFoundException(ex.ToString());
            }
        }
        public void SaveGame(GameEntity gameEntity)
        {
            gameTranStorageAccess.SaveGameEntity(gameEntity);
        }
    }
}
