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
        /// GetGames
        /// </summary>
        /// <returns></returns>
        public List<GameEntity> GetGameEntities(GameEntityStatus gameStatus, int? numberOfGames = null)
        {
            LoadTable(GameTableName);
            TableQuery<GameEntity> query;
            if (gameStatus == GameEntityStatus.Undefined)
            {
                query = new TableQuery<GameEntity>().Take(numberOfGames);
            }
            else
            {
                query = new TableQuery<GameEntity>().Where(TableQuery.GenerateFilterConditionForInt("GameStatus", QueryComparisons.Equal, (int)(gameStatus))).Take(numberOfGames);

            }
            var result = EntityTable.ExecuteQuery(query).ToList();
            return result;
        }

        /// <summary>
        /// Get the Current Game
        /// </summary>
        /// <returns></returns>
        public GameStoreEntity GetCurrentGame(DateTime timeStamp)
        {
            GameStoreEntity gameStoreEntity = new GameStoreEntity();
            LoadTable(GameTableName);
            TableQuery<GameEntity> query = new TableQuery<GameEntity>()
                .Where(TableQuery.CombineFilters(
                    TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, StorageUtility.DayPartitionKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterConditionForDate("StartTime", QueryComparisons.LessThanOrEqual, timeStamp)
                    ),
                    TableOperators.And,
                    TableQuery.GenerateFilterConditionForDate("EndTime", QueryComparisons.GreaterThanOrEqual, timeStamp)
                    ));
            var result = EntityTable.ExecuteQuery(query).ToList();
            var entity = result.First();
            var gameState = GetGameState(entity.ID.ToString());

            gameStoreEntity.GameEntityObject = entity;
            gameStoreEntity.GameStateEntityObject = gameState;

            return gameStoreEntity;
        }
    }
}
