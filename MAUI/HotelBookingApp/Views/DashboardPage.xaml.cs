using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
    }

    private async void BookRoom_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Book Room", "This page is under development.", "OK");

        // Later:
        // await Navigation.PushAsync(new BookARoomPage());
    }

    private async void MyBookings_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("My Bookings", "This page is under development.", "OK"); //To be edit for navigation 
    }

    private async void Home_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Home", "You are already on the Homepage.", "OK"); 
    }

    private async void Profile_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Profile", "This page is under development.", "OK"); //To be edit for navigation 
    }

    private async void StandardRoom_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Standard Room", "Room details will be shown here.", "OK"); //To be edit for navigation to StandardRoomPage
    }

    private async void SuperiorRoom_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Superior Room", "Room details will be shown here.", "OK"); //To be edit for navigation to SuperiorRoomPage
    }

    private async void DeluxeRoom_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Deluxe Room", "Room details will be shown here.", "OK"); //To be edit for navigation to DeluxeRoomPage
    }

    private async void FamilyRoom_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Family Room", "Room details will be shown here.", "OK"); //To be edit for navigation to FamilyRoomPage
    }

    private async void ExecutiveSuite_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Executive Suite", "Room details will be shown here.", "OK"); //To be edit for navigation to ExecutiveSuitePage
    }
}