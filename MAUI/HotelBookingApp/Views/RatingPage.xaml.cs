namespace HotelBookingApp.Views;

public partial class RatingPage : ContentPage
{
    private string _hotelName;
    private string _bookingId;

    // Parameterless constructor (required for XAML tooling & Shell navigation)
    public RatingPage()
    {
        InitializeComponent();
    }

    // Overloaded constructor to receive data passed from PaymentPage
    public RatingPage(string hotelName, string bookingId)
    {
        InitializeComponent();

        _hotelName = hotelName;
        _bookingId = bookingId;

        // Display hotel name on UI if passed, or default to original text
        if (!string.IsNullOrWhiteSpace(_hotelName))
        {
            HotelNameLabel.Text = _hotelName;
        }
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        string ratingText = RatingEntry.Text?.Trim();
        string feedback = FeedbackEditor.Text?.Trim();

        // 1. Check for empty rating
        if (string.IsNullOrEmpty(ratingText))
        {
            await DisplayAlert("Validation Error", "Please enter a rating before submitting.", "OK");
            return;
        }

        // 2. Validate rating range (Must be a number between 1 and 5)
        if (!int.TryParse(ratingText, out int rating) || rating < 1 || rating > 5)
        {
            await DisplayAlert("Validation Error", "Please enter a valid rating from 1 to 5.", "OK");
            return;
        }

        // TODO: Save rating & feedback to your database using _bookingId

        await DisplayAlert("Thank You!", "Your feedback has been submitted successfully.", "OK");

        // Navigate back to the home/main screen
        await Navigation.PopToRootAsync();
    }

    private async void OnViewReviewsTapped(object sender, TappedEventArgs e)
    {
        // Navigate to ReviewsPage
        await Navigation.PushAsync(new ReviewsPage());
    }
}