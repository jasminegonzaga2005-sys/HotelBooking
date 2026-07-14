using HotelBookingApp.Services;
using HotelBookingApp.ViewModels;
using HotelBookingApp.Views;
using Microsoft.Extensions.Logging;

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

            // Pages
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<DashboardPage>();

            // App
            builder.Services.AddSingleton<App>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}