using MyNote.Common;
using MyNote.DB;
using MyNote.Helper;
using MyNote.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MyNote.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Profile : Page
    {
        DBAccess _DBAccess = new DBAccess();
        UtilityHelper _UtilityHelper = new UtilityHelper();

        public Profile()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Loadprofile();
        }

        private async void Loadprofile()
        {
            User user = await _DBAccess.SearchUserByNameAsync();
            if (user != null)
            {
                txtFullName.Text = user.Name;
                txtDetails.Text = user.Details;
                if (user.Photo != null)
                    imgPhoto.Source = ImageHelper.BytesToBitmapImage(user.Photo);
                txtDOB.Text = user.DateOfBirth.Date.ToString("dd/MM/yyyy");
                List<Note> list = await _DBAccess.SearchNoteByUserAsync();
                txtDailyNotes.Text = list.Count.ToString();
            }
        }

        private void appbarHome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void appbarHelp_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Help));
        }

        private void appbarRating_Click(object sender, RoutedEventArgs e)
        {
            _UtilityHelper.AppsRating();
        }

        private void appbarExit_Click(object sender, RoutedEventArgs e)
        {            
            _UtilityHelper.ExitMe();
        }

        private void appbarEdit_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterNewUser));
        }
    }
}
