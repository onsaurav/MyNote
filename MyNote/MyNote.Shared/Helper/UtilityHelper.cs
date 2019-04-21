using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace MyNote.Helper
{
    public class UtilityHelper
    {
        public async void ExitMe()
        {
            MessageDialog msg = new MessageDialog("Are you sure, you want to exit this apps?", "Exit!");

            msg.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(CommandHandlers)));
            msg.Commands.Add(new UICommand("No", new UICommandInvokedHandler(CommandHandlers)));

            await msg.ShowAsync();
        }

        public void CommandHandlers(IUICommand commandLabel)
        {
            var Actions = commandLabel.Label;
            switch (Actions)
            {
                case "Yes":
                    Application.Current.Exit();
                    break;
                case "No":
                    break;
            }
        }

        public async void AppsRating()
        {
            try
            {
                //Windows.ApplicationModel.Package.Current.Id.Name
                 await Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + "709a5edf-3e58-438d-95bc-98a62e4f372a"));
            }
            catch (Exception ex)
            {
                MessageDialog msg = new MessageDialog(ex.Message, "Opps!");
                //await msg.ShowAsync();
            }
        }
    }
}
