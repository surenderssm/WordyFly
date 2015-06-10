using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace WordyFlyWPClient
{
    public class Alpha : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string character = string.Empty;
        private string point = string.Empty;
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
    public sealed partial class CharBlock : UserControl
    {
        public Alpha AlphaBlock = new Alpha();
        // The event 
        public event EventHandler<TappedRoutedEventArgs> BlockTapped;
        private bool isTapped = false;
        private SolidColorBrush backGround;
        public CharBlock()
        {
            this.InitializeComponent();
            backGround = AlphaBlock.Background;
            txtChar.DataContext = AlphaBlock;
            txtPoint.DataContext = AlphaBlock;
            grdBlock.DataContext = AlphaBlock;
        }
        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!isTapped)
            {
                isTapped = true;
                backGround = AlphaBlock.Background;
                AlphaBlock.Background = new SolidColorBrush(Colors.White);
                txtChar.Foreground = txtPoint.Foreground = new SolidColorBrush(Colors.Black);
                OnBlockTapped(e);
            }
        }
        // The thing that fires the event 
        private void OnBlockTapped(TappedRoutedEventArgs e)
        {
            EventHandler<TappedRoutedEventArgs> handler = BlockTapped;
            if (handler != null)
                handler(this, e);
        }
        public void BlockReset()
        {
            isTapped = false;
            AlphaBlock.Background = backGround;
            txtChar.Foreground = txtPoint.Foreground = new SolidColorBrush(Colors.White);
        }

    }
}
