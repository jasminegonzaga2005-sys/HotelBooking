using System;
using HotelBookingApp.Services;
using HotelBookingApp.ViewModels;

namespace HotelBookingApp.Views;

public partial class MyBookingsPage : ContentPage
{
    private readonly MyBookingsViewModel _viewModel;
    private readonly ApiService _apiService;

    public MyBookingsPage(MyBookingsViewModel viewModel, ApiService apiService)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
        _apiService = apiService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadBookings();
    }

    private async void ContinuePayment_Clicked(object sender, EventArgs e)
    {
        var booking = _viewModel.SelectedBooking;

        if (booking == null)
        {
            await DisplayAlert("No Booking Selected", "Please select a booking first.", "OK");
            return;
        }

        if (!booking.BookingStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
        {
            await DisplayAlert("Cannot Continue", "Only pending bookings can continue to payment.", "OK");
            return;
        }

        var paymentPage = Handler?.MauiContext?.Services.GetService<PaymentPage>();

        if (paymentPage != null)
        {
            paymentPage.InitializeBooking(booking);
            await Navigation.PushAsync(paymentPage);
        }
    }

    private async void CancelBooking_Clicked(object sender, EventArgs e)
    {
        var booking = _viewModel.SelectedBooking;

        if (booking == null)
        {
            await DisplayAlert("No Booking Selected", "Please select a booking first.", "OK");
            return;
        }

        if (!booking.BookingStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
        {
            await DisplayAlert("Cannot Cancel Booking", "Only pending bookings can be cancelled.", "OK");
            return;
        }

        bool answer = await DisplayAlert(
            "Cancel Booking",
            $"Are you sure you want to cancel Room {booking.Room.RoomNum}?",
            "Yes",
            "No");

        if (!answer)
            return;

        var bookingCancelled = await _viewModel.UpdateBookingStatus("Cancelled", booking.BookingID);

        if (!bookingCancelled)
        {
            await DisplayAlert("Error", "Unable to cancel booking.", "OK");
            return;
        }

        var roomUpdated = await _viewModel.UpdateRoomStatus("Available", booking.RoomID);

        if (!roomUpdated)
        {
            await DisplayAlert("Warning", "Booking cancelled but room status failed to update.", "OK");
        }
        else
        {
            await DisplayAlert("Cancelled", "Your booking has been cancelled.", "OK");
        }

        await _viewModel.LoadBookings();
        _viewModel.SelectedBooking = null;
    }

    private void AgreeCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            DisplayAlert("Agreement", "You checked the box.", "OK");
        }
        else
        {
            DisplayAlert("Agreement", "You unchecked the box.", "OK");
        }
    }

    private async void CloseModal_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void AcceptModal_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Accepted", "You've confirmed your booking.", "OK");

        var booking = _viewModel.SelectedBooking;

        if (booking != null)
        {
            var paymentPage = Handler?.MauiContext?.Services.GetService<PaymentPage>();

            if (paymentPage != null)
            {
                paymentPage.InitializeBooking(booking);
                await Navigation.PushAsync(paymentPage);
            }
        }

        await Navigation.PopModalAsync();
    }

    private async void Home_Clicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
        {
            await Navigation.PopToRootAsync();
        }
        else
        {
            await DisplayAlert("Home", "You are already on the homepage.", "OK");
        }
    }

    private async void BookRoom_Clicked(object sender, EventArgs e)
    {
        var bookRoomPage = Handler?.MauiContext?.Services.GetService<BookRoomPage>();

        if (bookRoomPage != null)
        {
            await Navigation.PushAsync(bookRoomPage);
        }
    }

    private async void MyBookings_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("My Bookings", "You are already on the My Bookings page.", "OK");
    }
}