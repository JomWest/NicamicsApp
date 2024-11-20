using NicamicsApp.Service;
using NicamicsApp.ViewModels;
using System.ComponentModel;

namespace NicamicsApp;

public partial class ComicEditPage : ContentPage
{

    IServiceProvider _serviceProvider;
    InventarioViewModel _inventarioViewModel;
    private ComicEditViewModel _comicEditViewModel;
    private readonly string _comicId;
	public ComicEditPage(IServiceProvider serviceProvider, InventarioViewModel inventarioViewModel,
        ComicService comicService,ComicEditViewModel comicEditViewModel, string comicId)
	{
		InitializeComponent();
		_serviceProvider = serviceProvider;
		_inventarioViewModel = inventarioViewModel;
        _comicEditViewModel = comicEditViewModel;
        BindingContext = comicEditViewModel;
        _comicId = comicId;

        _comicEditViewModel.PropertyChanged += OnViewModelPropertyChanged;

    }

    private async void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PerfilUsuarioViewModel.Mensaje))
        {
            var viewModel = (ComicEditViewModel)BindingContext;
            if (!string.IsNullOrEmpty(viewModel.Mensaje))
            {
                if (viewModel.Mensaje != "El comic se actualizó con éxito")
                {
                    await DisplayAlert("Mensaje", viewModel.Mensaje, "OK");
                    viewModel.Mensaje = string.Empty;
                    return;
                }

                await DisplayAlert("Mensaje", viewModel.Mensaje, "OK");
                viewModel.Mensaje = string.Empty;
                //var _perfiluser = _serviceProvider.GetService<Perfil_Usuario>();
                //Navigation.InsertPageBefore(_perfiluser, this);
                await Navigation.PopAsync();
            }
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _comicEditViewModel.LoadComic(_comicId);
    }

    private async void backtapped(object sender, EventArgs e)
	{
        var _perfiluser = _serviceProvider.GetService<PageInventario>();
        Navigation.InsertPageBefore(_perfiluser, this);
        await Navigation.PopAsync();
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            _comicEditViewModel.ImageResult = null;
            // Verifica si el dispositivo admite la selección de fotos
            if (MediaPicker.IsCaptureSupported)
            {
                // Abre la galería para seleccionar una foto
                var photo = await MediaPicker.PickPhotoAsync();

                if (photo != null)
                {
                    // Cargar la imagen seleccionada en el control Image
                    var stream = await photo.OpenReadAsync();
                    imgComic.Source = ImageSource.FromStream(() => stream);
                    _comicEditViewModel.ImageResult = photo;
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