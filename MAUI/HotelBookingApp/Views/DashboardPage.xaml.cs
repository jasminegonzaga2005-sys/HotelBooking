using HotelBookingApp.Views.Rooms;

namespace HotelBookingApp.Views;

public partial class DashboardPage : ContentPage
{
    private readonly BookRoomPage _bookroompage;
    private readonly MyBookingsPage _myBookingsPage;
    public string WelcomeMessage { get; set; }
    public DashboardPage(BookRoomPage bookRoomPage, MyBookingsPage myBookingsPage)
    {
        _bookroompage = bookRoomPage;
        _myBookingsPage = myBookingsPage;
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
        await Navigation.PushAsync(_bookroompage);
    }

    private async void MyBookings_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_myBookingsPage);
    }

    private async void Home_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Home", "You are already on the homepage.", "OK");
    }

    private async void Profile_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(_myBookingsPage);
    }
}