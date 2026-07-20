using HotelBookingApp.Views.Rooms;

namespace HotelBookingApp.Views;

public partial class DashboardPage : ContentPage
{
    public string WelcomeMessage { get; set; }

    public DashboardPage()
    {
        InitializeComponent();

        if (App.CurrentUser != null)
        {
            WelcomeMessage = $"Welcome, {App.CurrentUser.FirstName}!";
        }
        else
        {
            WelcomeMessage = "Welcome!";
        }

        BindingContext = this;
    }

    private async void StandardRoom_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StandardRoomPage());
    }

    private async void SuperiorRoom_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SuperiorRoomPage());
    }

    private async void DeluxeRoom_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DeluxeRoomPage());
    }

    private async void FamilyRoom_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FamilyRoomPage());
    }

    private async void ExecutiveSuite_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ExecutiveSuitePage());
    }

    private async void BookRoom_Clicked(object sender, EventArgs e)
    {
        var bookRoomPage = Handler?.MauiContext?.Services.GetService<BookRoomPage>();

        if (bookRoomPage != null)
        {
            await Navigation.PushAsync(bookRoomPage);
        }
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
        await DisplayAlert("Home", "You are already on the homepage.", "OK");
    }

    //private async void Profile_Clicked(object sender, EventArgs e)
    //{
    //    var profilePage = Handler?.MauiContext?.Services.GetService<ProfilePage>();
    //
    //    if (profilePage != null)
    //    {
    //        await Navigation.PushAsync(profilePage);
    //    }
    //}
}