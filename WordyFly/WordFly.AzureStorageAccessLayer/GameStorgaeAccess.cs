using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.AzureStorageAccessLayer.Entities;
using WordFly.Shared.Model;
using Newtonsoft.Json;
using WordFly.Common.Exceptions;

namespace WordFly.AzureStorageAccessLayer
{
    /// <summary>
    /// SRP of handling all the 
    /// </summary>
    public class GameStorageAccess : StorageAccessBase
    {
        private string GameTableName;
        /// <summary>
        /// Contatiner where the state of teh games is stored
        /// </summary>
        private const string gameStatesContainer = "gamestates";

        /// <summary>
        /// COntrol the Table SOurce from here (gamerepository,ActiveGame,ArchiveGame)
        /// </summary>
        /// <param name="gameTableName"></param>
        public GameStorageAccess(string gameTableName)
        {
            GameTableName = gameTableName;
        }

        /// <summary>
        /// Insert or Replace the Game
        /// </summary>
        /// <param name="game"></param>
        /// <param name="gameState">If null, then GameStae is not Stored in Blob, (Only update Scenario)</param>
        public void SaveGame(GameStoreEntity gameStoreEntity, bool ignoreGameStates = false)
        {
            try
            {
                if (!ignoreGameStates)
                {
                    SaveGameState(gameStoreEntity.GameStateEntityObject);

                }
                SaveGameEntity(gameStoreEntity.GameEntityObject);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }

        public void SaveGameEntity(GameEntity entity)
        {
            LoadTable(GameTableName);
            TableOperation insertGame = TableOperation.InsertOrReplace(entity);
            base.EntityTable.Execute(insertGame);
        }
        /// <summary>
        /// Store the Game state in Blob storage
        /// </summary>
        /// <param name="states"></param>
        public void SaveGameState(GameStateEntity gameStateEntity)
        {
            CloudBlobContainer stateContainer = BlobClient.GetContainerReference(gameStatesContainer);
            stateContainer.CreateIfNotExists();
            CloudBlockBlob blockBlob = stateContainer.GetBlockBlobReference(gameStateEntity.ParentGameID);
            blockBlob.UploadText(JsonConvert.SerializeObject(gameStateEntity));
        }

        /// <summary>
        /// Store the Game state in Blob storage
        /// </summary>
        /// <param name="states"></param>
        public GameStateEntity GetGameState(string gameID)
        {
            CloudBlobContainer stateContainer = BlobClient.GetContainerReference(gameStatesContainer);
            CloudBlockBlob blockBlob = stateContainer.GetBlockBlobReference(gameID);
            var result = blockBlob.DownloadText();
            GameStateEntity gameState;
            gameState = JsonConvert.DeserializeObject<GameStateEntity>(result);
            return gameState;
        }

       

       
        /// <summary>
        /// GetRow keys of all the game
        /// </summary>
        /// <returns></returns>
        public List<string> GetGameRowKeys()
        {
            LoadTable(GameTableName);

            List<string> rowKeys = new List<string>();
            // Define the query, and only select the Email property
            TableQuery<DynamicTableEntity> projectionQuery = new TableQuery<DynamicTableEntity>().Select(new string[] { "RowKey" });
            // Define an entity resolver to work with the entity after retrieval.
            EntityResolver<string> resolver = (pk, rk, ts, props, etag) => rk;

            foreach (string rowKey in EntityTable.ExecuteQuery(projectionQuery, resolver, null, null))
            {
                rowKeys.Add(rowKey);
            }
            return rowKeys;
        }

        /// <summary>
        /// GetGames Entities Random order
        /// </summary>
        /// <returns></returns>
        public GameEntity GetGameEntity(string rowKey)
        {
            LoadTable(GameTableName);
            TableQuery<GameEntity> query;
            List<GameEntity> entities = new List<GameEntity>();
            query = new TableQuery<GameEntity>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey));
            var resultEntities = EntityTable.ExecuteQuery(query).ToList();
            return resultEntities.FirstOrDefault();
        }
        /// <summary>
        /// Get the Current Game
        /// </summary>
        /// <returns></returns>
        public GameEntity GetGameEntity(DateTime timeStamp)
        {
            GameEntity gameEntity = null;
            LoadTable(GameTableName);

            // TODO: Surender use partition key to get the Data for Performance optimization
            //TableQuery<GameEntity> query = new TableQuery<GameEntity>()
            //    .Where(TableQuery.CombineFilters(
            //        TableQuery.CombineFilters(
            //        TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, StorageUtility.DayPartitionKey),
            //        TableOperators.And,
            //        TableQuery.GenerateFilterConditionForDate("StartTime", QueryComparisons.LessThanOrEqual, timeStamp)
            //        ),
            //        TableOperators.And,
            //        TableQuery.GenerateFilterConditionForDate("EndTime", QueryComparisons.GreaterThanOrEqual, timeStamp)
            //        ));

            TableQuery<GameEntity> query = new TableQuery<GameEntity>()
                .Where(TableQuery.CombineFilters(
                    TableQuery.GenerateFilterConditionForDate("StartTime", QueryComparisons.LessThanOrEqual, timeStamp),
                    TableOperators.And,
                    TableQuery.GenerateFilterConditionForDate("EndTime", QueryComparisons.GreaterThanOrEqual, timeStamp)
                    ));

            var result = EntityTable.ExecuteQuery(query).ToList();

            if (result.Count == 0)
            {
                throw new GameNotFoundException(string.Format("No game present for TimeStamp {0}", timeStamp));
            }

            gameEntity = result.First();

            // Get the game with least time Raise Alert for INconsistent Data
            // Inconsiteny Failed
            if (result.Count > 1)
            {
                result.ForEach(item =>
                {
                    if (item.StartTime < gameEntity.StartTime)
                    {
                        gameEntity = item;
                    }
                });
            }
            return gameEntity;
        }


        /// <summary>
        /// Get the Current Game
        /// </summary>
        /// <returns></returns>
        public GameStoreEntity GetGame(DateTime timeStamp)
        {
            var gEntity = GetGameEntity(timeStamp);
            var gameState = GetGameState(gEntity.ID.ToString());

            GameStoreEntity gameStoreEntity = new GameStoreEntity();
            gameStoreEntity.GameEntityObject = gEntity;
            gameStoreEntity.GameStateEntityObject = gameState;
            return gameStoreEntity;
        }

      
    }
}
