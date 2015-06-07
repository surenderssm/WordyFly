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

        public Word tempWord;
        public Queue<char> queue = new Queue<char>();
        public GamePage()
        {
            this.InitializeComponent();
            tempWord = new Word();
            char[] charList= "KLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(); 
            foreach( char c in charList)
            {
                queue.Enqueue(c);
            }
            textBlock.DataContext = tempWord;
            //tbInput.Focus(FocusState.Programmatic);
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
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
            btn1SlideIn.Begin();
            btn2SlideIn.Begin();
            btn3SlideIn.Begin();
            btn4SlideIn.Begin();
            btn5SlideIn.Begin();
            btn6SlideIn.Begin();
            btn7SlideIn.Begin();
            btn8SlideIn.Begin();
            btn9SlideIn.Begin();
            btn10SlideIn.Begin();
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

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

            btn1SlideIn.Begin();
            btn2SlideIn.Begin();
            btn3SlideIn.Begin();
            btn4SlideIn.Begin();
            btn5SlideIn.Begin();
            btn6SlideIn.Begin();
            btn7SlideIn.Begin();
            btn8SlideIn.Begin();
            btn9SlideIn.Begin();
            btn10SlideIn.Begin();
        }

        private void btn1SlideIn_Completed(object sender, object e)
        {            
            btn1.Content = queue.Dequeue();
            btn1SlideIn.Begin();
        }

        private void btn2SlideIn_Completed(object sender, object e)
        {
            btn2.Content = queue.Dequeue();
            btn2SlideIn.Begin();
        }

        private void btn3SlideIn_Completed(object sender, object e)
        {
            btn3.Content = queue.Dequeue();
            btn3SlideIn.Begin();
        }

        private void btn4SlideIn_Completed(object sender, object e)
        {
            btn4.Content = queue.Dequeue();
            btn4SlideIn.Begin();
        }

        private void btn5SlideIn_Completed(object sender, object e)
        {
            btn5.Content = queue.Dequeue();
            btn5SlideIn.Begin();
        }

        private void btn6SlideIn_Completed(object sender, object e)
        {
            btn6.Content = queue.Dequeue();
            btn6SlideIn.Begin();
        }

        private void btn7SlideIn_Completed(object sender, object e)
        {
            btn7.Content = queue.Dequeue();
            btn7SlideIn.Begin();
        }

        private void btn8SlideIn_Completed(object sender, object e)
        {
            btn8.Content = queue.Dequeue();
            btn8SlideIn.Begin();
        }

        private void btn9SlideIn_Completed(object sender, object e)
        {
            btn9.Content = queue.Dequeue();
            btn9SlideIn.Begin();
        }

        private void btn10SlideIn_Completed(object sender, object e)
        {
            btn10.Content = queue.Dequeue();
            btn10SlideIn.Begin();
        }
    }
}
