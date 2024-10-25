﻿using BackendlessAPI;

namespace NicamicsApp
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            Backendless.InitApp("7605569D-BF0C-42FC-9D54-F82F114CBF74", "Y00332653-9D7E-438F-9546-48BA9E616451");

            var loginPage = serviceProvider.GetService<LoginPage>();

            MainPage = new NavigationPage(loginPage);
        }
    }
}
