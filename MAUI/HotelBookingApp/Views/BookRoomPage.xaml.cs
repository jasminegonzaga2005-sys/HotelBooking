using System;

namespace HotelBookingApp.Views;

public partial class BookRoomPage : ContentPage
{
    public BookRoomPage()
    {
        InitializeComponent();
    }

    private async void BookNow_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Booking", "Booking feature is under development.", "OK");

        // await Navigation.PushAsync(new PaymentPage());
    }
}