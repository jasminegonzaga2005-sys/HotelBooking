using System;
using HotelBookingApp.Models;
using HotelBookingApp.Services;
using Microsoft.Maui.Controls;
//using HotelBookingApp.ViewModels;

namespace HotelBookingApp.Views;

public partial class PaymentPage : ContentPage
{
    private Booking _booking;
    private readonly ApiService _apiService;

    public PaymentPage(ApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

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

        var nights = (_booking.CheckOut - _booking.CheckIn).Days;
        NightsStayedLabel.Text = $"Nights Stayed: {(nights > 0 ? nights : 1)}";

        GuestsLabel.Text = $"Guests: {_booking.NumberOfGuests}";
        TotalLabel.Text = $"₱{_booking.TotalCost:N2}";
    }

    private async void ProcessPayment(string paymentMethod)
    {
        if (_booking == null) return;

        try
        {
            var success = await _apiService.UpdateBookingStatus("Confirmed", _booking.BookingID);

            if (success)
            {
                await DisplayAlert("Success", "Payment received! Thank you", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Payment Failed", "Unable to confirm booking.", "OK");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Payment error using {paymentMethod}: {ex.Message}");
            await DisplayAlert("Error", "An error occurred while processing payment.", "OK");
        }
    }

    private void PayGCash_Clicked(object sender, EventArgs e)
    {
        ProcessPayment("GCash");
    }

    private void PayMaya_Clicked(object sender, EventArgs e)
    {
        ProcessPayment("PayMaya");
    }

    private void PayCredit_Clicked(object sender, EventArgs e)
    {
        ProcessPayment("Credit Card");
    }

    private void PayDebit_Clicked(object sender, EventArgs e)
    {
        ProcessPayment("Debit Card");
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}