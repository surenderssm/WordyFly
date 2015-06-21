using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.Game.Utility
{
    public class WordsWiki
    {
        public static FileHelper dictionaryHelper = new FileHelper(Common.CommonUtility.WordSourceFileLocation);
        /// <summary>
        /// Swap two characters without using a third variable
        /// </summary>
        /// <param name="a">First character</param>
        /// <param name="b">Second character</param>
        public static void SwapChars(ref char a, ref char b)
        {
            if (a == b) return;

            a ^= b;
            b ^= a;
            a ^= b;
        }
        /// <summary>
        /// Prints all the keys of the dictionary 
        /// </summary>
        /// <param name="stringList">Dictionary</param>
        public static void DisplayList(Dictionary<string, bool> stringList)
        {
            foreach (string str in stringList.Keys)
            {
                Console.WriteLine(str);
            }
        }
        /// <summary>
        /// Gets all the valid words of a particular length that can be formed from a stream of characters and stores them in a dictionary
        /// </summary>
        /// <param name="inputStream">Input character stream</param>
        /// <param name="currentString">Current value of the string in the tree (Pass it empty)</param>
        /// <param name="stringList">Output dictionary</param>
        /// <param name="lengthSelect">Length of words to be formed</param>
        public static void GetValidWords(char[] inputStream, string currentString, ref Dictionary<string, RootWord> stringList, int lengthSelect)
        {
            if (currentString.Length == lengthSelect)
            {
                if (!stringList.ContainsKey(currentString) && dictionaryHelper.wordList.ContainsKey(currentString))
                {
                    stringList.Add(currentString, dictionaryHelper.wordList[currentString]);
                }
            }
            else
            {
                for (int i = currentString.Length; i < inputStream.Length; i++)
                {
                    SwapChars(ref inputStream[i], ref inputStream[currentString.Length]);
                    GetValidWords(inputStream, currentString + inputStream[currentString.Length], ref stringList, lengthSelect);
                    SwapChars(ref inputStream[i], ref inputStream[currentString.Length]);
                }
            }
        }
        /// <summary>
        /// Gets all the valid words that can be formed from a stream of characters
        /// </summary>
        /// <param name="inputStream">Input stream</param>
        /// <param name="minWordLength">Minimum length of words to be formed</param>
        /// <returns></returns>
        public static Dictionary<string, RootWord> GetAllValidWords(string inputStream, int minWordLength)
        {
            Dictionary<string, RootWord> stringList = new Dictionary<string, RootWord>();
            Parallel.For(minWordLength, inputStream.Length + 1, i =>
            {
                GetValidWords(inputStream.ToCharArray(), "", ref stringList, i);
            });
            return stringList;
        }
    }
}
