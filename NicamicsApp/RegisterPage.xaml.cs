using NicamicsApp.Models.AuthRequest;
using NicamicsApp.Service;
using NicamicsApp.ViewModels;
using System.ComponentModel;

namespace NicamicsApp;

public partial class RegisterPage : ContentPage
{
    IServiceProvider _serviceProvider;
    AuthService _authService;
    public RegisterPage(IServiceProvider serviceProvider, AuthService authService)
	{
		InitializeComponent();
        _serviceProvider = serviceProvider;
        _authService = authService;

        var navigation = this.Navigation;
        var viewModel = new RegisterViewModel(serviceProvider, authService, navigation);
        BindingContext = viewModel;

        // Suscribir el evento cuando cambia el BindingContext
        viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private async void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(RegisterViewModel.Mensaje))
        {
            var viewModel = (RegisterViewModel)BindingContext;
            if (!string.IsNullOrEmpty(viewModel.Mensaje))
            {
                await DisplayAlert("Mensaje", viewModel.Mensaje, "OK");
                viewModel.Mensaje = string.Empty; // Limpiar el mensaje después de mostrarlo
            }
        }
    }

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
    }

}