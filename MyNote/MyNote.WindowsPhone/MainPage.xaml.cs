using MyNote.Common;
using MyNote.DB;
using MyNote.Helper;
using MyNote.Model;
using MyNote.Pages;
using MyNote.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyNote
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DBAccess _DBAccess = new DBAccess();
        UtilityHelper _UtilityHelper = new UtilityHelper();
        DALRepository _LogInRepository = new DALRepository();

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.

            await _DBAccess.CreateDatabaseAsync();
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CurrentSourcePageType == typeof(MainPage))
            {
                //
            }
            else
            { 
                if (frame.CanGoBack)
                {
                    frame.GoBack();
                    e.Handled = true;
                }
            }
        }
                
        private void appbarAboutus_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutUs));
        }

        private void appbarHelp_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Help));
        }

        private void appbarExit_Click(object sender, RoutedEventArgs e)
        {            
            _UtilityHelper.ExitMe();
        }        

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Profile));
        }        

        private void appbarRating_Click(object sender, RoutedEventArgs e)
        {
            _UtilityHelper.AppsRating();
        }        

        private void btnAboutUs_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutUs));
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Help));
        }

        private void btnDailyNote_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NotesViewer));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
