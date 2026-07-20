namespace HotelBookingApp.Views;

public partial class StatusPage : ContentPage
{
    public StatusPage()
    {
        InitializeComponent();
    }

    private async void NavAllocation_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AllocationWindow());
    }

    private async void NavReviews_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReviewsPage());
    }
}