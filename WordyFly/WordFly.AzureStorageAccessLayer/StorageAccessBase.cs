using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Blob;

namespace WordFly.AzureStorageAccessLayer
{
    /// <summary>
    /// Base class to interact with Table Storgae
    /// </summary>
    public abstract class StorageAccessBase
    {
        public CloudStorageAccount StorageAcount { get; private set; }

        public CloudTableClient TableClient { get; private set; }

        public CloudBlobClient BlobClient { get; private set; }

        public CloudTable EntityTable { get; private set; }

        



        public bool IsTableLoaded { get; private set; }

        public StorageAccessBase()
        {
            StorageAcount = CloudStorageAccount.Parse(ConfigManager.Config.StorageAccounntConnectionString);
            TableClient = StorageAcount.CreateCloudTableClient();
            BlobClient = StorageAcount.CreateCloudBlobClient();
        }
        public bool LoadEntityTable(string entityType)
        {
            try
            {
                EntityTable = TableClient.GetTableReference(entityType);
                EntityTable.CreateIfNotExists();
                IsTableLoaded = true;
            }
            catch (Exception)
            {
                IsTableLoaded = false;
            }

            return IsTableLoaded;
        }

        public bool GetTableLoadedState(string expectedEntity)
        {
            bool loaded = false;
            if (IsTableLoaded)
            {
                loaded = EntityTable.Name == expectedEntity;
            }
            return loaded;
        }

        public void LoadTable(string tableName)
        {
            if (!GetTableLoadedState(tableName))
            {
                LoadEntityTable(tableName);
            }
            if (!IsTableLoaded)
                throw new FieldAccessException(string.Format("Cloud Table: {0} not loaded", tableName));
        }
    }
}
