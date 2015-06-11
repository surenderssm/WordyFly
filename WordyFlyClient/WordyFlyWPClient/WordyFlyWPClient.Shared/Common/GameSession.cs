using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WordyFlyWPClient.DataModel;

namespace WordyFlyWPClient.Common
{
    public class GameSession : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int totalPoint = 0;
        public Dictionary<string, Word> wordList = new Dictionary<string, Word>();
        public int TotalPoint
        {
            get
            {
                return totalPoint;
            }
            set
            {
                totalPoint = value;
                OnPropertyChange("TotalPoint");
            }
        }
        protected void OnPropertyChange(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
