using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using HotelBookingApp.Views;
using HotelBookingApp.Views.Rooms;

namespace HotelBookingApp.ViewModels;

public class DashboardViewModel : BaseViewModel
{
    public ICommand GoToStandardRoomCommand { get; }
    public ICommand GoToSuperiorRoomCommand { get; }
    public ICommand GoToDeluxeRoomCommand { get; }
    public ICommand GoToFamilyRoomCommand { get; }
    public ICommand GoToExecutiveSuiteCommand { get; }
    public ICommand GoToBookRoomCommand { get; }
    public ICommand GoToMyBookingsCommand { get; }

    public DashboardViewModel()
    {
        GoToStandardRoomCommand = new Command(async () => await GoToStandardRoomAsync());
        GoToSuperiorRoomCommand = new Command(async () => await GoToSuperiorRoomAsync());
        GoToDeluxeRoomCommand = new Command(async () => await GoToDeluxeRoomAsync());
        GoToFamilyRoomCommand = new Command(async () => await GoToFamilyRoomAsync());
        GoToExecutiveSuiteCommand = new Command(async () => await GoToExecutiveSuiteAsync());
        GoToBookRoomCommand = new Command(async () => await GoToBookRoomAsync());
        GoToMyBookingsCommand = new Command(async () => await GoToMyBookingsAsync());
    }

    private async Task GoToStandardRoomAsync()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new StandardRoomPage());
    }

    private async Task GoToSuperiorRoomAsync()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new SuperiorRoomPage());
    }

    private async Task GoToDeluxeRoomAsync()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new DeluxeRoomPage());
    }

    private async Task GoToFamilyRoomAsync()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new FamilyRoomPage());
    }

    private async Task GoToExecutiveSuiteAsync()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new ExecutiveSuitePage());
    }

    private async Task GoToBookRoomAsync()
    {
        var page = Application.Current?.Windows[0].Page?
            .Handler?.MauiContext?.Services.GetService<BookRoomPage>();

        if (page != null)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }

    private async Task GoToMyBookingsAsync()
    {
        var page = Application.Current?.Windows[0].Page?
            .Handler?.MauiContext?.Services.GetService<MyBookingsPage>();

        if (page != null)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}