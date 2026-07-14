using System;
using HotelBookingApp.Services;
using Microsoft.Extensions.Logging;
using HotelBookingApp.ViewModels;
//using Microsoft.Extensions.Http;



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

#if DEBUG
            builder.Services.AddHttpClient<ApiService>(client =>
            {
                //client.BaseAddress = new Uri("http://10.0.2.2:5226/");
                client.BaseAddress = new Uri("https://localhost:7068/");
            });

            builder.Services.AddTransient<LoginViewModel>();
            builder.Logging.AddDebug();

            
#endif

            return builder.Build();
        }
    }
}
