using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFly.Shared.Model;

namespace WordFly.Game.Utility
{
    public static class AlphaGenerator
    {
        private static readonly List<char> alphaList;
        static AlphaGenerator()
        {
            alphaList = new List<char>() { 'A', 'E', 'I', 'O', 'U', 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z' };
        }
        public static List<AtomicAlpha> GenerateRawAlpha(int totalAlpha, double vowelsProbability)
        {
            // Unordered Thread-safe Collection for Alphas
            ConcurrentBag<AtomicAlpha> alphaBag = new ConcurrentBag<AtomicAlpha>();
            Random randomGenerator = new Random();

            for (int index = 0; index < totalAlpha; index++)
            {
                int randomValue = randomGenerator.Next(0, 100);
                int charValue;
                if (randomValue < vowelsProbability)
                {
                    charValue = randomGenerator.Next(0, 5);
                }
                else
                {
                    charValue = randomGenerator.Next(5, 26);
                }
                // creates a Alpha with Codevalue between 0 and 26 [A..Z]:[0..25]
                AtomicAlpha alpha = new AtomicAlpha(alphaList.ElementAt(charValue));
                alphaBag.Add(alpha);
            }

            //TODO: probability of Vowels
            // TODO: probability of % of vowels

            return alphaBag.ToList();
        }
    }
}
