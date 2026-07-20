using System;
using Microsoft.Maui.Controls;
using HotelBookingApp.Views;

namespace HotelBookingApp.Views
{
    // The namespace and class name now perfectly match the XAML x:Class
    public partial class Splashpage : ContentPage
    {
        public Splashpage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Continue button click event and navigates to the LoginPage.
        /// </summary>
        //private async void OnContinueClicked(object sender, EventArgs e)
        //{
        //    // Navigates to the LoginPage inside the same folder
        //    //await Navigation.PushAsync(new LoginPage());
        //}

        private async void OnContinueClicked(object sender, EventArgs e)
        {
            var loginPage = Handler.MauiContext.Services.GetService<LoginPage>();
            await this.FadeTo(0, 300);
            await Navigation.PushAsync(loginPage);
        }
    }
}