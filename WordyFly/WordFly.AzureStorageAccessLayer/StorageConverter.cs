using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WordFly.Shared.Model;

namespace WordFly.AzureStorageAccessLayer
{
    public static class StorageConverter
    {
        public static Entities.GameEntity GetStorageGame(GameSession gameSession)
        {
            Entities.GameEntity gameEntity = new Entities.GameEntity();

            gameEntity.PartitionKey = StorageUtility.DatePartitionKey;
            gameEntity.ID = gameSession.ID;
            gameEntity.RowKey = gameEntity.ID.ToString();
            gameEntity.MasterAlpha = JsonConvert.SerializeObject(gameSession.MasterAlpha);
            gameEntity.States = JsonConvert.SerializeObject(gameSession.States);
            gameEntity.SessionJumpCounter = gameSession.SessionJumpCounter;
            gameEntity.NumberOfStates = gameSession.NumberOfStates;
            gameEntity.SizeOfState = gameSession.SizeOfState;
            gameEntity.CurrentState = gameSession.CurrentState;
            gameEntity.StartTime = gameSession.StartTime;
            gameEntity.EndTime = gameSession.EndTime;
            return gameEntity;
        }

        public static GameSession GetGameSession(Entities.GameEntity gameEntity)
        {
            GameSession gameSession = new GameSession();

            gameSession.ID = gameEntity.ID;
            gameSession.MasterAlpha = JsonConvert.DeserializeObject<List<AtomicAlpha>>(gameEntity.MasterAlpha);
            gameSession.States = JsonConvert.DeserializeObject<List<GameState>>(gameEntity.States);

            gameSession.SessionJumpCounter = gameEntity.SessionJumpCounter;
            gameSession.NumberOfStates = gameEntity.NumberOfStates;
            gameSession.SizeOfState = gameEntity.SizeOfState;
            gameSession.CurrentState = gameEntity.CurrentState;
            gameSession.StartTime = gameEntity.StartTime;
            gameSession.EndTime = gameEntity.EndTime;
            return gameSession;
        }
    }
}
