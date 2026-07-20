using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using HotelBookingApp.Services;
using HotelBookingApp.Views;

namespace HotelBookingApp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;

        private string _email = string.Empty;
        private string _password = string.Empty;
        private bool _isBusy;

        public LoginViewModel(ApiService apiService)
        {
            _apiService = apiService;

            LoginCommand = new Command(async () => await LoginAsync());
            RegisterCommand = new Command(async () => await GoToRegisterAsync());
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

        public ICommand RegisterCommand { get; }

        private async Task LoginAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var customer = await _apiService.LoginAsync(Email, Password);

                if (customer != null)
                {
                    App.CurrentUser = customer;

                    var dashboardPage =
                        Application.Current?.Windows[0].Page?
                        .Handler?.MauiContext?.Services
                        .GetService<DashboardPage>();

                    if (dashboardPage != null)
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(dashboardPage);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Login Failed",
                        "Invalid email or password.",
                        "OK");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GoToRegisterAsync()
        {
            var registerPage =
                Application.Current?.Windows[0].Page?
                .Handler?.MauiContext?.Services
                .GetService<RegisterPage>();

            if (registerPage != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(registerPage);
            }
        }
    }
}