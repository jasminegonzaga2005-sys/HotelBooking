using Microsoft.Maui.Controls;

namespace HotelBookingApp.Views;

public partial class MyBookingsPage : ContentPage
{
    public MyBookingsPage()
    {
        InitializeComponent();
    }

    private async void ContinuePayment_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Payment", "Proceeding to payment...", "OK");

        // await Navigation.PushAsync(new PaymentPage());
    }

    private async void CancelBooking_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert(
            "Cancel Booking",
            "Are you sure you want to cancel this booking?",
            "Yes",
            "No");

        if (answer)
        {
            await DisplayAlert("Cancelled", "Your booking has been cancelled.", "OK"); //need magreflect sa database
        }
    }
}