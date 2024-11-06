using BackendlessAPI.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.ApplicationModel.Communication;
using NicamicsApp.Models.AuthRequest;
using NicamicsApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weborb.Client;

namespace NicamicsApp.ViewModels
{
    public partial class LoginViewModel: ObservableObject
    {
        private readonly AuthService _authService;
        private readonly MainPageFactory _mainPageFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigation _navigation;

    public LoginViewModel(IServiceProvider serviceProvider,AuthService authService, MainPageFactory mainPageFactory, INavigation navigation)
    {
        _authService = authService;
        _mainPageFactory = mainPageFactory;
        _serviceProvider = serviceProvider;
        _navigation = navigation;
    }

        [ObservableProperty]
        private string correo = "";

        [ObservableProperty]
        private string contraseña = "";


        [ObservableProperty]
        private string mensaje = "";

        [RelayCommand]
        private async void IniciarSesion()
        {
            try
            {
                var request = new LoginRequest(Correo, Contraseña);

                var response = await _authService.LoginUser(request);

                if (!response.Token.Contains("Error"))
                {
                    IpAddress.userId = response.UserId;
                    IpAddress.token = response.Token;
                    var mainPage = _mainPageFactory.Create();

                    Mensaje = "Has iniciado sesión con éxito";
                    await _navigation.PushAsync(mainPage);
                }
                else
                {
                    Mensaje = "Error: No se pudo iniciar sesión";
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
            
        }

        [RelayCommand]
        private async void NavegarHaciaRegistro()
        {
            var registerPage = _serviceProvider.GetService<RegisterPage>();
            await _navigation.PushAsync(registerPage);
        }
    }
}
