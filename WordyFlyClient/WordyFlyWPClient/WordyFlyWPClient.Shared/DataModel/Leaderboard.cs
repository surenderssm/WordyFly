//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
//using System.Threading.Tasks;
//using Windows.Data.Json;
//using Windows.Storage;
//using Windows.UI.Xaml.Media;
//using Windows.UI.Xaml.Media.Imaging;

//// The data model defined by this file serves as a representative example of a strongly-typed
//// model.  The property names chosen coincide with data bindings in the standard item templates.
////
//// Applications may use this model as a starting point and build on it, or discard it entirely and
//// replace it with something appropriate to their needs. If using this model, you might improve app 
//// responsiveness by initiating the data loading task in the code behind for App.xaml when the app 
//// is first launched.

//namespace WordyFlyWPClient.Data
//{

//    public class LeaderboardResponse
//    {
//        public Profile UserProfile { get; set; }
//        public LeaderBoard LeaderBoard { get; set; }
//    }

//    public class Profile
//    {
//        public event PropertyChangedEventHandler PropertyChanged;

//        protected void OnPropertyChange(string name)
//        {
//            PropertyChangedEventHandler handler = PropertyChanged;
//            if (handler != null)
//            {
//                handler(this, new PropertyChangedEventArgs(name));
//            }
//        }

//        public string UserName
//        {
//            get;
//            set
//            {
//                OnPropertyChange("UserName");
//            }
//        }

//        public string UserID
//        {
//            get;
//            set
//            {
//                OnPropertyChange("UserID");
//            }
//        }

//        public long Rank
//        {
//            get;
//            set
//            {
//                OnPropertyChange("Rank");
//            }
//        }

//        public long Score
//        {
//            get;
//            set
//            {
//                OnPropertyChange("Score");
//            }
//        }

//        public long NumberOfWords
//        {
//            get;
//            set
//            {
//                OnPropertyChange("NumberOfWords");
//            }
//        }
//    }

//    public class LeaderBoard
//    {
//        /// <summary>
//        /// GameID of the Game whose LeaderBoard is Presented
//        /// </summary>
//        public string GameID { get; set; }

//        /// <summary>
//        /// List of Profiles participated in the Game
//        /// </summary>
//        public List<Profile> Profiles { get; set; }
//    }
//}