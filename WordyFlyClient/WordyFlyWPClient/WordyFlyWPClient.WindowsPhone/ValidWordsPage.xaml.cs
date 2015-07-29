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
using System.Threading.Tasks;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WordyFlyWPClient
{
    public class WordsTripod
    {
        public string Word1 { get; set; }
        public string Word2 { get; set; }
        public string Word3 { get; set; }
    }

    public class WordsTripodGroup
    {
        public List<WordsTripod> ValidWordsTripod { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ValidWordsPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        DispatcherTimer countDownTimer;
        private CountDown counter;
        List<WordsTripod> ValidWordsTripod;

        public ValidWordsPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            ValidWordsTripod = new List<WordsTripod>();
            counter = new CountDown(10);
            countDownTimer = new DispatcherTimer();
            countDownTimer.Interval = new TimeSpan(0, 0, 0, 1);
            countDownTimer.Tick += CountDownTimer_Tick;
        }
        private void CountDownTimer_Tick(object sender, object e)
        {
            //txtCountdown.Text = count + " Seconds Remaining";

            if (counter.Count > 0)
            {
                counter.Count--;
                // TODO: remvoe
                //    txtTimer.Text = counter.Timer;
            }
            if (counter.Count == 0)
            {
                countDownTimer.Stop();

                // TODO : think Only for dev
                Frame.Navigate(typeof(LeaderboardPage));
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
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            try
            {

                countDownTimer.Start();
                //txtTimer.Text = counter.Timer;
                txtTimer.DataContext = counter;
                SetWords();
                WordsTripodGroup group = new WordsTripodGroup();
                group.ValidWordsTripod = ValidWordsTripod;
                this.DefaultViewModel["LeaderBoardView"] = group;
                //leaderboardListView.DataContext = ValidWordsTripod;
                txtPossibleWords.Text = ValidWordsTripod.Count.ToString();
                await GameManager.SubmitScore();
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// DUmmy Valdi Words
        /// </summary>
        private void SetWords()
        {
            int totalWords = UserProfile.ValidWords.Count;
            var wordList = UserProfile.ValidWords.Keys.ToList();
            var randomGenerator = new Random();

            int possibleWords = randomGenerator.Next(200, 400);

            List<string> lstPossibleWords = new List<string>();

            for (int i = 0; i < possibleWords; i++)
            {
                lstPossibleWords = new List<string>();

                for (int index = 0; index < 3; index++)
                {

                    int randomIndex = randomGenerator.Next(2, totalWords - 10);
                    try
                    {
                        lstPossibleWords.Add(wordList.ElementAt(randomIndex));
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                ValidWordsTripod.Add(new WordsTripod { Word1 = lstPossibleWords[0], Word2 = lstPossibleWords[1], Word3 = lstPossibleWords[2] });
            }
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
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
