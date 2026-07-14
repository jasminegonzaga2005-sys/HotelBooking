using HotelBookingApp.Models;
using HotelBookingApp.Views;

namespace HotelBookingApp
{
    public partial class App : Application
    {
        public static Customer? CurrentUser { get; set; }

        public App(LoginPage loginPage)
        {
            InitializeComponent();

            MainPage = new NavigationPage(loginPage);
        }
    }
}