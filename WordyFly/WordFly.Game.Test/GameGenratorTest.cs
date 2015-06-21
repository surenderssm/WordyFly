using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordFly.Shared.Model;
using System.Diagnostics;
namespace WordFly.Game.Test
{
    [TestClass]
    public class GameGenratorTest
    {
        /// <summary>
        /// 
        /// </summary>

        [TestMethod]
        public void NewGameTest()
        {
            //SLA in milliseconds to create new game
            long SLAForNewGameMilliseconds = 2 * 60 * 1000; // 2 minutes

            Stopwatch watch = new Stopwatch();

            watch.Start();
            GameGenrator generator = new GameGenrator();
            GameSession game = generator.CreateNewGame(GameType.Basic);

            watch.Stop();
            Console.WriteLine("Time Taken : Basic" + watch.Elapsed.Seconds);

            if (watch.ElapsedMilliseconds > SLAForNewGameMilliseconds)
            {
                string message = "SLA for New Game Failed Time Taken : Basic" + watch.ElapsedMilliseconds;
                Assert.Fail(message);
            }
        }

        [TestMethod]
        public void NewBasicGameTest()
        {
            //SLA in milliseconds to create new game
            long SLAForNewGameMilliseconds = 2 * 60 * 1000; // 2 minutes
            Stopwatch watch = new Stopwatch();

            watch.Start();
            GameGenrator generator = new GameGenrator();
            GameSession game = generator.CreateNewGame(GameType.Basic);

            watch.Stop();
            Console.WriteLine("Time Taken : Basic" + watch.Elapsed.Seconds);
            if (watch.ElapsedMilliseconds > SLAForNewGameMilliseconds)
            {
                string message = "SLA for New Game Failed Time Taken : Basic" + watch.ElapsedMilliseconds;
                Assert.Fail(message);
            }


        }
        [TestMethod]
        public void NewNormalGameTest()
        {
            //SLA in milliseconds to create new game
            long SLAForNewGameMilliseconds = 4 * 60 * 1000; // 4 minutes
            Stopwatch watch = new Stopwatch();

            watch.Start();
            GameGenrator generator = new GameGenrator();
            GameSession game = generator.CreateNewGame(GameType.Normal);

            watch.Stop();
            Console.WriteLine("Time Taken : Basic" + watch.Elapsed.Seconds);
            if (watch.ElapsedMilliseconds > SLAForNewGameMilliseconds)
            {
                string message = "SLA for New Game Failed Time Taken : Basic" + watch.ElapsedMilliseconds;
                Assert.Fail(message);
            }
        }


        [TestMethod]
        public void NewAdvancedGameTest()
        {
            //SLA in milliseconds to create new game
            long SLAForNewGameMilliseconds = 5 * 60 * 1000; // 5 minutes
            Stopwatch watch = new Stopwatch();

            watch.Start();
            GameGenrator generator = new GameGenrator();
            GameSession game = generator.CreateNewGame(GameType.Advanced);

            watch.Stop();
            Console.WriteLine("Time Taken : Basic" + watch.Elapsed.Seconds);

            if (watch.ElapsedMilliseconds > SLAForNewGameMilliseconds)
            {
                string message = "SLA for New Game Failed Time Taken : Basic" + watch.ElapsedMilliseconds;
                Assert.Fail(message);
            }
        }
    }
}
