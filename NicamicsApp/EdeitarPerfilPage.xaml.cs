
using NicamicsApp.Service;
using NicamicsApp.ViewModels;
using System.ComponentModel;

namespace NicamicsApp;

public partial class EdeitarPerfilPage : ContentPage
{

    IServiceProvider _serviceProvider;
    UserServices _userServices;

    PerfilUsuarioViewModel _perfilUsuarioViewModel;

    public EdeitarPerfilPage(IServiceProvider serviceProvider,UserServices userServices, PerfilUsuarioViewModel perfilUsuarioViewModel)
	{
		InitializeComponent();
        _serviceProvider = serviceProvider;
            _userServices = userServices;
        _perfilUsuarioViewModel = perfilUsuarioViewModel;
        BindingContext = perfilUsuarioViewModel;
        _perfilUsuarioViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _perfilUsuarioViewModel.InitializeData();
    }

    private async void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PerfilUsuarioViewModel.Mensaje))
        {
            var viewModel = (PerfilUsuarioViewModel)BindingContext;
            if (!string.IsNullOrEmpty(viewModel.Mensaje))
            {
                await DisplayAlert("Mensaje", viewModel.Mensaje, "OK");
                viewModel.Mensaje = string.Empty;
                var _perfiluser = _serviceProvider.GetService<Perfil_Usuario>();
                Navigation.InsertPageBefore(_perfiluser, this);
                await Navigation.PopAsync();
            }
        }
    }

    private async void backtapped(object sender, EventArgs e)
    {
        var _perfiluser = _serviceProvider.GetService<Perfil_Usuario>();
        Navigation.InsertPageBefore(_perfiluser, this);
        await Navigation.PopAsync();
    }
}