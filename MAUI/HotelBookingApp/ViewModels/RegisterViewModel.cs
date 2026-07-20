using System.Threading.Tasks;
using System.Windows.Input;
using HotelBookingApp.Models;
using HotelBookingApp.Services;
using Microsoft.Maui.Controls;
using HotelBookingApp.Views;
using HotelBookingApp.ViewModels;

namespace HotelBookingApp.ViewModels
{
    public class RegisterViewModel : BindableObject
    {
        private readonly ApiService _apiService;
        private readonly DashboardPage _dashboardPage;

        public RegisterViewModel(ApiService apiService, DashboardPage dashboardPage )
        {
            _apiService = apiService;
            RegisterCommand = new Command(async () => await RegisterAsync());
            _dashboardPage = dashboardPage;
        }

        // Bound properties
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNum { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string Role {  get; set; } = "Customer";

        public ICommand RegisterCommand { get; }

        private async Task RegisterAsync()
        {
            if (Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            var newCustomer = new Customer
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
                PhoneNum = PhoneNum,
                Role = "Customer"
            };

            try
            {
                var createdCustomer = await _apiService.CreateCustomerAsync(newCustomer);

                if (createdCustomer)
                {
                    App.CurrentUser = newCustomer; // auto-login
                    await Application.Current.MainPage.DisplayAlert("Success", "Account created successfully!", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(_dashboardPage);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Registration failed.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
