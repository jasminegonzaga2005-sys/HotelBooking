using HotelBookingApp.Models;
using HotelBookingApp.Services;

namespace HotelBookingApp.Views;

public partial class PaymentPage : ContentPage
{
    private readonly Booking _booking;
    private readonly ApiService _apiService;



    public PaymentPage(
        Booking booking,
        ApiService apiService)
    {
        InitializeComponent();

        _booking = booking;
        _apiService = apiService;


        LoadBookingInfo();
    }




    private void LoadBookingInfo()
    {
        RoomLabel.Text =
            $"Room: {_booking.Room.RoomNum} ({_booking.Room.RoomType.RoomTypeName})";


        CheckInLabel.Text =
            $"Check-in: {_booking.CheckIn:MMMM dd, yyyy}";


        CheckOutLabel.Text =
            $"Check-out: {_booking.CheckOut:MMMM dd, yyyy}";


        GuestsLabel.Text =
            $"Guests: {_booking.NumberOfGuests}";


        TotalLabel.Text =
            $"₱{_booking.TotalCost:N2}";
    }





    private async void Pay_Clicked(object sender, EventArgs e)
    {
        try
        {
            var success = await _apiService.UpdateBookingStatus(
                "Confirmed",
                _booking.BookingID
            );


            if (success)
            {
                await DisplayAlert(
                    "Payment Successful",
                    "Your booking has been confirmed.",
                    "OK"
                );


                // Return to My Bookings
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert(
                    "Payment Failed",
                    "Unable to confirm booking.",
                    "OK"
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Payment error: {ex.Message}"
            );


            await DisplayAlert(
                "Error",
                "An error occurred while processing payment.",
                "OK"
            );
        }
    }





    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}