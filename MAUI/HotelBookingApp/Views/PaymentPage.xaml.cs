using HotelBookingApp.Views.Rooms;

namespace HotelBookingApp.Views;

public partial class PaymentPage : ContentPage
{
    public PaymentPage()
    {
        InitializeComponent();
    }

    private async void Pay_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Payment", "Payment feature is under development.", "OK"); //you can replace this with actual payment processing logic if you want
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync(); //bring back to booking page
    }
}