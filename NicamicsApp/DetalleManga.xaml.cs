using BackendlessAPI.Service;
using NicamicsApp.Models.UserRequest;
using NicamicsApp.Service;
using NicamicsApp.ViewModels;

namespace NicamicsApp;

public partial class DetalleManga : ContentPage
{
    private int cantidad = 1;
    IServiceProvider _serviceProvider;
    ComicService _comicService;

    UserServices _userServices;

    private string _comicId;


    public DetalleManga(IServiceProvider serviceProvider, ComicService comicService, UserServices userServices,string comicId)
    {
        InitializeComponent();
        UpdateCantidad();
        _serviceProvider = serviceProvider;
        _comicService = comicService;
        _userServices = userServices;
        _comicId = comicId;
        var viewModel = new DetalleMangaViewModel(comicService, userServices, comicId);
        BindingContext = viewModel;
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
    private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
    {
        double newValue = e.NewValue;
        LabelCantidad.Text = $"{newValue}";
    }
    // Evento para incrementar la cantidad
    private void OnIncrementClicked(object sender, EventArgs e)
    {
        cantidad++;
        UpdateCantidad();
    }

    // Evento para decrementar la cantidad
    private void OnDecrementClicked(object sender, EventArgs e)
    {
        if (cantidad > 1)
        {
            cantidad--;
        }
        UpdateCantidad();
    }

    // Actualiza la cantidad mostrada
    private void UpdateCantidad()
    {
        LabelCantidad.Text = cantidad.ToString();
    }

    private async void carriticliked(object sender, EventArgs e)
    {
        var _carritoPage = _serviceProvider.GetService<CarritoPage>();

        await Navigation.PushAsync(_carritoPage);
    }
}