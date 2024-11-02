using Microsoft.Extensions.Logging;
using NicamicsApp.Service;

namespace NicamicsApp
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

            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<ComicService>();

            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<AddComicPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<CarritoPage>();
            builder.Services.AddTransient<DetalleMangaFactory>();

            /* Cambio no fusionado mediante combinación del proyecto 'NicamicsApp (net8.0-android)'
            Antes:
                        builder.Services.AddTransient<DetalleManga.DetalleManga>();
                        builder.Services.AddTransient<Perfil_Usuario>();
            Después:
                        builder.Services.AddTransient<NicamicsApp.DetalleManga>();
                        builder.Services.AddTransient<Perfil_Usuario>();
            */
            builder.Services.AddTransient<DetalleManga>();
            builder.Services.AddTransient<Perfil_Usuario>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
