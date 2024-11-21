using NicamicsApp.PedidosOrdenes;
using NicamicsApp.ReportesMongo;
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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _perfilUsuarioViewModel.InitializeData();
    }

    private async void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_perfilUsuarioViewModel.SelectedComic))
        {
            // Verifica si hay un c�mic seleccionado
            if (_perfilUsuarioViewModel.SelectedComic != null && _perfilUsuarioViewModel.SelectedComic != "")
            {
                // Navega a la p�gina de detalle
                await Navigation.PushAsync(_detalleManga.Create(_perfilUsuarioViewModel.SelectedComic));
            }
        }
    }

    private async void Reportestapped(object sender, EventArgs e)
    {
        var _reportes = _serviceProvider.GetService<ReporteComicsMasVendidos>();
        await Navigation.PushAsync(_reportes);
    }

    private async void AgregarComicTapped(object sender, EventArgs e)
    {
        var _addcomic = _serviceProvider.GetService<AddComicPage>();
        Navigation.InsertPageBefore(_addcomic, this);
        await Navigation.PopAsync();
    }

    private async void InventarioTapped(object sender, EventArgs e)
    {
        var _inventario = _serviceProvider.GetService<PageInventario>();
        Navigation.InsertPageBefore(_inventario, this);
        await Navigation.PopAsync();
    }

    private async void EditPerfil(object sender, EventArgs e)
    {
        var _editarperfil = _serviceProvider.GetService<EdeitarPerfilPage>();
        Navigation.InsertPageBefore(_editarperfil, this);
        await Navigation.PopAsync();
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

    private async void imgArrowBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();    
    }

    private async void btOrdenes_Clicked(object sender, EventArgs e)
    {
        var _historial = _serviceProvider.GetService<HistorialOrdenes>();
        await Navigation.PushAsync(_historial);
    }

    private async void btnPedidos_Clicked(object sender, EventArgs e)
    {
        var _historial = _serviceProvider.GetService<HistorialPedidos>();
        await Navigation.PushAsync(_historial);
    }
}
