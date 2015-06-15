using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.AzureStorageAccessLayer.Entities;
namespace WordFly.AzureStorageAccessLayer
{
    /// <summary>
    /// SRP of handling all the constats of AzureStorage layer
    /// </summary>
    public class GameStorageAccess : StorageAccessBase
    {
        private string GameTableName;

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
        public void SaveGame(GameEntity game)
        {
            LoadTable(GameTableName);
            TableOperation insertGame = TableOperation.InsertOrReplace(game);
            base.EntityTable.Execute(insertGame);
        }

        /// <summary>
        /// GetGames
        /// </summary>
        /// <returns></returns>
        public List<GameEntity> GetGames(GameStatus gameStatus, int? numberOfGames = null)
        {
            LoadTable(GameTableName);
            TableQuery<GameEntity> query = new TableQuery<GameEntity>().Where(TableQuery.GenerateFilterConditionForInt("GameStatus", QueryComparisons.Equal, (int)(gameStatus))).Take(numberOfGames);
            var result = EntityTable.ExecuteQuery(query).ToList();
            return result;
        }

        /// <summary>
        /// Get the Current Game
        /// </summary>
        /// <returns></returns>
        public GameEntity GetCurrentGame(DateTime timeStamp)
        {
            LoadTable(GameTableName);
            TableQuery<GameEntity> query = new TableQuery<GameEntity>()
                .Where(TableQuery.CombineFilters(
                    TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, StorageUtility.DatePartitionKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterConditionForDate("StartTime", QueryComparisons.LessThanOrEqual, timeStamp)
                    ),
                    TableOperators.And,
                    TableQuery.GenerateFilterConditionForDate("EndTime", QueryComparisons.GreaterThanOrEqual, timeStamp)
                    ));
            var result = EntityTable.ExecuteQuery(query).ToList();
            return result.FirstOrDefault();
        }
    }
}
