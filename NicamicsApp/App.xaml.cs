﻿using BackendlessAPI;
using NicamicsApp.Reportes;

namespace NicamicsApp
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            var loginPage = serviceProvider.GetService<LoginPage>();
            var comicPage = serviceProvider.GetService<AddComicPage>();
            var detalle = serviceProvider.GetService<Detallecompra>();

            MainPage = new NavigationPage(detalle);
        }
    }
}
