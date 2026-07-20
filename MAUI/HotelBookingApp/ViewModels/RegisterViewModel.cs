using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace HotelBookingApp.ViewModels
{
    public class RegisterViewModel : BindableObject
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNum { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;

        public Command RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () => await RegisterAsync());
        }

        private async Task RegisterAsync()
        {
            if (Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            var customer = new
            {
                FirstName,
                LastName,
                Email,
                Password,
                PhoneNum
            };

            var json = JsonSerializer.Serialize(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://yourapiurl/api/Customer", content);

            if (response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Account created successfully!", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                await Application.Current.MainPage.DisplayAlert("Error", error, "OK");
            }
        }
    }
}
