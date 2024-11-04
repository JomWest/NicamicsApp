
using BackendlessAPI;
using BackendlessAPI.Exception;
using System;
using Microsoft.Maui.Controls;
using NicamicsApp.Service;
using NicamicsApp.Models.AuthRequest;
using NicamicsApp.ViewModels;
using System.ComponentModel;

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
    }
}
