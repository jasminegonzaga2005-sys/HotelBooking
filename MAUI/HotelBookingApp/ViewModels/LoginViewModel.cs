using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using HotelBookingApp.Services;
using HotelBookingApp.Models;
using HotelBookingApp.Views;

namespace HotelBookingApp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        private readonly DashboardPage _dashboardPage;

        private string _email;
        private string _password;
        private bool _isBusy;

        public LoginViewModel(ApiService apiService, DashboardPage dashboardPage)
        {
            _apiService = apiService;
            _dashboardPage = dashboardPage;

            LoginCommand = new Command(async () => await LoginAsync());
            RegisterCommand = new Command(async () => await GoToRegisterAsync()); // ✅ Added
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; } // ✅ Added

        private async Task LoginAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                var customer = await _apiService.LoginAsync(Email, Password);

                if (customer != null)
                {
                    App.CurrentUser = customer;
                    await Application.Current.MainPage.Navigation.PushAsync(_dashboardPage);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Login Failed", "Invalid email or password.", "OK");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GoToRegisterAsync()
        {
           
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }
}
