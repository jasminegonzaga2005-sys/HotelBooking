using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using HotelBookingApp.Models;


namespace HotelBookingApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }

        //customer endpoints
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Customer>>("api/Customer");
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Customer", customer);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Customer>();
        }

        //added this 
        public async Task<Customer?> LoginAsync(string email, string password)
        {
            Console.WriteLine("Fetching Customer info");
            try
            {
                var url = $"api/Customer?email={Uri.EscapeDataString(email)}&password={Uri.EscapeDataString(password)}";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var customer = await response.Content.ReadFromJsonAsync<Customer>();

                Console.WriteLine($"Custmer{customer}");
                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return null;
            }
        }

        //rooms endpoints
        public async Task<List<Room>?> GetAvailableRooms()
        {
            Console.WriteLine("Fetching Available Rooms");

            try
            {
                var url = "api/Rooms?status=Available";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Request failed: {response.StatusCode}");
                    return null;
                }

                var rooms = await response.Content.ReadFromJsonAsync<List<Room>>();

                Console.WriteLine($"Retrieved {rooms?.Count ?? 0} available rooms.");

                return rooms;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching rooms: {ex.Message}");
                return new List<Room>();
            }
        }

        
        public async Task<bool> UpdateRoomStatus(string status, int id)
        {
            Console.WriteLine($"Updating room {id} status to {status}");

            try
            {
                var requestBody = new
                {
                    roomStatus = status
                };


                var response = await _httpClient.PatchAsJsonAsync(
                    $"api/Rooms/{id}",
                    requestBody
                );


                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Room status updated successfully.");
                    return true;
                }


                var error = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Update room failed: {error}");

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update room status error: {ex.Message}");

                return false;
            }
        }

        public async Task<List<Room>> GetRoomsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Room>>("api/Rooms");
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Room>($"api/Rooms/{id}");
        }

        public async Task UpdateRoomAsync(int id, Room updatedRoom)
        {
            var response = await _httpClient.PatchAsJsonAsync($"api/Rooms/{id}", updatedRoom);
            response.EnsureSuccessStatusCode();
        }

        //booking endpoints
        public async Task<bool> CreateBooking(Booking booking)
        {
            Console.WriteLine("Creating booking...");

            try
            {
                var requestBody = new
                {
                    customerID = booking.CustomerID,
                    roomID = booking.RoomID,
                    checkIn = booking.CheckIn.ToString("yyyy-MM-dd"),
                    checkOut = booking.CheckOut.ToString("yyyy-MM-dd"),
                    numberOfGuests = booking.NumberOfGuests
                };


                var response = await _httpClient.PostAsJsonAsync(
                    "api/Booking",
                    requestBody
                );


                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Booking created successfully.");
                    return true;
                }


                var error = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Booking failed: {error}");

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create booking error: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Booking>> GetBookings(int id)
        {
            try
            {
                Console.WriteLine($"Fetching bookings for customer ID: {id}");

                var url = $"api/Booking/customer/{id}";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(
                        $"Failed to get bookings. Status: {response.StatusCode}"
                    );

                    return new List<Booking>();
                }


                var bookings = await response.Content.ReadFromJsonAsync<List<Booking>>();

                if (bookings == null)
                {
                    return new List<Booking>();
                }


                Console.WriteLine(
                    $"Loaded {bookings.Count} bookings"
                );

                return bookings;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Get bookings error: {ex.Message}"
                );

                return new List<Booking>();
            }
        }

    }
}
