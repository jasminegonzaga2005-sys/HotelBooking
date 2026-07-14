using HotelBookingApp.ViewModels;
using HotelBookingApp.Views.Rooms;

namespace HotelBookingApp.Views;

public partial class MyBookingsPage : ContentPage
{
    private readonly MyBookingsViewModel _viewModel;


    public MyBookingsPage(MyBookingsViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = _viewModel = viewModel;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadBookings();
    }



    private async void ContinuePayment_Clicked(object sender, EventArgs e)
    {
        if (_viewModel.SelectedBooking == null)
        {
            await DisplayAlert(
                "No Booking Selected",
                "Please select a booking first.",
                "OK");

            return;
        }


        await Navigation.PushAsync(new PaymentPage());
    }



    private async void CancelBooking_Clicked(object sender, EventArgs e)
    {
        if (_viewModel.SelectedBooking == null)
        {
            await DisplayAlert(
                "No Booking Selected",
                "Please select a booking first.",
                "OK");

            return;
        }


        bool answer = await DisplayAlert(
            "Cancel Booking",
            "Are you sure you want to cancel this booking?",
            "Yes",
            "No");


        if (answer)
        {
            await DisplayAlert(
                "Cancelled",
                "Your booking has been cancelled.",
                "OK");

            // Later:
            // await _viewModel.CancelBooking();
        }
    }
}