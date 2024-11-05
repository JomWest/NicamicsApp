using NicamicsApp.Service;
using NicamicsApp.ViewModels;
using System.ComponentModel;

namespace NicamicsApp;

public partial class Perfil_Usuario : ContentPage
{
    IServiceProvider _serviceProvider;
    UserServices _userServices;

    PerfilUsuarioViewModel _perfilUsuarioViewModel;

    DetalleMangaFactory _detalleManga;
    public Perfil_Usuario(IServiceProvider serviceProvider, UserServices userService, DetalleMangaFactory detalleMangaFactory, PerfilUsuarioViewModel perfilUsuarioViewModel)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _userServices = userService;
        _detalleManga = detalleMangaFactory;

        BindingContext = perfilUsuarioViewModel;
        _perfilUsuarioViewModel = perfilUsuarioViewModel;
        _perfilUsuarioViewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private async void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_perfilUsuarioViewModel.SelectedComic))
        {
            // Verifica si hay un cómic seleccionado
            if (_perfilUsuarioViewModel.SelectedComic != null && _perfilUsuarioViewModel.SelectedComic != "")
            {
                // Navega a la página de detalle
                NavegarAlDetalle(_perfilUsuarioViewModel.SelectedComic);
                // Limpia el cómic seleccionado después de la navegación
                _perfilUsuarioViewModel.SelectedComic = string.Empty;
            }
        }
    }

    private async void NavegarAlDetalle(string comicId)
    {
        var detalleComic = _detalleManga.Create(comicId);
        await Navigation.PushAsync(detalleComic);
    }

    private async void OnImageTapped(object sender, EventArgs e)
    {
        var _mainPage = _serviceProvider.GetService<MainPage>();
        await Navigation.PushAsync(_mainPage);
    }

    private async void OnImageTappedMenu(object sender, EventArgs e)
    {
        MainContent.IsVisible = false;

        Overlay.IsVisible = true;
        BottomMenu.IsVisible = true;

        await BottomMenu.TranslateTo(0, 0, 250, Easing.SinInOut);
    }
    private async void OnTappedSalidaMenu(object sender, EventArgs e)
    {
        CloseMenu();
    }


    private async void CloseMenu()
    {
        await BottomMenu.TranslateTo(0, 700, 250, Easing.SinInOut);

        BottomMenu.IsVisible = false;
        Overlay.IsVisible = false;
        MainContent.IsVisible = true;
    }

}
