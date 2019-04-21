using MyNote.Common;
using MyNote.DB;
using MyNote.Helper;
using MyNote.Model;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MyNote.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NoteItem : Page
    {
        private int ID;
        byte[] Photo = null;
        private string PHOTO_PATH;
        DBAccess _DBAccess = new DBAccess();
        UtilityHelper _UtilityHelper = new UtilityHelper();

        public NoteItem()
        {
            this.InitializeComponent();
            PHOTO_PATH = string.Empty;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mapLocation.MapServiceToken = "15kIr-gFNmdXnRqU56Xn1A";
            Clear();
            Note item = e.Parameter as Note;
            if (item != null)
                Loadprofile(item);
        }

        private void Loadprofile(Note _Note)
        {
            ID =_Note.Id;
            dpkDate.Date = _Note.Date;
            tpkTime.Time = new TimeSpan(_Note.Time.Hour, _Note.Time.Minute, _Note.Time.Second); 
            txtSubject.Text =_Note.Subject;
            txtNote.Text =_Note.Text;
            PHOTO_PATH = _Note.PhotoPath;
            txtLatitude.Text = _Note.Latitude != 0 ? _Note.Latitude.ToString() : "";
            txtLongitude.Text = _Note.Longitude != 0 ? _Note.Longitude.ToString() : "";
            if (_Note.Photo != null)
                imgPhoto.Source = ImageHelper.BytesToBitmapImage(_Note.Photo);
        }

        private void appbarHome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
                      
        private void appbarExit_Click(object sender, RoutedEventArgs e)
        {
            _UtilityHelper.ExitMe();
        }

        private async void appbarSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckObject())
            {
                Note _Note = LoadNoteObject();
                MyResult result = await _DBAccess.SaveNoteAsync(_Note);
                MessageDialogHelper.Show(result.Message, "Note");
                if (result.IsSuccess)
                    Frame.Navigate(typeof(NotesViewer));
            }
        }

        private bool CheckObject()
        {
            if (string.IsNullOrEmpty(txtSubject.Text.Trim()))
            {
                MessageDialogHelper.Show("Subject is empty", "Checking");
                return false;
            }

            if (string.IsNullOrEmpty(txtNote.Text.Trim()))
            {
                MessageDialogHelper.Show("Note is empty", "Checking");
                return false;
            }

            return true;
        }

        private Note LoadNoteObject()
        {
            Note _Note = new Note();
            _Note.Id = ID;
            _Note.Date = dpkDate.Date.Date;
            _Note.Time = DateTime.Parse(tpkTime.Time.ToString());
            _Note.Subject = txtSubject.Text;
            _Note.Text  = txtNote.Text;
            _Note.PhotoPath = PHOTO_PATH;

            double latitude = 0;
            double.TryParse(txtLatitude.Text, out latitude);
            _Note.Latitude = latitude;

            double longitude = 0;
            double.TryParse(txtLongitude.Text, out longitude);
            _Note.Longitude = longitude;
            _Note.Photo = Photo;
            return _Note;
        }

        private void appbarNew_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            ID = 0;
            dpkDate.Date = DateTime.Now;
            tpkTime.Time = new TimeSpan( DateTime.Now.Hour , DateTime.Now.Minute , DateTime.Now.Second);
            txtSubject.Text = "";
            txtNote.Text = "";
            PHOTO_PATH = "";
            imgPhoto.Source = new BitmapImage();
            txtLatitude.Text = "";
            txtLongitude.Text = "";
        }

        private void LoadNote(Note _Note)
        {
            Clear();

            ID = _Note.Id;
            dpkDate.Date = _Note.Date;
            tpkTime.Time = new TimeSpan(_Note.Time.Hour, _Note.Time.Minute, _Note.Time.Second);
            txtSubject.Text = _Note.Subject;
            txtNote.Text = _Note.Text;
            PHOTO_PATH = _Note.PhotoPath;
            if (_Note.Photo != null)
                imgPhoto.Source = ImageHelper.BytesToBitmapImage(_Note.Photo);
            txtLatitude.Text = _Note.Latitude == 0 ? "" : _Note.Latitude.ToString();
            txtLongitude.Text = _Note.Longitude == 0 ? "" : _Note.Longitude.ToString();
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
                filePicker.FileTypeFilter.Add(".gif");
                filePicker.FileTypeFilter.Add(".ico");

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

        private void btnLocation_Click(object sender, RoutedEventArgs e)
        {
            if (!StandardPopup.IsOpen)
                StandardPopup.IsOpen = true;
            if (ID == 0 || txtLatitude.Text.Trim() == string.Empty || txtLongitude.Text.Trim() == string.Empty)
            {
                InitializeMAP();
            }
            else
            {
                InitializeMAP(true);
            }
        }
                        
        private void appbarDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void InitializeMAP(bool ExistingPoint = false)
        {
            mapLocation.MapServiceToken = "abcdef-abcdefghijklmno";
            if (!ExistingPoint)
            {
                BasicGeoposition geoposition = await GetCurrentLocationAsync();
                await ShowPoints(geoposition);
            }
            else
            {
                await ShowPoints(new BasicGeoposition()
                {
                    Latitude = double.Parse(txtLatitude.Text),
                    Longitude = double.Parse(txtLongitude.Text)
                });
            }
        }

        private async Task ShowPoints(BasicGeoposition geoposition)
        {
            MapIcon mapIcon = new MapIcon();
            mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/pin_red.png"));
            mapIcon.Title = "Now Here";
            mapIcon.Location = new Geopoint(geoposition);
            mapIcon.NormalizedAnchorPoint = new Point(0.5, 0.5);
            mapLocation.MapElements.Add(mapIcon);
            await mapLocation.TrySetViewAsync(mapIcon.Location, 16, 0, 0, MapAnimationKind.Bow);

            txtLatitude.Text = geoposition.Latitude.ToString();
            txtLongitude.Text = geoposition.Longitude.ToString();
        }

        static async Task<BasicGeoposition> GetCurrentLocationAsync()
        {
            try
            {
                int LOCATION_AGE_SEC = 10;
                int LOCATION_TIMEOUT_SEC = 5;
                Geolocator geolocator = new Geolocator();
                geolocator.DesiredAccuracyInMeters = 50;
                Geoposition position = await geolocator.GetGeopositionAsync(TimeSpan.FromSeconds(LOCATION_AGE_SEC), TimeSpan.FromSeconds(LOCATION_TIMEOUT_SEC));
                return (position.Coordinate.Point.Position);
            }
            catch
            {
                return (new BasicGeoposition()
                {
                    Latitude = 23.757661,
                    Longitude = 90.386665
                });
            }
        }
        
        private void mapLocation_MapDoubleTapped(MapControl sender, MapInputEventArgs args)
        {
            txtLatitude.Text = args.Location.Position.Latitude.ToString();
            txtLongitude.Text = args.Location.Position.Longitude.ToString();
           
            if (StandardPopup.IsOpen)
                StandardPopup.IsOpen = false;
        }
    }
}
