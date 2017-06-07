using CheckTickets.Views;
using System.Windows.Input;

namespace CheckTickets.ViewModels
{
    class MainViewModel
    {
        #region Properties
        public LoginViewModel Login { get; set; }
        #endregion

        public MainViewModel()
        {
            Login = new LoginViewModel();
        }
    }
}