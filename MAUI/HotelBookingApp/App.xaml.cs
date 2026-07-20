using HotelBookingApp.Models;
using HotelBookingApp.Views;

namespace HotelBookingApp
{
    public partial class App : Application
    {
        public static Customer? CurrentUser { get; set; }

        public App(Splashpage splashpage)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Splashpage());
        }
    }
}