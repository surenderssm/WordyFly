using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.Game.Utility
{
    public class FileHelper
    {
        public Dictionary<string, Word> wordList = new Dictionary<string, Word>();
        public FileHelper()
        {
            PopulateWordList();
        }
        /// <summary>
        /// Function to read the words from the Files folder and populate the wordList member of the class with those words
        /// </summary>
        private void PopulateWordList()
        {
            foreach (string file in Directory.GetFiles(@"..\..\Files"))
            {
                File.ReadAllLines(file).Distinct().Where(t => !wordList.ContainsKey(t.ToUpper())).ToList().ForEach(word => wordList.Add(word.ToUpper(), new Word() {value=word, sourceFile=file }));
            }
        }
    }
    public class Word
    {
        public string value { get; set; }
        public string meaning { get; set; }
        public string sourceFile { get; set; }
    }
}
