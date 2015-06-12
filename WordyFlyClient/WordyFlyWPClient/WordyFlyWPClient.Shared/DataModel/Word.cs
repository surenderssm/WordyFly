using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WordyFlyWPClient.DataModel
{
    public class Word : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string currentWord = string.Empty;
        public int Point { get; set; }

        public Word()
        {
            Point = 0;
        }
        public string CurrentWord
        {
            get
            {
                return currentWord.ToUpper();
            }
            set
            {
                currentWord = value;
                OnPropertyChange("CurrentWord");
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
