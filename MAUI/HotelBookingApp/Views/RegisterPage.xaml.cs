using System;
using Microsoft.Maui.Controls;
using HotelBookingApp.ViewModels; 

namespace HotelBookingApp.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel(); 
        }

        // Handle Register button click
        private async void Register_Clicked(object sender, EventArgs e)
        {
            // Collect values from entries
            string firstName = FirstNameEntry.Text;
            string lastName = LastNameEntry.Text;
            string email = EmailEntry.Text;
            string phone = PhoneEntry.Text;
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            // Basic validation
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            // TODO: Save user data to database or service
            await DisplayAlert("Success", "Account created successfully!", "OK");

            // Navigate back to LoginPage
            await Navigation.PopAsync();
        }

        // Handle Login button click
        private async void Login_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Goes back to LoginPage
        }
    }
}
