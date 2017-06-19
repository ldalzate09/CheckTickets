using CheckTickets.Views;
using System.Threading.Tasks;

namespace CheckTickets.Services
{
    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
                case "CheckTicketPage":
                    await App.Current.MainPage.Navigation.PushAsync(new CheckTicketPage());
                    break;
                default:
                    break;
            }
        }

        public async Task Back()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
