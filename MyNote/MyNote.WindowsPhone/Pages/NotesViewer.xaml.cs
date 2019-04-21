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
using MyNote.Common;
using MyNote.Model;
using MyNote.DB;
using MyNote.Helper;
using MyNote.Repository;
using Windows.UI.Popups;
using MyNote.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MyNote.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotesViewer : Page
    {
        int NoteId = 0;
        DBAccess _DBAccess = new DBAccess();
        UtilityHelper _UtilityHelper = new UtilityHelper();
        DALRepository _DALRepository = new DALRepository();

        public NotesViewer()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {            
            await LoadNotes();
        }

        private async System.Threading.Tasks.Task LoadNotes()
        {
            try
            {
                List<NoteView> list = new List<NoteView>();
                list = await _DBAccess.LoadNotesByUserAsync();
                list = list.OrderByDescending(x => x.Date).ToList();
                lvwDailyNotes.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageDialogHelper.Show(ex.Message, "Loading");
            }
        }

        private void appbarNew_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NoteItem));
        }
        
        private async void DeleteSelectedNote()
        {
            try
            {
                MyResult _MyResult = await _DALRepository.DeleteNote(NoteId);
                MessageDialogHelper.Show(_MyResult.Message, "Delete");
                if (_MyResult.IsSuccess)
                    await LoadNotes();
            }
            catch (Exception ex)
            {
                MessageDialogHelper.Show(ex.Message, "Delete");
            }
        }

        private async void ConformToDeleteNote(string Text, string Title)
        {
            MessageDialog msg = new MessageDialog(Text, Title);
            msg.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandHandlers)));
            msg.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandHandlers)));

            await msg.ShowAsync();
        }

        private void CommandHandlers(IUICommand commandLabel)
        {
            var Actions = commandLabel.Label;
            switch (Actions)
            {
                case "Yes":
                    DeleteSelectedNote();
                    break;
                case "No":
                    break;
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
        
        private static Note GetNote(NoteView item)
        {
            Note nItem = new Note();
            nItem.Id = item.Id;
            nItem.Subject = item.Subject;
            nItem.Date = item.Date;
            nItem.Time = item.Time;
            nItem.Latitude = item.Latitude;
            nItem.Longitude = item.Longitude;
            nItem.PhotoPath = item.PhotoPath;
            nItem.Text = item.Text;
            nItem.Photo = item.Photo;
            return nItem;
        }

        private void brdEdit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (lvwDailyNotes.SelectedItems.Count > 0)
            {
                NoteView item = new NoteView();
                item = (NoteView)lvwDailyNotes.SelectedItems[0];
                Note nItem = GetNote(item);
                Frame.Navigate(typeof(NoteDetail), nItem);
            }
        }

        private void brdDelete_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NoteId = 0;
            Border B = sender as Border;
            NoteId = int.Parse(B.Tag.ToString());
            ConformToDeleteNote("Are you sure, you want to delete this note?", "Delete");
        }

    }
}
