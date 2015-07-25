using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Windows.UI.Xaml;
using WordyFlyWPClient.DataModel;

namespace WordyFlyWPClient.Common
{
    public class GameSession : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int totalPoint = 0;
        private string totalPointString = "0 points";
        private int totalWord = 0;
        private string totalWordString = "0 words";

        public DispatcherTimer dispatcherTimer;
        private DateTimeOffset startTime;
        public DateTimeOffset lastTime;
        public DateTimeOffset stopTime;
        private int timesTicked = 0;
        public int timesToTick = 120;
        private string timerString = "0:00";

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
                TotalPointString = totalPoint.ToString() + " points";
                OnPropertyChange("TotalPoint");
            }
        }
        public string TotalPointString
        {
            get
            {
                return totalPointString;
            }
            set
            {
                totalPointString = value;
                OnPropertyChange("TotalPointString");
            }
        }
        public int TotalWord
        {
            get
            {
                return totalWord;
            }
            set
            {
                totalWord = value;
                TotalWordString = totalWord.ToString() + " words";
                OnPropertyChange("TotalWord");
            }
        }
        public string TotalWordString
        {
            get
            {
                return totalWordString;
            }
            set
            {
                totalWordString = value;
                OnPropertyChange("TotalWordString");
            }
        }
        public int TimesTicked
        {
            get
            {
                return timesTicked;
            }
            set
            {
                timesTicked = value;
                if((timesToTick - timesTicked+1) % 60<10)
                {
                    TimerString = ((timesToTick-timesTicked+1) / 60).ToString() + ":0" + ((timesToTick - timesTicked+1) % 60).ToString();
                }
                else
                {
                    TimerString = ((timesToTick - timesTicked+1) / 60).ToString() + ":" + ((timesToTick - timesTicked+1) % 60).ToString();
                }
                OnPropertyChange("TimesTicked");
            }
        }
        public string TimerString
        {
            get
            {
                return timerString;
            }
            set
            {
                timerString = value;
                OnPropertyChange("TimerString");
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

        public void TimerSetup(int baseTime)
        {
            timesToTick = 120-baseTime;
            if(timesToTick>120 || timesToTick<0)
            {
                timesToTick = 120;
            }
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            startTime = DateTimeOffset.Now;
            lastTime = startTime;
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, object e)
        {
            DateTimeOffset time = DateTimeOffset.Now;
            TimeSpan span = time - lastTime;
            lastTime = time;
            TimesTicked++;
            if (TimesTicked > timesToTick)
            {
                stopTime = time;
                dispatcherTimer.Stop();
                span = stopTime - startTime;
            }
        }
    }
}
