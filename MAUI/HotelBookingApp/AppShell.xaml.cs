using HotelBookingApp.Views;

namespace HotelBookingApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));

        Loaded += async (sender, e) =>
        {
            await GoToAsync(nameof(LoginPage));
        };
    }
}