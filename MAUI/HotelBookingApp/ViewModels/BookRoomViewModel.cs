using System.Collections.ObjectModel;
using System.Windows.Input;
using HotelBookingApp.Models;
using HotelBookingApp.Services;

namespace HotelBookingApp.ViewModels
{
    public class BookRoomViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;

        public ObservableCollection<Room> AvailableRooms { get; } = new();



        private Room? _selectedRoom;
        public Room? SelectedRoom
        {
            get => _selectedRoom;
            set => SetProperty(ref _selectedRoom, value);
        }



        private DateTime _checkInDate = DateTime.Today;
        public DateTime CheckInDate
        {
            get => _checkInDate;
            set => SetProperty(ref _checkInDate, value);
        }



        private DateTime _checkOutDate = DateTime.Today.AddDays(1);
        public DateTime CheckOutDate
        {
            get => _checkOutDate;
            set => SetProperty(ref _checkOutDate, value);
        }



        private string _guests = string.Empty;
        public string Guests
        {
            get => _guests;
            set => SetProperty(ref _guests, value);
        }



        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private RoomType? _selectedRoomType;
        public RoomType? SelectedRoomType
        {
            get => _selectedRoomType;
            set
            {
                if (SetProperty(ref _selectedRoomType, value))
                {
                    FilterRoomsByType();
                }
            }
        }

        private string _roomIdInput;
        public string RoomIdInput
        {
            get => _roomIdInput;
            set
            {
                if (SetProperty(ref _roomIdInput, value))
                    TrySelectRoomById();
            }
        }


        public ICommand SelectRoomCommand { get; }

        public ICommand BookRoomCommand { get; }
        public ObservableCollection<RoomType> RoomTypes { get; } = new();
        public ObservableCollection<Room> FilteredRooms { get; } = new();

        private void TrySelectRoomById()
        {
            if (int.TryParse(RoomIdInput, out int id))
                SelectedRoom = AvailableRooms.FirstOrDefault(r => r.RoomID == id);
        }

        private void FilterRoomsByType()
        {
            FilteredRooms.Clear();
            if (SelectedRoomType == null) return;

            var filtered = AvailableRooms
                .Where(r => r.RoomType.RoomTypeID == SelectedRoomType.RoomTypeID)
                .ToList();

            foreach (var room in filtered)
                FilteredRooms.Add(room);

            Console.WriteLine($"Filtered {FilteredRooms.Count} rooms for {SelectedRoomType.RoomTypeName}");
        }

        public BookRoomViewModel(ApiService apiService)
        {
            _apiService = apiService;


            SelectRoomCommand = new Command<Room>(SelectRoom);

            BookRoomCommand = new Command(async () => await BookRoomAsync());


            _ = LoadAvailableRooms();
        }





        private void SelectRoom(Room room)
        {
            if (room == null)
                return;


            SelectedRoom = room;


            Console.WriteLine(
                $"Selected Room: {room.RoomNum} - {room.RoomType.RoomTypeName}"
            );
        }





        //private async Task LoadAvailableRooms()
        //{
        //    try
        //    {
        //        AvailableRooms.Clear();


        //        var rooms = await _apiService.GetAvailableRooms();


        //        if (rooms == null)
        //            return;



        //        foreach (var room in rooms)
        //        {
        //            AvailableRooms.Add(room);
        //        }


        //        Console.WriteLine(
        //            $"Loaded {AvailableRooms.Count} available rooms"
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(
        //            $"Load rooms error: {ex.Message}"
        //        );
        //    }
        //}

        private async Task LoadAvailableRooms()
        {
            try
            {
                AvailableRooms.Clear();
                RoomTypes.Clear();

                var rooms = await _apiService.GetAvailableRooms();

                if (rooms == null)
                    return;

                foreach (var room in rooms)
                {
                    AvailableRooms.Add(room);

                    // Add distinct room types
                    if (!RoomTypes.Any(rt => rt.RoomTypeID == room.RoomType.RoomTypeID))
                        RoomTypes.Add(room.RoomType);
                }

                // 🔍 Debug output
                foreach (var room in AvailableRooms)
                {
                    Console.WriteLine($"RoomNum: {room.RoomNum}, RoomID: {room.RoomID}, " +
                                      $"TypeID: {room.RoomType.RoomTypeID}, " +
                                      $"TypeName: {room.RoomType.RoomTypeName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Load rooms error: {ex.Message}");
            }
        }





        private async Task BookRoomAsync()
        {
            if (IsBusy)
                return;


            try
            {
                IsBusy = true;



                if (SelectedRoom == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Missing Room",
                        "Please select a room.",
                        "OK");

                    return;
                }





                if (string.IsNullOrWhiteSpace(Guests))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Missing Guests",
                        "Please enter the number of guests.",
                        "OK");

                    return;
                }





                if (!int.TryParse(Guests, out int guestCount))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Invalid Guests",
                        "Guest count must be a number.",
                        "OK");

                    return;
                }





                if (guestCount <= 0)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Invalid Guests",
                        "Guest count must be greater than zero.",
                        "OK");

                    return;
                }





                if (CheckOutDate <= CheckInDate)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Invalid Dates",
                        "Check-out date must be after check-in date.",
                        "OK");

                    return;
                }





                if (App.CurrentUser == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Login Required",
                        "Please login again.",
                        "OK");

                    return;
                }





                // Create booking
                var booking = new Booking
                {
                    CustomerID = App.CurrentUser.CustomerID,

                    RoomID = SelectedRoom.RoomID,

                    CheckIn = CheckInDate,

                    CheckOut = CheckOutDate,

                    NumberOfGuests = guestCount
                };





                // POST booking
                var bookingCreated = await _apiService.CreateBooking(booking);




                if (!bookingCreated)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Booking Failed",
                        "Unable to create booking.",
                        "OK");

                    return;
                }





                // Update room status after booking succeeds
                var roomUpdated = await _apiService.UpdateRoomStatus(
                    "Booked",
                    SelectedRoom.RoomID
                );





                if (!roomUpdated)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Warning",
                        "Booking created but room status was not updated.",
                        "OK");
                }





                await Application.Current.MainPage.DisplayAlert(
                    "Booking Confirmed",
                    $"Room {SelectedRoom.RoomNum} ({SelectedRoom.RoomType.RoomTypeName}) booked.\n\n" +
                    $"Guests: {guestCount}\n" +
                    $"Check-in: {CheckInDate:MMMM dd, yyyy}\n" +
                    $"Check-out: {CheckOutDate:MMMM dd, yyyy}",
                    "OK");





                // Refresh room list so booked room disappears
                await LoadAvailableRooms();


                // Clear selection
                SelectedRoom = null;
                Guests = string.Empty;

            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Booking error: {ex.Message}"
                );


                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "An unexpected error occurred while booking.",
                    "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}