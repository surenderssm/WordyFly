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
            //Thread createGameCoordinator = new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    GameCreateCoordinate();
            //});

            //createGameCoordinator.Start();

            //Thread archiveGame = new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    Console.WriteLine("archiving started");
            //});

            //archiveGame.Start();


            await GameCreateCoordinate();

        }

        private static async Task GameCreateCoordinate()
        {
            while (true)
            {
                try
                {
                    Task createGameTask = new Task(new Action(CreateGame));
                    createGameTask.Start();
                    createGameTask.Wait();

                }
                catch (Exception)
                {
                    // Safe fail back
                }
                await Task.Delay(ConfigManager.Config.TimeToCreateGamesInMinutes * 60 * 1000);
            }
        }

        private static void CreateGame()
        {
           // GameCreator.CreateGames();
        }
    }
}
