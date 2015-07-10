using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WordFly.Shared.Model;
using WordFly.AzureStorageAccessLayer.Entities;

namespace WordFly.AzureStorageAccessLayer
{
    public static class StorageConverter
    {
        /// <summary>
        /// Gets teh GameStoreEntity from GameSession
        /// </summary>
        /// <param name="gameSession"></param>
        /// <returns></returns>
        public static GameStoreEntity GetGameStoreEntity(GameSession gameSession)
        {
            GameStoreEntity gameStoreEntity = new GameStoreEntity();
            gameStoreEntity.GameEntityObject = GetGameEntity(gameSession);
            gameStoreEntity.GameStateEntityObject = GetGameStateEntity(gameSession);
            return gameStoreEntity;
        }

        /// <summary>
        /// Gets teh GameSession
        /// </summary>
        /// <param name="gameStoreEntity"></param>
        /// <returns></returns>
        public static GameSession GetGameSession(Entities.GameStoreEntity gameStoreEntity)
        {
            Entities.GameEntity gameEntity = gameStoreEntity.GameEntityObject;

            GameSession gameSession = new GameSession();
            gameSession.ID = gameEntity.ID;
            gameSession.LogicalGroup = gameEntity.PartitionKey;
            gameSession.MasterAlpha = JsonConvert.DeserializeObject<List<AtomicAlpha>>(gameEntity.MasterAlpha);
            gameSession.NumberOfStates = gameEntity.NumberOfStates;
            gameSession.SizeOfState = gameEntity.SizeOfState;
            gameSession.CurrentState = gameEntity.CurrentState;
            gameSession.StartTime = gameEntity.StartTime.HasValue ? gameEntity.StartTime.Value : DateTime.MinValue;
            gameSession.EndTime = gameEntity.EndTime.HasValue ? gameEntity.EndTime.Value : DateTime.MinValue;
            gameSession.SessionJumpCounter = gameEntity.SessionJumpCounter;
            gameSession.GameDurationInSeconds = gameEntity.Duration;

            if (gameStoreEntity.GameStateEntityObject != null && gameStoreEntity.GameStateEntityObject.GameStates != null)
            {
                gameSession.States = new GameState();
                gameSession.States.Items = gameStoreEntity.GameStateEntityObject.GameStates;
            }
            return gameSession;
        }
        private static GameStateEntity GetGameStateEntity(GameSession gameSession)
        {
            GameStateEntity gameStateEntity = new GameStateEntity();
            gameStateEntity.ParentGameID = gameSession.ID.ToString();
            gameStateEntity.GameStates = gameSession.States.Items;
            return gameStateEntity;
        }

        private static Entities.GameEntity GetGameEntity(GameSession gameSession)
        {
            Entities.GameEntity gameEntity = new Entities.GameEntity();
            gameEntity.PartitionKey = gameSession.LogicalGroup;
            gameEntity.ID = gameSession.ID;
            gameEntity.RowKey = gameEntity.ID.ToString();
            gameEntity.MasterAlpha = JsonConvert.SerializeObject(gameSession.MasterAlpha);
            gameEntity.SessionJumpCounter = gameSession.SessionJumpCounter;
            gameEntity.NumberOfStates = gameSession.NumberOfStates;
            gameEntity.SizeOfState = gameSession.SizeOfState;
            gameEntity.CurrentState = gameSession.CurrentState;
            gameEntity.StartTime = gameSession.StartTime == DateTime.MinValue ? null : (DateTime?)gameSession.StartTime;
            gameEntity.EndTime = gameSession.EndTime == DateTime.MinValue ? null : (DateTime?)gameSession.EndTime;
            gameEntity.Duration = gameSession.GameDurationInSeconds;
            return gameEntity;
        }

    }
}
