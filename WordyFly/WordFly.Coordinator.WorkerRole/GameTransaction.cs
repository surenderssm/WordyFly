using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.Game;
using Storage = WordFly.AzureStorageAccessLayer;
using WordFly.Shared.Model;
using WordFly.Common;
using WordFly.AzureStorageAccessLayer.Entities;
namespace WordFly.Coordinator.WorkerRole
{
    /// <summary>
    /// Create the Games
    /// </summary>
    public  class GameTransaction
    {
        private  Storage.GameTransactionManager gameTransactionManager;

        public GameTransaction()
        {
            gameTransactionManager = new Storage.GameTransactionManager();

        }
        public  void InsertGames()
        {
            try
            {
                var gameGeneratorConfig = Storage.StorageUtility.GetGameGeneratorConfig();

                DateTime gameStartTime = gameGeneratorConfig.LastGameEndTime.AddSeconds(1);
                var gameEntityStore = gameTransactionManager.GetGameEntityFromStore(1).First();
                GameEntity gameEntity = new GameEntity();

                // For the Reference
                gameEntity.SourceRepoGameID = gameEntityStore.ID;
                gameEntity.NumberOfStates = gameEntityStore.NumberOfStates;
                gameEntity.SessionJumpCounter = gameEntityStore.SessionJumpCounter;
                gameEntity.SizeOfState = gameEntityStore.SizeOfState;

                gameEntity.StartTime = gameStartTime;
                gameEntity.RowKey = Storage.StorageUtility.GetRowKeyFromTicks(gameStartTime);
                gameEntity.ID = Guid.NewGuid();
                gameEntity.PartitionKey = Storage.StorageUtility.GetDayPartitionKey(gameStartTime);
                gameEntity.Duration = gameGeneratorConfig.GameDurationInSeconds;

                gameEntity.EndTime = gameStartTime.AddSeconds(gameEntity.Duration + gameGeneratorConfig.TotalSecondsBetweenTwoGames);

                
                gameTransactionManager.SaveGame(gameEntity);

                gameGeneratorConfig.LastGameEndTime = gameEntity.EndTime.Value;
                // TODO: surender think about the performance
                Storage.StorageUtility.SaveGameGeneratorConfig(gameGeneratorConfig);

            }
            catch (Exception ex)
            {
                throw new Common.Exceptions.CreateGameFailedException("Not able to InsertGames!" + ex.ToString());
            }
        }
    }
}
