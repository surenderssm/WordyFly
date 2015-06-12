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
using WordyFlyWPClient.DataModel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace WordyFlyWPClient
{    
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
