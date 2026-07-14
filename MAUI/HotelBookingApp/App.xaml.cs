using HotelBookingApp.Models;

namespace HotelBookingApp
{
    public partial class App : Application
    {
        public static Customer? CurrentUser { get; set; } // Property to hold the logged-in customer
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
