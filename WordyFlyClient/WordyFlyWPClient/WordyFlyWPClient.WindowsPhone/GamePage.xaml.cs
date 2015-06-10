using WordyFlyWPClient.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using Windows.UI.Xaml.Media.Animation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WordyFlyWPClient
{
    public class Word : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string currentWord = string.Empty;


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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private Random rand = new Random();

        public Word tempWord; 
        public Queue<Alpha> queue = new Queue<Alpha>();
        public GamePage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            InitGame();
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // tbInput.Focus(FocusState.Programmatic);
            //   tbInput.Focus(FocusState.Programmatic);
            await StatusBar.GetForCurrentView().HideAsync();
            block1SlideIn.Begin();
            block2SlideIn.Begin();
            block3SlideIn.Begin();
            block4SlideIn.Begin();
            block5SlideIn.Begin();
            block6SlideIn.Begin();
            block7SlideIn.Begin();
            block8SlideIn.Begin();
            block9SlideIn.Begin();
            block10SlideIn.Begin();
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            tempWord.CurrentWord += Convert.ToString((sender as Button).Content);
            //  textBlock.DataContext = CurrentWord;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void InitGame()
        {
            tempWord = new Word();
            char[] charList = "ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            
            foreach (char c in charList)
            {
                queue.Enqueue(new Alpha() { Character = c.ToString(), Point = rand.Next(1, 10).ToString() });
            }

            textBlock.DataContext = tempWord;
            
            Alpha alpha = queue.Dequeue();
            charBlock1.AlphaBlock.Character = alpha.Character;
            charBlock1.AlphaBlock.Point = alpha.Point;
            charBlock1.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0,70), Rotation=rand.Next(0,30)-15 };

            alpha = queue.Dequeue();
            charBlock2.AlphaBlock.Character = alpha.Character;
            charBlock2.AlphaBlock.Point = alpha.Point;
            charBlock2.RenderTransform = new CompositeTransform { TranslateX = rand.Next(70, 140), Rotation = rand.Next(0, 30) - 15 };

            alpha = queue.Dequeue();
            charBlock3.AlphaBlock.Character = alpha.Character;
            charBlock3.AlphaBlock.Point = alpha.Point;
            charBlock3.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 70), Rotation = rand.Next(0, 30) - 15 };

            alpha = queue.Dequeue();
            charBlock4.AlphaBlock.Character = alpha.Character;
            charBlock4.AlphaBlock.Point = alpha.Point;
            charBlock4.RenderTransform = new CompositeTransform { TranslateX = rand.Next(70, 140), Rotation = rand.Next(0, 30) - 15 };

            alpha = queue.Dequeue();
            charBlock5.AlphaBlock.Character = alpha.Character;
            charBlock5.AlphaBlock.Point = alpha.Point;
            charBlock5.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 70), Rotation = rand.Next(0, 30) - 15 };

            alpha = queue.Dequeue();
            charBlock6.AlphaBlock.Character = alpha.Character;
            charBlock6.AlphaBlock.Point = alpha.Point;
            charBlock6.RenderTransform = new CompositeTransform { TranslateX = rand.Next(70, 140), Rotation = rand.Next(0, 30) - 15 };

            alpha = queue.Dequeue();
            charBlock7.AlphaBlock.Character = alpha.Character;
            charBlock7.AlphaBlock.Point = alpha.Point;
            charBlock7.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 70), Rotation = rand.Next(0, 30) - 15 };

            alpha = queue.Dequeue();
            charBlock8.AlphaBlock.Character = alpha.Character;
            charBlock8.AlphaBlock.Point = alpha.Point;
            charBlock8.RenderTransform = new CompositeTransform { TranslateX = rand.Next(70, 140), Rotation = rand.Next(0, 30) - 15 };

            alpha = queue.Dequeue();
            charBlock9.AlphaBlock.Character = alpha.Character;
            charBlock9.AlphaBlock.Point = alpha.Point;
            charBlock9.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 70), Rotation = rand.Next(0, 30) - 15 };

            alpha = queue.Dequeue();
            charBlock10.AlphaBlock.Character = alpha.Character;
            charBlock10.AlphaBlock.Point = alpha.Point;
            charBlock10.RenderTransform = new CompositeTransform { TranslateX = rand.Next(70, 140), Rotation = rand.Next(0, 30) - 15 };
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tempWord.CurrentWord = string.Empty;
            
            charBlock1.BlockReset();
            charBlock2.BlockReset();
            charBlock3.BlockReset();
            charBlock4.BlockReset();
            charBlock5.BlockReset();
            charBlock6.BlockReset();
            charBlock7.BlockReset();
            charBlock8.BlockReset();
            charBlock9.BlockReset();
            charBlock10.BlockReset();
        }
        private void block1SlideIn_Completed(object sender, object e)
        {
            Alpha alpha = queue.Dequeue();
            charBlock1.AlphaBlock.Character = alpha.Character;
            charBlock1.AlphaBlock.Point = alpha.Point;
            charBlock1.BlockReset();
            block1SlideIn.Begin();
            charBlock1.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 70), Rotation = rand.Next(0, 30) - 15 };
        }

        private void block2SlideIn_Completed(object sender, object e)
        {
            Alpha alpha = queue.Dequeue();
            charBlock2.AlphaBlock.Character = alpha.Character;
            charBlock2.AlphaBlock.Point = alpha.Point;
            charBlock2.BlockReset();
            block2SlideIn.Begin();
            charBlock2.RenderTransform = new CompositeTransform { TranslateX = rand.Next(70, 140), Rotation = rand.Next(0, 30) - 15 };
        }

        private void block3SlideIn_Completed(object sender, object e)
        {
            Alpha alpha = queue.Dequeue();
            charBlock3.AlphaBlock.Character = alpha.Character;
            charBlock3.AlphaBlock.Point = alpha.Point;
            charBlock3.BlockReset();
            block3SlideIn.Begin();
            block3SlideIn.Seek(new TimeSpan(0, 0, 2));
            charBlock3.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 70), Rotation = rand.Next(0, 30) - 15 };
        }

        private void block4SlideIn_Completed(object sender, object e)
        {
            Alpha alpha = queue.Dequeue();
            charBlock4.AlphaBlock.Character = alpha.Character;
            charBlock4.AlphaBlock.Point = alpha.Point;
            charBlock4.BlockReset();
            block4SlideIn.Begin();
            block4SlideIn.Seek(new TimeSpan(0, 0, 2));
            charBlock4.RenderTransform = new CompositeTransform { TranslateX = rand.Next(70, 140), Rotation = rand.Next(0, 30) - 15 };
        }

        private void block5SlideIn_Completed(object sender, object e)
        {
            Alpha alpha = queue.Dequeue();
            charBlock5.AlphaBlock.Character = alpha.Character;
            charBlock5.AlphaBlock.Point = alpha.Point;
            charBlock5.BlockReset();
            block5SlideIn.Begin();
            block5SlideIn.Seek(new TimeSpan(0, 0, 4));
            charBlock5.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 70), Rotation = rand.Next(0, 30) - 15 };
        }

        private void block6SlideIn_Completed(object sender, object e)
        {
            Alpha alpha = queue.Dequeue();
            charBlock6.AlphaBlock.Character = alpha.Character;
            charBlock6.AlphaBlock.Point = alpha.Point;
            charBlock6.BlockReset();
            block6SlideIn.Begin();
            block6SlideIn.Seek(new TimeSpan(0, 0, 4));
            charBlock6.RenderTransform = new CompositeTransform { TranslateX = rand.Next(70, 140), Rotation = rand.Next(0, 30) - 15 };
        }

        private void block7SlideIn_Completed(object sender, object e)
        {
            Alpha alpha = queue.Dequeue();
            charBlock7.AlphaBlock.Character = alpha.Character;
            charBlock7.AlphaBlock.Point = alpha.Point;
            charBlock7.BlockReset();
            block7SlideIn.Begin();
            block7SlideIn.Seek(new TimeSpan(0, 0, 6));
            charBlock7.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 70), Rotation = rand.Next(0, 30) - 15 };
        }

        private void block8SlideIn_Completed(object sender, object e)
        {
            Alpha alpha = queue.Dequeue();
            charBlock8.AlphaBlock.Character = alpha.Character;
            charBlock8.AlphaBlock.Point = alpha.Point;
            charBlock8.BlockReset();
            block8SlideIn.Begin();
            block8SlideIn.Seek(new TimeSpan(0, 0, 6));
            charBlock8.RenderTransform = new CompositeTransform { TranslateX = rand.Next(70, 140), Rotation = rand.Next(0, 30) - 15 };
        }

        private void block9SlideIn_Completed(object sender, object e)
        {
            Alpha alpha = queue.Dequeue();
            charBlock9.AlphaBlock.Character = alpha.Character;
            charBlock9.AlphaBlock.Point = alpha.Point;
            charBlock9.BlockReset();
            block9SlideIn.Begin();
            block9SlideIn.Seek(new TimeSpan(0, 0, 8));
            charBlock9.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 70), Rotation = rand.Next(0, 30) - 15 };
        }

        private void block10SlideIn_Completed(object sender, object e)
        {
            Alpha alpha = queue.Dequeue();
            charBlock10.AlphaBlock.Character = alpha.Character;
            charBlock10.AlphaBlock.Point = alpha.Point;
            charBlock10.BlockReset();
            block10SlideIn.Begin();
            block10SlideIn.Seek(new TimeSpan(0, 0, 8));
            charBlock10.RenderTransform = new CompositeTransform { TranslateX = rand.Next(70, 140), Rotation = rand.Next(0, 30) - 15 };
        }

        private void charBlock_BlockTapped1(object sender, TappedRoutedEventArgs e)
        {
            tempWord.CurrentWord += Convert.ToString((sender as CharBlock).AlphaBlock.Character);
        }
        private void charBlock_BlockTapped2(object sender, TappedRoutedEventArgs e)
        {
            tempWord.CurrentWord += Convert.ToString((sender as CharBlock).AlphaBlock.Character);
        }
        private void charBlock_BlockTapped3(object sender, TappedRoutedEventArgs e)
        {
            tempWord.CurrentWord += Convert.ToString((sender as CharBlock).AlphaBlock.Character);
        }
        private void charBlock_BlockTapped4(object sender, TappedRoutedEventArgs e)
        {
            tempWord.CurrentWord += Convert.ToString((sender as CharBlock).AlphaBlock.Character);
        }
        private void charBlock_BlockTapped5(object sender, TappedRoutedEventArgs e)
        {
            tempWord.CurrentWord += Convert.ToString((sender as CharBlock).AlphaBlock.Character);
        }
        private void charBlock_BlockTapped6(object sender, TappedRoutedEventArgs e)
        {
            tempWord.CurrentWord += Convert.ToString((sender as CharBlock).AlphaBlock.Character);
        }
        private void charBlock_BlockTapped7(object sender, TappedRoutedEventArgs e)
        {
            tempWord.CurrentWord += Convert.ToString((sender as CharBlock).AlphaBlock.Character);
        }
        private void charBlock_BlockTapped8(object sender, TappedRoutedEventArgs e)
        {
            tempWord.CurrentWord += Convert.ToString((sender as CharBlock).AlphaBlock.Character);
        }
        private void charBlock_BlockTapped9(object sender, TappedRoutedEventArgs e)
        {
            tempWord.CurrentWord += Convert.ToString((sender as CharBlock).AlphaBlock.Character);
        }
        private void charBlock_BlockTapped10(object sender, TappedRoutedEventArgs e)
        {
            tempWord.CurrentWord += Convert.ToString((sender as CharBlock).AlphaBlock.Character);
        }
    }
    
}
