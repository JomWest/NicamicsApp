
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
                if (viewModel.Mensaje != "Perfil actualizado exitosamente.")
                {
                    await DisplayAlert("Mensaje", viewModel.Mensaje, "OK");
                    viewModel.Mensaje = string.Empty;
                    return;
                }

                await DisplayAlert("Mensaje", viewModel.Mensaje, "OK");
                viewModel.Mensaje = string.Empty;
                var _perfiluser = _serviceProvider.GetService<Perfil_Usuario>();
                Navigation.InsertPageBefore(_perfiluser, this);
                await Navigation.PopAsync();
            }
        }
    }

    private async void imgArrowBack_Clicked(object sender, EventArgs e)
    {
        var _perfiluser = _serviceProvider.GetService<Perfil_Usuario>();
        Navigation.InsertPageBefore(_perfiluser, this);
        await Navigation.PopAsync();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            _perfilUsuarioViewModel.FileResult = null;
            // Verifica si el dispositivo admite la selección de fotos
            if (MediaPicker.IsCaptureSupported)
            {
                // Abre la galería para seleccionar una foto
                var photo = await MediaPicker.PickPhotoAsync();

                if (photo != null)
                {
                    // Cargar la imagen seleccionada en el control Image
                    var stream = await photo.OpenReadAsync();
                    imgPerfil.Source = ImageSource.FromStream(() => stream);
                    _perfilUsuarioViewModel.FileResult = photo;
                }
            }
            else
            {
                await DisplayAlert("Error", "La galería no está disponible en este dispositivo.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar la imagen: {ex.Message}", "OK");
        }
    }
}