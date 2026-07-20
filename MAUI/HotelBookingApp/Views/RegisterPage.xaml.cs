using HotelBookingApp.Services;
using HotelBookingApp.ViewModels;

namespace HotelBookingApp.Views
{
    public partial class RegisterPage : ContentPage
    {
        private readonly ApiService _apiService;
        public RegisterPage(ApiService apiService)
        {
            InitializeComponent();

            // Set the BindingContext for MVVM
            InitializeComponent();
            _apiService = apiService;
        }

        // Navigate back to the Login page
        private async void Login_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}