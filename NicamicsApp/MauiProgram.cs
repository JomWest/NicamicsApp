﻿using Microsoft.Extensions.Logging;
using NicamicsApp.ReportesMongo;
using NicamicsApp.Service;
using NicamicsApp.ViewModels;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace NicamicsApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<ComicService>();
            builder.Services.AddSingleton<UserServices>();
            builder.Services.AddSingleton<ReporteService>();
            builder.Services.AddSingleton<CartService>();
            builder.Services.AddSingleton<AddressService>();
            builder.Services.AddSingleton<OrderService>();
            builder.Services.AddSingleton<TarifaService>();

            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<AddComicPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<CarritoPage>();
            builder.Services.AddTransient<CompraDetalle>();
            builder.Services.AddTransient<adddireccion>();
            builder.Services.AddTransient<Reportes>();
            builder.Services.AddTransient<formaPago>();
            builder.Services.AddTransient<ReporteComicsMasVendidos>();
            builder.Services.AddTransient<PageInventario>();
            builder.Services.AddTransient<EdeitarPerfilPage>();
            builder.Services.AddTransient<ComicEditPage>();


            builder.Services.AddTransient<DetalleMangaFactory>();
            builder.Services.AddTransient<MainPageFactory>();
            builder.Services.AddTransient<ComicEditFactory>();
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

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<PerfilUsuarioViewModel>();
            builder.Services.AddSingleton<CartViewModel>();
            builder.Services.AddSingleton<AddressViewModel>();
            builder.Services.AddSingleton<InventarioViewModel>();
            builder.Services.AddSingleton<ComicEditViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
