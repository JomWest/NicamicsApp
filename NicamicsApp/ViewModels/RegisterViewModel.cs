using BackendlessAPI.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NicamicsApp.Models.AuthRequest;
using NicamicsApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BackendlessAPI.File.FilePermission;

namespace NicamicsApp.ViewModels
{
    public partial class RegisterViewModel: ObservableObject
    {

        private readonly AuthService _authService;
        private readonly IServiceProvider _serviceProvider;
        private readonly INavigation _navigation;
        public ObservableCollection<string> TiposUsuario { get; set; }

        public RegisterViewModel(IServiceProvider serviceProvider, AuthService authService, INavigation navigation)
        {
            _serviceProvider = serviceProvider;
            _authService = authService;
            _navigation = navigation;

            TiposUsuario = new ObservableCollection<string>
            {
                "Comprador",
                "Vendedor"
            };
        }

        [ObservableProperty]
        private string username = "";

        [ObservableProperty]
        private string correo = "";

        [ObservableProperty]
        private string contraseña = "";

        [ObservableProperty]
        private string repetirContraseña = "";

        [ObservableProperty]
        private string mensaje = "";

        [ObservableProperty]
        private string tipoSeleccionado;

        [RelayCommand]
        public async void RegistrarUsuario()
        {
            try
            {
                if (Contraseña != RepetirContraseña)
                {
                    Mensaje = "Las contraseñas no coinciden";
                    return;
                }

                var request = new RegisterRequest(Username, Contraseña, Correo, TipoSeleccionado);

                var response = await _authService.RegisterUser(request);


                if (response.Contains("Success"))
                {
                    Mensaje = "Cuenta creada exitosamente";
                    await _navigation.PopAsync();
                }
                else
                {
                    Mensaje = response;
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
        }
    }
}
