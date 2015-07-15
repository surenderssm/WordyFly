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
using Windows.Storage;
using Windows.UI.Popups;
using WordyFlyWPClient.DataModel;
using Windows.UI.Input;
using System.Threading.Tasks;
using WordFly.ServiceClientMe;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WordyFlyWPClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private Random rand = new Random();

        public Word tempWord;
        Queue<Masteralpha> queue = new Queue<Masteralpha>();
        //public Queue<Alpha> queue = new Queue<Alpha>();
        public GameSession gameSession;
        private Point initialpoint;
        GestureRecognizer gr = new GestureRecognizer();
        private static double baseGHridHeight = 490;
        private double multiplier = 0;
        public Rootobject GameResult { get; set; }

        List<Storyboard> storyBoard = new List<Storyboard>();
        List<CharBlock> charBlock = new List<CharBlock>();
        List<Grid> gridBlock = new List<Grid>();
        public GamePage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            InitCharBlockList();
        }

        public void InitCharBlockList()
        {
            storyBoard.Add(block1SlideIn);
            storyBoard.Add(block2SlideIn);
            storyBoard.Add(block3SlideIn);
            storyBoard.Add(block4SlideIn);
            storyBoard.Add(block5SlideIn);
            storyBoard.Add(block6SlideIn);
            storyBoard.Add(block7SlideIn);
            storyBoard.Add(block8SlideIn);
            storyBoard.Add(block9SlideIn);
            storyBoard.Add(block10SlideIn);

            charBlock.Add(charBlock1);
            charBlock.Add(charBlock2);
            charBlock.Add(charBlock3);
            charBlock.Add(charBlock4);
            charBlock.Add(charBlock5);
            charBlock.Add(charBlock6);
            charBlock.Add(charBlock7);
            charBlock.Add(charBlock8);
            charBlock.Add(charBlock9);
            charBlock.Add(charBlock10);

            gridBlock.Add(block1);
            gridBlock.Add(block2);
            gridBlock.Add(block3);
            gridBlock.Add(block4);
            gridBlock.Add(block5);
            gridBlock.Add(block6);
            gridBlock.Add(block7);
            gridBlock.Add(block8);
            gridBlock.Add(block9);
            gridBlock.Add(block10);
        }
        private void gr_ManipulationStarted(GestureRecognizer sender, ManipulationStartedEventArgs args)
        {
            initialpoint = args.Position;
        }

        private void gr_ManipulationCompleted(GestureRecognizer sender, ManipulationCompletedEventArgs args)
        {
            Point currentpoint = args.Position;
            if (Math.Abs(currentpoint.X - initialpoint.X) >= 100)
            {
                if (tempWord.CurrentWord.Length >= 3)
                {
                    if (UserProfile.ValidWords.ContainsKey(tempWord.CurrentWord.ToUpper()) && !gameSession.wordList.ContainsKey(tempWord.CurrentWord.ToUpper()))
                    {
                        gameSession.wordList.Add(tempWord.CurrentWord, tempWord);
                        gameSession.TotalPoint += tempWord.Point;
                        gameSession.TotalWord++;
                    }
                }
                ResetAlpha();
                gr.CompleteGesture();
            }
        }

        void MainPage_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var ps = e.GetIntermediatePoints(null);
            if (ps != null && ps.Count > 0)
            {
                gr.ProcessUpEvent(ps[0]);
                e.Handled = true;
                gr.CompleteGesture();
            }
        }

        void MainPage_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            gr.ProcessMoveEvents(e.GetIntermediatePoints(null));
            e.Handled = true;
        }

        void MainPage_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var ps = e.GetIntermediatePoints(null);
            if (ps != null && ps.Count > 0)
            {
                gr.ProcessDownEvent(ps[0]);
                e.Handled = true;
            }
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            StoryboardToggle(false);
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            tempWord.CurrentWord += Convert.ToString((sender as Button).Content);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StoryBoardInit();
            await InitGame();
        }

        private void StoryBoardInit()
        {
            double gridActualHeight = ContentRoot.ActualHeight;
            multiplier = gridActualHeight / 490;
            List<DoubleAnimationUsingKeyFrames> animationList = new List<DoubleAnimationUsingKeyFrames>();

            //Scaling grid
            foreach (Grid grid in gridBlock)
            {
                grid.Width = multiplier * grid.Width;
                grid.Height = multiplier * grid.Height;
            }

            //Scaling block font sizes
            foreach (CharBlock block in charBlock)
            {
                block.AlphaBlock.CharacterFontSize = multiplier * block.AlphaBlock.CharacterFontSize;
                block.AlphaBlock.PointFontSize = multiplier * block.AlphaBlock.PointFontSize;

            }

            foreach (Storyboard story in storyBoard)
            {
                animationList.Add(story.Children[0] as DoubleAnimationUsingKeyFrames);
            }

            foreach (DoubleAnimationUsingKeyFrames doubleAnimation in animationList)
            {
                foreach (EasingDoubleKeyFrame myEasingKey in doubleAnimation.KeyFrames)
                {
                    myEasingKey.Value = multiplier * myEasingKey.Value;
                }

            }

        }
        private async Task InitGame()
        {
            GameResult = await GameRepository.GetGame();
            queue = GameResult.GamePlay.MasterAlpha;

            this.ManipulationMode = ManipulationModes.All;
            this.PointerPressed += MainPage_PointerPressed;
            this.PointerMoved += MainPage_PointerMoved;
            this.PointerReleased += MainPage_PointerReleased;
            gr.ManipulationCompleted += gr_ManipulationCompleted;
            gr.ManipulationStarted += gr_ManipulationStarted;
            gr.GestureSettings = GestureSettings.ManipulationTranslateX | GestureSettings.ManipulationTranslateY;

            tempWord = new Word();
            txtCurrentWord.DataContext = tempWord;

            foreach (CharBlock block in charBlock)
            {
                Masteralpha alpha = queue.Dequeue();
                block.AlphaBlock.Character = alpha.Name;
                block.AlphaBlock.Point = alpha.CodeValue.ToString();
                block.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 100) - 50, Rotation = rand.Next(0, 30) - 15 };
            }

            StoryboardToggle(true);

            int baseTime = GameResult.GamePlay.baseTime;
            for (int i = 0; i < storyBoard.Count; i++)
            {
                if (baseTime <= (20 + (i * 2)))
                {
                    storyBoard[i].Seek(new TimeSpan(0, 0, baseTime));
                }
                else
                {
                    int time = ((baseTime - (i * 2)) % 20);
                    if (time == 0)
                    {
                        time = 20;
                    }
                    storyBoard[i].Seek(new TimeSpan(0, 0, time + (i * 2)));
                }
            }

            gameSession = new GameSession();
            gameSession.TimerSetup(GameResult.GamePlay.StartTime);
            txtPoint.DataContext = gameSession;
            txtTotalWord.DataContext = gameSession;
            txtTimer.DataContext = gameSession;
        }

        private void ResetAlpha()
        {
            tempWord.CurrentWord = string.Empty;
            tempWord.Point = 0;
            txtCurrentWord.Visibility = Visibility.Collapsed;
            txtTotalWord.Visibility = Visibility.Visible;
            txtPoint.Visibility = Visibility.Visible;

            foreach (CharBlock block in charBlock)
            {
                block.BlockReset();
            }
        }
        private void StoryboardToggle(bool start)
        {
            foreach (Storyboard block in storyBoard)
            {
                if (start)
                {
                    block.Begin();
                }
                else
                {
                    block.Stop();
                }
            }
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetAlpha();
        }
        private void block1SlideIn_Completed(object sender, object e)
        {
            if (queue.Count > 0)
            {
                Masteralpha alpha = queue.Dequeue();
                charBlock1.AlphaBlock.Character = alpha.Name;
                charBlock1.AlphaBlock.Point = alpha.CodeValue.ToString();
                charBlock1.BlockReset();
                block1SlideIn.Begin();
                charBlock1.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 100) - 50, Rotation = rand.Next(0, 30) - 15 };
            }
        }

        private void block2SlideIn_Completed(object sender, object e)
        {
            if (queue.Count > 0)
            {
                Masteralpha alpha = queue.Dequeue();
                charBlock2.AlphaBlock.Character = alpha.Name;
                charBlock2.AlphaBlock.Point = alpha.CodeValue.ToString();
                charBlock2.BlockReset();
                block2SlideIn.Begin();
                block2SlideIn.Seek(new TimeSpan(0, 0, 2));
                charBlock2.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 100) - 50, Rotation = rand.Next(0, 30) - 15 };
            }
        }

        private void block3SlideIn_Completed(object sender, object e)
        {
            if (queue.Count > 0)
            {
                Masteralpha alpha = queue.Dequeue();
                charBlock3.AlphaBlock.Character = alpha.Name;
                charBlock3.AlphaBlock.Point = alpha.CodeValue.ToString();
                charBlock3.BlockReset();
                block3SlideIn.Begin();
                block3SlideIn.Seek(new TimeSpan(0, 0, 4));
                charBlock3.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 100) - 50, Rotation = rand.Next(0, 30) - 15 };
            }
        }

        private void block4SlideIn_Completed(object sender, object e)
        {
            if (queue.Count > 0)
            {
                Masteralpha alpha = queue.Dequeue();
                charBlock4.AlphaBlock.Character = alpha.Name;
                charBlock4.AlphaBlock.Point = alpha.CodeValue.ToString();
                charBlock4.BlockReset();
                block4SlideIn.Begin();
                block4SlideIn.Seek(new TimeSpan(0, 0, 6));
                charBlock4.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 100) - 50, Rotation = rand.Next(0, 30) - 15 };
            }
        }

        private void block5SlideIn_Completed(object sender, object e)
        {
            if (queue.Count > 0)
            {
                Masteralpha alpha = queue.Dequeue();
                charBlock5.AlphaBlock.Character = alpha.Name;
                charBlock5.AlphaBlock.Point = alpha.CodeValue.ToString();
                charBlock5.BlockReset();
                block5SlideIn.Begin();
                block5SlideIn.Seek(new TimeSpan(0, 0, 8));
                charBlock5.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 100) - 50, Rotation = rand.Next(0, 30) - 15 };
            }
        }

        private void block6SlideIn_Completed(object sender, object e)
        {
            if (queue.Count > 0)
            {
                Masteralpha alpha = queue.Dequeue();
                charBlock6.AlphaBlock.Character = alpha.Name;
                charBlock6.AlphaBlock.Point = alpha.CodeValue.ToString();
                charBlock6.BlockReset();
                block6SlideIn.Begin();
                block6SlideIn.Seek(new TimeSpan(0, 0, 10));
                charBlock6.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 100) - 50, Rotation = rand.Next(0, 30) - 15 };
            }
        }

        private void block7SlideIn_Completed(object sender, object e)
        {
            if (queue.Count > 0)
            {
                Masteralpha alpha = queue.Dequeue();
                charBlock7.AlphaBlock.Character = alpha.Name;
                charBlock7.AlphaBlock.Point = alpha.CodeValue.ToString();
                charBlock7.BlockReset();
                block7SlideIn.Begin();
                block7SlideIn.Seek(new TimeSpan(0, 0, 12));
                charBlock7.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 100) - 50, Rotation = rand.Next(0, 30) - 15 };
            }
        }

        private void block8SlideIn_Completed(object sender, object e)
        {
            if (queue.Count > 0)
            {
                Masteralpha alpha = queue.Dequeue();
                charBlock8.AlphaBlock.Character = alpha.Name;
                charBlock8.AlphaBlock.Point = alpha.CodeValue.ToString();
                charBlock8.BlockReset();
                block8SlideIn.Begin();
                block8SlideIn.Seek(new TimeSpan(0, 0, 14));
                charBlock8.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 100) - 50, Rotation = rand.Next(0, 30) - 15 };
            }
        }

        private void block9SlideIn_Completed(object sender, object e)
        {
            if (queue.Count > 0)
            {
                Masteralpha alpha = queue.Dequeue();
                charBlock9.AlphaBlock.Character = alpha.Name;
                charBlock9.AlphaBlock.Point = alpha.CodeValue.ToString();
                charBlock9.BlockReset();
                block9SlideIn.Begin();
                block9SlideIn.Seek(new TimeSpan(0, 0, 16));
                charBlock9.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 100) - 50, Rotation = rand.Next(0, 30) - 15 };
            }
        }

        private void block10SlideIn_Completed(object sender, object e)
        {
            if (queue.Count > 0)
            {
                Masteralpha alpha = queue.Dequeue();
                charBlock10.AlphaBlock.Character = alpha.Name;
                charBlock10.AlphaBlock.Point = alpha.CodeValue.ToString();
                charBlock10.BlockReset();
                block10SlideIn.Begin();
                block10SlideIn.Seek(new TimeSpan(0, 0, 18));
                charBlock10.RenderTransform = new CompositeTransform { TranslateX = rand.Next(0, 100) - 50, Rotation = rand.Next(0, 30) - 15 };
            }
        }
        private void updateCurrentWord(string word, int point)
        {
            tempWord.CurrentWord += word;
            tempWord.Point += point;
            txtCurrentWord.Visibility = Visibility.Visible;
            txtTotalWord.Visibility = Visibility.Collapsed;
            txtPoint.Visibility = Visibility.Collapsed;
        }
        private void charBlock_BlockTapped1(object sender, TappedRoutedEventArgs e)
        {
            updateCurrentWord(Convert.ToString((sender as CharBlock).AlphaBlock.Character),
                Convert.ToInt32((sender as CharBlock).AlphaBlock.Point));
        }
        private void charBlock_BlockTapped2(object sender, TappedRoutedEventArgs e)
        {
            updateCurrentWord(Convert.ToString((sender as CharBlock).AlphaBlock.Character),
                Convert.ToInt32((sender as CharBlock).AlphaBlock.Point));
        }
        private void charBlock_BlockTapped3(object sender, TappedRoutedEventArgs e)
        {
            updateCurrentWord(Convert.ToString((sender as CharBlock).AlphaBlock.Character),
                Convert.ToInt32((sender as CharBlock).AlphaBlock.Point));
        }
        private void charBlock_BlockTapped4(object sender, TappedRoutedEventArgs e)
        {
            updateCurrentWord(Convert.ToString((sender as CharBlock).AlphaBlock.Character),
                Convert.ToInt32((sender as CharBlock).AlphaBlock.Point));
        }
        private void charBlock_BlockTapped5(object sender, TappedRoutedEventArgs e)
        {
            updateCurrentWord(Convert.ToString((sender as CharBlock).AlphaBlock.Character),
                Convert.ToInt32((sender as CharBlock).AlphaBlock.Point));
        }
        private void charBlock_BlockTapped6(object sender, TappedRoutedEventArgs e)
        {
            updateCurrentWord(Convert.ToString((sender as CharBlock).AlphaBlock.Character),
                Convert.ToInt32((sender as CharBlock).AlphaBlock.Point));
        }
        private void charBlock_BlockTapped7(object sender, TappedRoutedEventArgs e)
        {
            updateCurrentWord(Convert.ToString((sender as CharBlock).AlphaBlock.Character),
                Convert.ToInt32((sender as CharBlock).AlphaBlock.Point));
        }
        private void charBlock_BlockTapped8(object sender, TappedRoutedEventArgs e)
        {
            updateCurrentWord(Convert.ToString((sender as CharBlock).AlphaBlock.Character),
                Convert.ToInt32((sender as CharBlock).AlphaBlock.Point));
        }
        private void charBlock_BlockTapped9(object sender, TappedRoutedEventArgs e)
        {
            updateCurrentWord(Convert.ToString((sender as CharBlock).AlphaBlock.Character),
                Convert.ToInt32((sender as CharBlock).AlphaBlock.Point));
        }
        private void charBlock_BlockTapped10(object sender, TappedRoutedEventArgs e)
        {
            updateCurrentWord(Convert.ToString((sender as CharBlock).AlphaBlock.Character),
                Convert.ToInt32((sender as CharBlock).AlphaBlock.Point));
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (tempWord.CurrentWord.Length >= 3)
            {
                if (UserProfile.ValidWords.ContainsKey(tempWord.CurrentWord.ToUpper()) && !gameSession.wordList.ContainsKey(tempWord.CurrentWord.ToUpper()))
                {
                    gameSession.wordList.Add(tempWord.CurrentWord, tempWord);
                    gameSession.TotalPoint += tempWord.Point;
                    gameSession.TotalWord++;
                }
                ResetAlpha();
            }
        }
    }

}
