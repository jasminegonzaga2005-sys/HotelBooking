using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace HotelBookingApp.Converters
{
    public class BookingStatusColorConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value == null)
                return Colors.Gray;

            string status = value.ToString();

            return status switch
            {
                "Pending" => Colors.Gold,
                "Cancelled" => Colors.Red,
                "Confirmed" => Colors.Green,
                _ => Colors.Gray
            };
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}