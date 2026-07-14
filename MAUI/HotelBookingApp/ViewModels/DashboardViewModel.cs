using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using HotelBookingApp.Services;
using HotelBookingApp.Models;
using HotelBookingApp.Views;
using HotelBookingApp.Views.Rooms;

namespace HotelBookingApp.ViewModels;

public class DashboardViewModel : BaseViewModel
{
    private readonly BookRoomPage _bookroompage;
    private readonly MyBookingsPage _mybookingspage;
    public ICommand GoToStandardRoomCommand { get; }
    public ICommand GoToSuperiorRoomCommand {  get; }
    public ICommand GoToDeluxeRoomCommand {  get; }
    public ICommand GoToFamilyRoomCommand { get; }
    public ICommand GoToExecutiveSuiteCommand { get; }
    public ICommand GoToBookRoomCommand {  get; }
    public ICommand GoToMyBookingsCommand { get; }
    public ICommand GoToMyProfileCommand {  get; }



    public DashboardViewModel(BookRoomPage bookroompage, MyBookingsPage myBookingsPage)
    {
        GoToStandardRoomCommand = new Command(async () =>
            await GoToStandardRoomAsync());

        GoToSuperiorRoomCommand = new Command(async () =>
            await GoToSuperiorRoomAsync());

        GoToDeluxeRoomCommand = new Command(async () =>
            await GoToDeluxeRoomAsync());

        GoToFamilyRoomCommand = new Command(async () =>
            await GoToFamilyRoomAsync());

        GoToExecutiveSuiteCommand = new Command(async () =>
            await GoToExecutiveSuiteAsync());

        GoToBookRoomCommand = new Command(async () =>
            await GoToBookRoomAsync());

        GoToMyBookingsCommand = new Command(async () =>
            await GoToMyBookingsAsync());

        GoToMyProfileCommand = new Command(async () =>
            await GoToMyProfileAsync());

        _bookroompage = bookroompage;
        _mybookingspage = myBookingsPage;
    }


    private async Task GoToStandardRoomAsync()
    {
        await Application.Current.MainPage.Navigation
            .PushAsync(new StandardRoomPage());
    }

    private async Task GoToSuperiorRoomAsync()
    {
        await Application.Current.MainPage.Navigation
            .PushAsync(new SuperiorRoomPage());
    }

    private async Task GoToDeluxeRoomAsync()
    {
        await Application.Current.MainPage.Navigation
            .PushAsync(new DeluxeRoomPage());
    }
    private async Task GoToFamilyRoomAsync()
    {
        await Application.Current.MainPage.Navigation
            .PushAsync(new DeluxeRoomPage());
    }
    private async Task GoToExecutiveSuiteAsync()
    {
        await Application.Current.MainPage.Navigation
            .PushAsync(new ExecutiveSuitePage());
    }

    private async Task GoToBookRoomAsync()
    {
        await Application.Current.MainPage.Navigation
            .PushAsync(_bookroompage);
    }
    private async Task GoToMyBookingsAsync()
    {
        await Application.Current.MainPage.Navigation
            .PushAsync(_mybookingspage);
    }
    private async Task GoToMyProfileAsync()
    {
        await Application.Current.MainPage.Navigation
            .PushAsync(new ProfilePage());
    }
}