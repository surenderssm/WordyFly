using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WordFly.Game.Utility
{
    public class FileHelper
    {
        public Dictionary<string, RootWord> wordList = new Dictionary<string, RootWord>();

        private string WordSourceLocation;
        public FileHelper(string wordSourceLocation)
        {
            //wordSourceLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, );
            if (string.IsNullOrEmpty(wordSourceLocation))
            {
                wordSourceLocation = "WordsSourceFiles";
            }
            string runningBasePath = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
            WordSourceLocation = Path.Combine(Path.GetDirectoryName(runningBasePath), wordSourceLocation);
            PopulateWordList();
        }
        /// <summary>
        /// Function to read the words from the Files folder and populate the wordList member of the class with those words
        /// </summary>
        private void PopulateWordList()
        {
            foreach (string file in Directory.GetFiles(WordSourceLocation))
            {
                File.ReadAllLines(file).Distinct().Where(t => !wordList.ContainsKey(t.ToUpper())).ToList().ForEach(word => wordList.Add(word.ToUpper(), new RootWord() { value = word, sourceFile = file }));
            }
        }
    }
  
    public class RootWord
    {
        public string value { get; set; }
        public string meaning { get; set; }
        public string sourceFile { get; set; }
    }
}
