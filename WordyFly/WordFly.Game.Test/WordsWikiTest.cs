using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordFly.Game.Utility;
using System.Diagnostics;

namespace WordFly.Game.Test
{
    [TestClass]
    public class WordsWikiTest
    {
        [TestMethod]
        public void ComparisonTest()
        {
            string sampleInput = "BEZCAESDUI";
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var res = WordsWiki.GetAllValidWords(sampleInput, 3);
            watch.Stop();

            Stopwatch parallelWatch = new Stopwatch();
            parallelWatch.Start();

            var res1 = WordsWiki.GetAllValidWordsParallel(sampleInput, 3);

            parallelWatch.Stop();

            double difference = parallelWatch.ElapsedMilliseconds - watch.ElapsedMilliseconds;

            string message = difference.ToString();
        }
    }
}
