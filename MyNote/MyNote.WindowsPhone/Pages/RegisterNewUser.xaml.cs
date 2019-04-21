using MyNote.Common;
using MyNote.DB;
using MyNote.Helper;
using MyNote.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MyNote.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterNewUser : Page
    {
        public int ID = 0;
        public string PHOTO_PATH = string.Empty;
        Byte[] Photo = null;

        DBAccess _DBAccess = new DBAccess();
        UtilityHelper _UtilityHelper = new UtilityHelper();

        public RegisterNewUser()
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
                ID = user.Id;
                txtFullName.Text = user.Name;
                dpkDOB.Date = DateTime.Now;
                txtDetails.Text = user.Details;
                if (user.Photo != null)
                {
                    Photo = user.Photo;
                    imgPhoto.Source = ImageHelper.BytesToBitmapImage(user.Photo);
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (CheckObject())
            {
                User _User = LoadUserObject();
                MyResult result = await _DBAccess.SaveUserAsync(_User);
                MessageDialogHelper.Show(result.Message, "User");
                if (result.IsSuccess)
                {
                    Frame.Navigate(typeof(Profile));
                }
            }
        }

        private bool CheckObject()
        {
            if (string.IsNullOrEmpty(txtFullName.Text.Trim()))
            {
                MessageDialogHelper.Show("Name is empty", "Checking");
                return false;
            }

            return true;
        }

        private void txtFullName_KeyDown(object sender, KeyRoutedEventArgs e)
        {

        }       

        private void dpkDOB_KeyDown(object sender, KeyRoutedEventArgs e)
        {

        }

        private void txtDetails_KeyDown(object sender, KeyRoutedEventArgs e)
        {

        }

        private void btnLoadImage_Click(object sender, RoutedEventArgs e)
        {
            SelectImageFromPhone();            
        }

        public void SelectImageFromPhone()
        {
            try
            {
                CoreApplicationView view = CoreApplication.GetCurrentView();

                FileOpenPicker filePicker = new FileOpenPicker();
                filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                filePicker.ViewMode = PickerViewMode.Thumbnail;

                filePicker.FileTypeFilter.Clear();
                filePicker.FileTypeFilter.Add(".bmp");
                filePicker.FileTypeFilter.Add(".png");
                filePicker.FileTypeFilter.Add(".jpeg");
                filePicker.FileTypeFilter.Add(".jpg");

                filePicker.PickSingleFileAndContinue();
                view.Activated += viewActivated;
            }
            catch (Exception ex)
            {
                MessageDialogHelper.Show(ex.Message, "Error!");
            }
        }

        private async void viewActivated(CoreApplicationView sender, IActivatedEventArgs args1)
        {
            FileOpenPickerContinuationEventArgs args = args1 as FileOpenPickerContinuationEventArgs;

            if (args != null)
            {
                if (args.Files.Count == 0) return;
                StorageFile _StorageFile = args.Files[0];
                imgPhoto.Source = new BitmapImage(new Uri("file://" + _StorageFile.Path));
                Photo = await ImageHelper.GetBytesFromStorageFile(_StorageFile);
            }
        }

        public async Task<Image> GetImageAsync(StorageFile storageFile)
        {
            BitmapImage bitmapImage = new BitmapImage();
            FileRandomAccessStream stream = (FileRandomAccessStream)await storageFile.OpenAsync(FileAccessMode.Read);
            bitmapImage.SetSource(stream);
            Image image = new Image();
            image.Source = bitmapImage;            
            return image;
        }

        private void appbarAboutus_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutUs));
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
             
        private User LoadUserObject()
        {
            User _User = new User();
            _User.Id = ID;
            _User.Name = txtFullName.Text;
            _User.PhotoPath = PHOTO_PATH;
            _User.DateOfBirth = dpkDOB.Date.Date;
            _User.Details = txtDetails.Text;      
            _User.Photo = Photo;
            return _User;
        }

        private void Clear()
        {
            ID = 0;
            txtFullName.Text = "";
            PHOTO_PATH="";
            txtDetails.Text = "";
            dpkDOB.Date = DateTime.Now;
            imgPhoto.Source = new BitmapImage();
        }

        private void LoadUser(User _User)
        {
            Clear();
            ID = _User.Id;
            txtFullName.Text = _User.Name;
            PHOTO_PATH = _User.PhotoPath;
            dpkDOB.Date = DateTime.Parse(_User.DateOfBirth.ToString());
            txtDetails.Text = _User.Details;
            if (_User.Photo != null)
                imgPhoto.Source = ImageHelper.BytesToBitmapImage(_User.Photo);
        }
    }
}
