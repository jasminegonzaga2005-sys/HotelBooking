using HotelBookingApp.ViewModels;

namespace HotelBookingApp.Views;

public partial class BookRoomPage : ContentPage
{
    private readonly MyBookingsPage _myBookingsPage;
    public BookRoomPage(BookRoomViewModel viewModel, MyBookingsPage myBookingsPage)
    {
        InitializeComponent();

        BindingContext = viewModel;
        _myBookingsPage = myBookingsPage;
    }

    private async void MyBookings_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_myBookingsPage);
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