using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            UpdateGame();

            Task.Delay(10 * 1000);
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
    }
}
