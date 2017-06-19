using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CheckTickets.Services;
using CheckTickets.Models;

namespace CheckTickets.ViewModels
{
    public class LoginViewModel : User, INotifyPropertyChanged
    {

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Atributes
        private NavigationService navigationService;
        private String message;
        private ApiService apiService;
        private bool isRunning;
        private DialogService dialogService;
        #endregion

        #region Properties

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }
        public string Message
        {
            set
            {
                if (message != value)
                {
                    message = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Message"));
                }
            }
            get
            {
                return message;
            }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            navigationService = new NavigationService();
            apiService = new ApiService();
            dialogService = new DialogService();
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }

        private async void Login()
        {
            if (Email == null)
            {
                await dialogService.ShowMessage(
                    "Error",
                    "Ingrese su Email");
                return;
            }
            if (Password == null)
            {
                await dialogService.ShowMessage(
                    "Error",
                    "Ingrese su Password");
                return;
            }
            IsRunning = true;
            User userLogin = new User
            {
                Email = Email,
                Password = Password

            };
            var response = await apiService.Post<User>(
                "http://checkticketsback.azurewebsites.net",
                "/api",
                "/Users/Login",
                userLogin);

            IsRunning = false;
            if (!response.IsSuccess)
            {
                Message = response.Message;
                if (Message.Equals("NotFound", StringComparison.CurrentCultureIgnoreCase))
                {
                    Message = "El usuario o la clave ingresada no son correctos";
                }
                await App.Current.MainPage.DisplayAlert(
                "Error",
                Message,
                "Aceptar");
                return;
            }
            userLogin = (User)response.Result;

            this.UserId = userLogin.UserId;
            this.Email = userLogin.Email;
            this.FirstName = userLogin.FirstName;
            this.FullName = userLogin.FullName;
            this.LastName = userLogin.LastName;
            this.Password = userLogin.Password;

            Message = "";
            var maninViewModel = MainViewModel.GetInstance();
            maninViewModel.CheckTicket = new CheckTicketViewModel(this);
            await navigationService.Navigate("CheckTicketPage");
        }
        #endregion
    }
}
