using HotelBookingApp.ViewModels;

namespace HotelBookingApp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnCreateAccountClicked(object sender, EventArgs e)
    {
       await Navigation.PushAsync(new RegisterPage());
    }
}
