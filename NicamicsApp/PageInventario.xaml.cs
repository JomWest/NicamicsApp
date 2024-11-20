using NicamicsApp.ViewModels;
using System.ComponentModel;

namespace NicamicsApp;

public partial class PageInventario : ContentPage
{
    IServiceProvider _serviceProvider;
    InventarioViewModel _viewModel;
    ComicEditFactory _comicEditFactory;
    public PageInventario(IServiceProvider serviceProvider,InventarioViewModel viewModel, ComicEditFactory comicEditFactory)
	{
		InitializeComponent();
        _serviceProvider = serviceProvider;
        _viewModel = viewModel;
        BindingContext = _viewModel;
        _comicEditFactory = comicEditFactory;

        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private async void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {


        if (e.PropertyName == nameof(_viewModel.SelectedComic))
        {
            // Verifica si hay un cómic seleccionado
            if (_viewModel.SelectedComic != null && _viewModel.SelectedComic.comicId != "")
            {
                // Navega a la página de detalle
                await Navigation.PushAsync(_comicEditFactory.Create(_viewModel.SelectedComic.comicId));
            }
        }

        if (e.PropertyName == nameof(_viewModel.Mensaje) && _viewModel.Mensaje != "")
        {
            await DisplayAlert("Mensaje", _viewModel.Mensaje, "OK");
            _viewModel.Mensaje = string.Empty;
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadComicVendedor();
    }

    private async void clickedback(object sender,EventArgs e)
    {
        var _perfiluser = _serviceProvider.GetService<Perfil_Usuario>();
        Navigation.InsertPageBefore(_perfiluser, this);
        await Navigation.PopAsync();
    }

}