using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.AzureStorageAccessLayer;
using WordFly.AzureStorageAccessLayer.Entities;
namespace GameUpdate
{
    public static class GameUpdate
    {
        private static GameStorageAccess gameStorageAccess;
        public static void StartDateOfAllGames(DateTime gameStartUTC)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            try
            {
                gameStorageAccess = new GameStorageAccess(Constants.GameRepositoryTableName);

                var games = gameStorageAccess.GetGameEntities(GameEntityStatus.Undefined);
                
                foreach (var game in games)
                {
                    var totalGameTime = game.EndTime.Subtract(game.StartTime);
                    game.StartTime = gameStartUTC;
                    game.EndTime = game.StartTime.Add(totalGameTime);
                    gameStorageAccess.SaveGameEntity(game);
                }
            }
            catch (Exception)
            {
                watch.Stop();
                Console.WriteLine(" total time taken " + watch.Elapsed.TotalMilliseconds);
                throw;
            }
            watch.Stop();
            Console.WriteLine(" total time taken " + watch.Elapsed.TotalMilliseconds);
        }
    }
}
