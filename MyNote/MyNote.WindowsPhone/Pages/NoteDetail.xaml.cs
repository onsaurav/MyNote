using MyNote.DB;
using MyNote.Helper;
using MyNote.Model;
using MyNote.ViewModel;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MyNote.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NoteDetail : Page
    {
        private int ID;
        byte[] Photo = null;
        private string PHOTO_PATH;
        DBAccess _DBAccess = new DBAccess();
        UtilityHelper _UtilityHelper = new UtilityHelper();
        Note item = new Note();
        public NoteDetail()
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
            Clear();
            item = new Note();
            item = e.Parameter as Note;
            if (item != null)
                Loadprofile(item);
        }

        private void Loadprofile(Note _Note)
        {
            try
            {
                ID = _Note.Id;
                dpkDate.Text = _Note.Date.ToString();
                tpkTime.Text = new TimeSpan(_Note.Time.Hour, _Note.Time.Minute, _Note.Time.Second).ToString();
                txtSubject.Text = _Note.Subject;
                txtNote.Text = _Note.Text;
                PHOTO_PATH = _Note.PhotoPath;
                txtLatitude.Text = _Note.Latitude != 0 ? _Note.Latitude.ToString() : "";
                txtLongitude.Text = _Note.Longitude != 0 ? _Note.Longitude.ToString() : "";
                if (_Note.Photo != null)
                    imgPhoto.Source = ImageHelper.BytesToBitmapImage(_Note.Photo);
            }
            catch (Exception ex)
            {
                MessageDialogHelper.Show(ex.Message, "Clear");
            }
        }

        private void Clear()
        {
            try
            {
                ID = 0;
                dpkDate.Text = DateTime.Now.Date.ToString();
                tpkTime.Text = DateTime.Now.Hour.ToString() + " : " + DateTime.Now.Minute.ToString() + " : " + DateTime.Now.Second;
                txtSubject.Text = "";
                txtNote.Text = "";
                PHOTO_PATH = "";
                imgPhoto.Source = new BitmapImage();
                txtLatitude.Text = "";
                txtLongitude.Text = "";
            }
            catch (Exception ex)
            {
                MessageDialogHelper.Show(ex.Message, "Clear");
            }
        }

        private void appbarEdit_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NoteItem), item);
        }
    
        private void appbarHelp_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Help));
        }

        private void appbarExit_Click(object sender, RoutedEventArgs e)
        {
            _UtilityHelper.ExitMe();
        }

        private void appbarNotes_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NotesViewer));
        }
    }
}
