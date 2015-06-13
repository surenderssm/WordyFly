using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.Game;
using Storage = WordFly.AzureStorageAccessLayer;
using WordFly.Game.Model;
using WordFly.Common;
using System.Threading;
namespace WordFly.Coordinator.WorkerRole
{
    /// <summary>
    /// Co-ordinates the Game
    /// </summary>
    public  class GameCoordinator
    {

        public GameCoordinator()
        {

        }

        public void StartCoordinating()
        {
            Thread createGameCoordinator = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                GameCreateCoordinate();
            });

            createGameCoordinator.Start();

            Thread archiveGame = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Console.WriteLine("archiving started");
            });

            archiveGame.Start();

        }

        private void GameCreateCoordinate()
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
                    // TODO:surender
                }
                Thread.Sleep(ConfigManager.Config.TimeToCreateGamesInMinutes * 60 * 1000);
            }
        }

        private void CreateGame()
        {
            GameCreator.CreateGames();
        }
    }
}
