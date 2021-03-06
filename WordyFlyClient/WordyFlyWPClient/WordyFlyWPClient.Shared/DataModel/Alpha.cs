﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace WordyFlyWPClient.DataModel
{
    public class Alpha : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string character = string.Empty;
        private string point = string.Empty;
        private double characterFontSize = 54;
        private double pointFontSize = 16;
        private SolidColorBrush background = new SolidColorBrush(Color.FromArgb(0xFF, 0xE7, 0x14, 0x01));

        public string Character
        {
            get
            {
                return character.ToUpper();
            }
            set
            {
                character = value;
                OnPropertyChange("Character");
            }
        }

        public string Point
        {
            get
            {
                return point;
            }
            set
            {
                point = value;
                OnPropertyChange("Point");
            }
        }
        public double CharacterFontSize
        {
            get
            {
                return characterFontSize;
            }
            set
            {
                characterFontSize = value;
                OnPropertyChange("CharacterFontSize");
            }
        }

        public double PointFontSize
        {
            get
            {
                return pointFontSize;
            }
            set
            {
                pointFontSize = value;
                OnPropertyChange("PointFontSize");
            }
        }
        public SolidColorBrush Background
        {
            get
            {
                return background;
            }
            set
            {
                background = value;
                OnPropertyChange("Background");
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
