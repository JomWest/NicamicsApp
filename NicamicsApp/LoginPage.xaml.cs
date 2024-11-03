
using BackendlessAPI;
using BackendlessAPI.Exception;
using System;
using Microsoft.Maui.Controls;
using NicamicsApp.Service;
using NicamicsApp.Models.AuthRequest;

namespace NicamicsApp
{
    public partial class LoginPage : ContentPage
    {
        IServiceProvider _serviceProvider;
        AuthService _authService;
        MainPageFactory _mainPageFactory;
       

        public LoginPage(IServiceProvider serviceProvider, AuthService authService, MainPageFactory mainPageFactory)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _authService = authService;
            _mainPageFactory = mainPageFactory;
        }

        async void IniciarSesionClicked(object sender, EventArgs e)
        {
            var email = UsernameEntry.Text;

            var password = PasswordEntry.Text;

            var request = new LoginRequest(email, password);

            var response = await _authService.LoginUser(request);

            if (!response.Token.Contains("Error")) 
            {
                IpAddress.userId = response.UserId;
                var mainPage = _mainPageFactory.Create();

                await DisplayAlert("Éxtio", "Has iniciado sesión con éxito", "OK");
                await Navigation.PushAsync(mainPage);
            }
            else
            {
                await DisplayAlert("Error", $"{response}", "OK");
            }
        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            var registerPage = _serviceProvider.GetService<RegisterPage>();
            await Navigation.PushAsync(registerPage);
        }
    }
}
