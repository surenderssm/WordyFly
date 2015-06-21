// Unit test cases for Cooridnator

namespace WordFly.Coordinator.WorkerRole.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics;
    [TestClass]
    public class GameCreatorTest
    {
        /// <summary>
        /// Create Single game and Save in teh Game Repository
        /// </summary>
        [TestMethod]
        public void CreateAndSaveTest()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            try
            {
                GameCreator.CreateGames();
            }
            catch (Exception ex)
            {
                watch.Stop();
                Assert.Fail(ex.ToString());
            }
            watch.Stop();

            string message = "Time Taken to create games : " + watch.ElapsedMilliseconds;
            
        }
    }
}
