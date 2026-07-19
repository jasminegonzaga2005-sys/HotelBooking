using System;
using HotelBookingApp.Models;
using HotelBookingApp.Services;
using Microsoft.Maui.Controls;

namespace HotelBookingApp.Views;

public partial class PaymentPage : ContentPage
{
    private Booking _booking;
    private readonly ApiService _apiService;

    // The constructor now only requires what the DI container can inject automatically
    public PaymentPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    /// <summary>
    /// Receives the booking data at runtime from the calling page and populates the UI labels.
    /// </summary>
    public void InitializeBooking(Booking booking)
    {
        _booking = booking;
        LoadBookingInfo();
    }

    private void LoadBookingInfo()
    {
        if (_booking == null) return;

        RoomLabel.Text = $"Room: {_booking.Room.RoomNum} ({_booking.Room.RoomType.RoomTypeName})";
        RoomRateLabel.Text = $"Room Rate per Night: ₱{_booking.Room.RoomType.PricePerNight:N2}";
        CheckInLabel.Text = $"Check-in: {_booking.CheckIn:MMMM dd, yyyy}";
        CheckOutLabel.Text = $"Check-out: {_booking.CheckOut:MMMM dd, yyyy}";
        GuestsLabel.Text = $"Guests: {_booking.NumberOfGuests}";
        TotalLabel.Text = $"₱{_booking.TotalCost:N2}";
    }

    private async void Pay_Clicked(object sender, EventArgs e)
    {
        if (_booking == null) return;

        try
        {
            var success = await _apiService.UpdateBookingStatus("Confirmed", _booking.BookingID);

            if (success)
            {
                await DisplayAlert("Payment Successful", "Your booking has been confirmed.", "OK");
                await Navigation.PopAsync(); // Return to My Bookings
            }
            else
            {
                await DisplayAlert("Payment Failed", "Unable to confirm booking.", "OK");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Payment error: {ex.Message}");
            await DisplayAlert("Error", "An error occurred while processing payment.", "OK");
        }
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}