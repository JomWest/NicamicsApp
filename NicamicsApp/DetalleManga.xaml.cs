using BackendlessAPI.Service;
using NicamicsApp.Models;
using NicamicsApp.Models.UserRequest;
using NicamicsApp.Service;
using NicamicsApp.ViewModels;
using System.ComponentModel;

namespace NicamicsApp;

public partial class DetalleManga : ContentPage
{
    private int cantidad = 1;
    private readonly IServiceProvider _serviceProvider;
    private readonly ComicService _comicService;
    private readonly CartService _cartService;
    private readonly UserServices _userServices;
    private readonly string _comicId;

    private readonly DetalleMangaViewModel _viewModel;

    // Constructor
    public DetalleManga(IServiceProvider serviceProvider, ComicService comicService, UserServices userServices, CartService cartService, string comicId)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _comicService = comicService;
        _cartService = cartService;
        _userServices = userServices;
        _comicId = comicId;

        // Vinculamos el ViewModel a la vista
        var viewModel = new DetalleMangaViewModel(comicService, userServices, cartService, comicId);
        BindingContext = viewModel;
        _viewModel = viewModel;
        UpdateCantidad();

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;

    }

    private async void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(DetalleMangaViewModel.Mensaje))
        {
            var viewModel = (DetalleMangaViewModel)BindingContext;
            if (!string.IsNullOrEmpty(viewModel.Mensaje))
            {
                await DisplayAlert("Mensaje", viewModel.Mensaje, "OK");
                viewModel.Mensaje = string.Empty;
                var _compradetalle = _serviceProvider.GetService<CarritoPage>();
                Navigation.InsertPageBefore(_compradetalle, this);
                await Navigation.PopAsync();

            }
        }
    }

    // Método para ir a la página principal
    private async void OnImageTapped(object sender, EventArgs e)
    {
        var _mainPage = _serviceProvider.GetService<MainPage>();
        await Navigation.PushAsync(_mainPage);
    }

    // Método para mostrar el menú
    private async void OnImageTappedMenu(object sender, EventArgs e)
    {
        MainContent.IsVisible = false;
        Overlay.IsVisible = true;
        BottomMenu.IsVisible = true;
        await BottomMenu.TranslateTo(0, 0, 250, Easing.SinInOut);
    }

    // Cerrar el menú
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

    // Método para actualizar la cantidad
    private void UpdateCantidad()
    {
        LabelCantidad.Text = cantidad.ToString();
        _viewModel.Cantidad = cantidad;
    }

    // Método que se ejecuta cuando el valor del stepper cambia
    private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
    {
        double newValue = e.NewValue;
        LabelCantidad.Text = $"{newValue}";
    }

    // Método para incrementar la cantidad
    private void OnIncrementClicked(object sender, EventArgs e)
    {
        cantidad++;
        UpdateCantidad();
    }

    // Método para decrementar la cantidad
    private void OnDecrementClicked(object sender, EventArgs e)
    {
        if (cantidad > 1)
        {
            cantidad--;
        }
        UpdateCantidad();
    }

  
    private async void carriticliked(object sender, EventArgs e)
    {
        
    }

}
