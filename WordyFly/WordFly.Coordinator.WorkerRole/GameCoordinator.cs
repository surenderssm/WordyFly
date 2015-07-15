using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.Game;
using Storage = WordFly.AzureStorageAccessLayer;
using WordFly.Shared.Model;
using WordFly.Common;
using System.Threading;
namespace WordFly.Coordinator.WorkerRole
{
    /// <summary>
    /// Co-ordinates the Game
    /// </summary>
    public static class GameCoordinator
    {

        static GameCoordinator()
        {

        }

        public static async Task StartCoordinating()
        {
            await GameInsertCoordinate();
        }

        private static async Task GameInsertCoordinate()
        {
            while (true)
            {
                try
                {
                    Task createGameTask = new Task(new Action(InsertGames));
                    createGameTask.Start();
                    createGameTask.Wait();

                }
                catch (Exception)
                {
                    // Safe fail back
                    continue;
                }
                await Task.Delay(ConfigManager.Config.TimeToInsertGamesInMinutes * 60 * 1000);
                await Task.Delay(2000);
            }
        }

        private static void InsertGames()
        {
            var gameTran = new GameTransaction();
            for (int i = 0; i < ConfigManager.Config.NumberOfGamesToInsert; i++)
            {
                gameTran.InsertGames();
            }
        }
    }
}