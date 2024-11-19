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
using System.Text.RegularExpressions;
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

        public ObservableCollection<string> Departamentos { get; set; }

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

            Departamentos = new ObservableCollection<string>
            {
                "Boaco", "Carazo", "Chinandega", "Chontales", "Esteli",
            "Granada", "Jinotega", "Leon", "Madriz", "Managua",
            "Masaya", "Matagalpa", "Nueva Segovia", "Rivas", "Rio San Juan",
            "RAAN", "RAAS"
            };
        }

        [ObservableProperty]
        private string nombreCompleto = "";

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
        private string tipoSeleccionado = "";

        [ObservableProperty]
        private string _departamento = "";

        [RelayCommand]
        public async Task RegistrarUsuario()
        {
            try
            {

                if (string.IsNullOrEmpty(NombreCompleto))
                {
                    Mensaje = "El campo nombre no puede estar vacío";
                    return;
                }

                if (string.IsNullOrEmpty(Username))
                {
                    Mensaje = "El campo nombre de usuario no puede estar vacío";
                    return;
                }

                if (string.IsNullOrEmpty(Correo))
                {
                    Mensaje = "El campo correo electrónico no puede estar vacío";
                    return;
                }

                if (!ValidarCorreo(Correo))
                {
                    Mensaje = "El correo electrónico ingresado no es válido";
                    return;
                }

                if (string.IsNullOrEmpty(Contraseña))
                {
                    Mensaje = "El campo contraseña no puede estar vacío";
                    return;
                }

                if (Contraseña.Length < 8)
                {
                    Mensaje = "La contraseña debe contener como mínimo 8 caracteres";
                    return;
                }

                if (Contraseña != RepetirContraseña)
                {
                    Mensaje = "Las contraseñas no coinciden";
                    return;
                }

                if (string.IsNullOrEmpty(TipoSeleccionado))
                {
                    Mensaje = "Debe seleccionar un tipo de usuario";
                    return;
                }

                if (string.IsNullOrEmpty(Departamento))
                {
                    Mensaje = "Debe seleccionar un departamento";
                    return;
                }

                var request = new RegisterRequest(NombreCompleto,Username, Contraseña, Correo, TipoSeleccionado,Departamento);

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

        public bool ValidarCorreo(string correo)
        {
            string patronCorreo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(correo, patronCorreo))
            {
                return false;
            }

            return true;
        }

    }
}
