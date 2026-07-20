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
                //client.BaseAddress = new Uri("http://172.26.134.45:5226/");
                client.BaseAddress = new Uri("https://localhost:7068/");
            });

            // ViewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<DashboardViewModel>();
            builder.Services.AddTransient<BookRoomViewModel>();
            builder.Services.AddTransient<MyBookingsViewModel>();
            builder.Services.AddTransient<PaymentViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<RatingViewModel>();

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
            
            builder.Services.AddTransient<PaymentPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<RatingPage>();
            builder.Services.AddTransient<ReviewsPage>();
            builder.Services.AddTransient<Splashpage>();
            builder.Services.AddTransient<StatusPage>();
            builder.Services.AddTransient<AllocationWindow>();
            builder.Services.AddTransient<ProfilePage>();



            // App
            builder.Services.AddSingleton<App>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}