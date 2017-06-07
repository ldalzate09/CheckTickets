using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CheckTickets.ViewModels 
{
    class LoginViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Atributos
        private string message;
        #endregion

        #region Properties
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

        public String Email { get; set; }
        public String Password { get; set; }
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
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar un Email",
                    "Aceptar");
                return;
            }
        }
        #endregion
    }
}
