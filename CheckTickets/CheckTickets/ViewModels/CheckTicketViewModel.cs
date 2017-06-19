using CheckTickets.Models;
using CheckTickets.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CheckTickets.ViewModels
{
    public class CheckTicketViewModel: Ticket, INotifyPropertyChanged
    {

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Atributes
        private NavigationService navigationService;
        private String message;
        private String color;
        private bool isRunning;
        private ApiService apiService;
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


        public string Color
        {
            set
            {
                if (color != value)
                {
                    color = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color"));
                }
            }
            get
            {
                return color;
            }
        }

        #endregion

        #region Constructors
        public CheckTicketViewModel(User user)
        {
            navigationService = new NavigationService();
            apiService = new ApiService();
            Color = "Green";
            this.UserId = user.UserId;
            this.DateTime = DateTime.Now;
        } 
        #endregion

        #region Commands
        public ICommand CheckTicketCommand
        {
            get { return new RelayCommand(CheckTicket); }
        }

        private async void CheckTicket()
        {
            if (TicketCode == null)
            {
                Message =  "Ingrese el ticket";
                Color = "Red";
                return;
            }


            IsRunning = true;
            var response = await apiService.GetTicket(
                "http://checkticketsback.azurewebsites.net",
                "/api",
                "/Tickets/",
                this);

            if (response.IsSuccess)
            {
                Message = "Ticket ya leido";
                Color = "Red";
                IsRunning = false;
                return;
            }

            response = await apiService.Post<Ticket>(
                "http://checkticketsback.azurewebsites.net",
                "/api",
                "/Tickets",
                this);
            IsRunning = false;
            if (!response.IsSuccess)
            {
                Message = "Error al registrar el ticket";
                Color = "Red";
                return;
            }

            Message = this.TicketCode + ", Acceso Autorizado";
            Color = "Green";


        }
        #endregion
    }
}
