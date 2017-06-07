using CheckTickets.ViewModels;

namespace CheckTickets.Infrastructure
{
    class InstanceLocator
    {
        public MainViewModel Main { get; set; }
        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
