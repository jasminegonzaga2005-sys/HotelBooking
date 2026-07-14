using System.Collections.ObjectModel;
using HotelBookingApp.Models;
using HotelBookingApp.Services;

namespace HotelBookingApp.ViewModels
{
    public class MyBookingsViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;


        public ObservableCollection<Booking> Bookings { get; } = new();



        private Booking? _selectedBooking;

        public Booking? SelectedBooking
        {
            get => _selectedBooking;
            set => SetProperty(ref _selectedBooking, value);
        }


        public MyBookingsViewModel(ApiService apiService)
        {
            _apiService = apiService;

            LoadBookings();
        }



        public async Task LoadBookings()
        {
            if (App.CurrentUser == null)
            {
                Console.WriteLine("No current user found");
                return;
            }


            var bookings = await _apiService.GetBookings(
                App.CurrentUser.CustomerID
            );


            Bookings.Clear();


            foreach (var booking in bookings)
            {
                Bookings.Add(booking);
            }
        }
    }
}