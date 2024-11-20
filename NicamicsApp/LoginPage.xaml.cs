
using BackendlessAPI;
using BackendlessAPI.Exception;
using System;
using Microsoft.Maui.Controls;
using NicamicsApp.Service;
using NicamicsApp.Models.AuthRequest;
using NicamicsApp.ViewModels;
using System.ComponentModel;
using BackendlessAPI.Service;

namespace NicamicsApp
{
    public partial class LoginPage : ContentPage
    {
        IServiceProvider _serviceProvider;
        AuthService _authService;
        MainPageFactory _mainPageFactory;
        UserServices _userServices;
       

        public LoginPage(IServiceProvider serviceProvider, AuthService authService, MainPageFactory mainPageFactory, UserServices userServices)
        {
            InitializeComponent();
            _userServices = userServices;
            _authService = authService;
            var navigation = this.Navigation;
            var viewModel = new LoginViewModel(serviceProvider, authService, mainPageFactory, navigation);
            BindingContext = viewModel;

            // Suscribir el evento cuando cambia el BindingContext
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private async void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.Mensaje))
            {
                var viewModel = (LoginViewModel)BindingContext;
                if (!string.IsNullOrEmpty(viewModel.Mensaje))
                {
                    await DisplayAlert("Mensaje", viewModel.Mensaje, "OK");
                    viewModel.Mensaje = string.Empty; // Limpiar el mensaje después de mostrarlo
                }
            }
        }

        private async void btnRestablecer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RestablecerContraseña(_authService));
        }

        private void imgContra_Clicked(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            if (PasswordEntry.IsPassword)
            {
                imgContra.Source = "eyeslash.png";
            }
            else
            {
                imgContra.Source = "eyeopen.png";
            }
        }
    }
}
