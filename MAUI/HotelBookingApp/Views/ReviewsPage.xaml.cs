namespace HotelBookingApp.Views;

public partial class ReviewsPage : ContentPage
{
    public ReviewsPage()
    {
        InitializeComponent();
    }

    private async void NavAllocation_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AllocationWindow());
    }

    private async void NavStatus_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StatusPage());
    }
}