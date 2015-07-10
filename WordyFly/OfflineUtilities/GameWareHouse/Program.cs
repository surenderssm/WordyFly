// WordFly

namespace GameWareHouse
{
    using GameWareHouse.Properties;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    class Program
    {
        static void Main(string[] args)
        {
            //UpdateGame();

            Console.WriteLine("Game Creation Started");
            CreateGames();
            Console.WriteLine("Game Creation Completed");
            Console.ReadLine();
            Console.ReadLine();
        }

        private static void UpdateGame()
        {
            try
            {
                DateTime time = DateTime.UtcNow;
                GameUpdate.StartDateOfAllGames(time);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void CreateGames()
        {
            int numberOfGames = Settings.Default.NumberOfGameToCreate;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            try
            {
                GameCreator.CreateGames(numberOfGames);
                watch.Stop();
            }
            catch (Exception)
            {
                watch.Stop();
                throw;
            }
            string message = string.Format("Number of GameCreated : {0}, TimeTaken : {1}",numberOfGames,watch.ElapsedMilliseconds);
            Console.WriteLine(message);
            Console.WriteLine("Games Created");
        }
    }
}
