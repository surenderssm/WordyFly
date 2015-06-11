using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WordyFlyWPClient.Common
{
    public static class UserProfile
    {
        public static int Version { get; set; }
        public static Dictionary<string, object> ValidWords = new Dictionary<string, object>();
        public static async Task<Dictionary<string, object>> GetUserProfile()
        {
            StorageFolder assetFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFile assetFile = await assetFolder.GetFileAsync(@"Files\Words.txt");
            Stream stream = await assetFile.OpenStreamForReadAsync();
            using (var streamReader = new StreamReader(stream))
            {
                string[] allWords = streamReader.ReadToEnd().Replace("\r\n", "\n").Split('\n');
                foreach(string word in allWords)
                {
                    if(!ValidWords.ContainsKey(word))
                    {
                        ValidWords.Add(word, word);
                    }
                }
            }
            return ValidWords;
        }
    }
}
