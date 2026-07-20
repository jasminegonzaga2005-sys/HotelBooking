namespace HotelBookingApp.Views;

public partial class AllocationWindow : ContentPage
{
    public AllocationWindow()
    {
        InitializeComponent();
    }

    private async void NavStatus_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StatusPage());
    }

    private async void NavReviews_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReviewsPage());
    }
}