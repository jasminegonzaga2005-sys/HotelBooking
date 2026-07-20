using System;
using System.Linq;
using HotelBookingApp.Services;
using HotelBookingApp.ViewModels;
using Microsoft.Maui.Controls;

namespace HotelBookingApp.Views;

public partial class MyBookingsPage : ContentPage
{
    private readonly MyBookingsViewModel _viewModel;
    private readonly ApiService _apiService;
    private readonly BookRoomPage _bookRoomPage;

    public MyBookingsPage(MyBookingsViewModel viewModel, ApiService apiService, BookRoomPage bookRoomPage)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
        _apiService = apiService;
        _bookRoomPage = bookRoomPage;
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

        // Resolves the Page with its registered ApiService instance cleanly
        var paymentPage = Handler?.MauiContext?.Services.GetService<PaymentPage>();
        if (paymentPage != null)
        {
            // Explicitly pass the runtime booking details down
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

    // --- NAVIGATION BAR HANDLERS ---

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
        if (_bookRoomPage == null)
            return;

        if (Navigation.NavigationStack.LastOrDefault() == _bookRoomPage)
            return;

        if (Navigation.NavigationStack.Contains(_bookRoomPage))
        {
            while (Navigation.NavigationStack.LastOrDefault() != _bookRoomPage && Navigation.NavigationStack.Count > 1)
            {
                await Navigation.PopAsync();
            }
        }
        else
        {
            await Navigation.PushAsync(_bookRoomPage);
        }
    }

    private async void MyBookings_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("My Bookings", "You are already on the My Bookings page.", "OK");
    }
}