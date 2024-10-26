
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
        public LoginPage(IServiceProvider serviceProvider, AuthService authService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _authService = authService;

        }

        async void IniciarSesionClicked(object sender, EventArgs e)
        {
            var email = UsernameEntry.Text;

            var password = PasswordEntry.Text;

            var request = new LoginRequest(email, password);

            var response = await _authService.LoginUser(request);

            if (!response.Contains("Error")) 
            {
                await DisplayAlert("�xtio", "Has iniciado sesi�n con �xito", "OK");
                await Navigation.PushAsync(new MainPage());
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
