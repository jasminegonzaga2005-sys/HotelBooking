using HotelBookingApp.Services;
using HotelBookingApp.ViewModels;
using HotelBookingApp.Views;
using Microsoft.Extensions.Logging;
using HotelBookingApp.Views.Rooms;

namespace HotelBookingApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddHttpClient<ApiService>(client =>
            {
                client.BaseAddress = new Uri("http://192.168.68.132:5226/");
            });

            // ViewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<DashboardViewModel>();
            builder.Services.AddTransient<BookRoomViewModel>();
            builder.Services.AddTransient<MyBookingsViewModel>();
            builder.Services.AddTransient<PaymentViewModel>();

            // Pages
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<DashboardPage>();
            builder.Services.AddTransient<StandardRoomPage>();
            builder.Services.AddTransient<SuperiorRoomPage>();
            builder.Services.AddTransient<DeluxeRoomPage>();
            builder.Services.AddTransient<FamilyRoomPage>();
            builder.Services.AddTransient<ExecutiveSuitePage>();
            builder.Services.AddTransient<BookRoomPage>();
            builder.Services.AddTransient<MyBookingsPage>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<PaymentPage>();
            builder.Services.AddTransient<BookRoomPage>();


            // App
            builder.Services.AddSingleton<App>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}