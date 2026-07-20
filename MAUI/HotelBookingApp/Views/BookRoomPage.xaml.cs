using HotelBookingApp.ViewModels;

namespace HotelBookingApp.Views;

public partial class BookRoomPage : ContentPage
{
    public BookRoomPage(BookRoomViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void MyBookings_Clicked(object sender, EventArgs e)
    {
        var myBookingsPage = Handler?.MauiContext?.Services.GetService<MyBookingsPage>();

        if (myBookingsPage != null)
        {
            await Navigation.PushAsync(myBookingsPage);
        }
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
}