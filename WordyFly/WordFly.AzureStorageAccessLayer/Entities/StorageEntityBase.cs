using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.AzureStorageAccessLayer.Entities
{
    /// <summary>
    /// Base of Entities
    /// </summary>
    [Serializable]
    public abstract class StoreEntityBase : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {
        public StoreEntityBase() { }
    }
}

