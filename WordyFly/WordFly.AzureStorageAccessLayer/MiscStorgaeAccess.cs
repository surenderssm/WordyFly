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
    public class MiscStorgaeAccess : StorageAccessBase
    {
        /// <summary>
        /// Insert or Replace the Config
        /// <param name="game"></param>
        public void SaveGameConfig(GameConfig config)
        {
            LoadTable(Constants.GameConfigTableName);
            TableOperation insertConfig = TableOperation.InsertOrReplace(config);
            base.EntityTable.Execute(insertConfig);
        }

        /// <summary>
        /// GetGameConfig
        /// </summary>
        /// <returns></returns>
        public GameConfig GetGameConfig(string key)
        {
            LoadTable(Constants.GameConfigTableName);
            TableQuery<GameConfig> query = new TableQuery<GameConfig>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, key));
            var result = EntityTable.ExecuteQuery(query).ToList();
            return result.FirstOrDefault();
        }
    }
}
