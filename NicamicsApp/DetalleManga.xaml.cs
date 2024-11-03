using BackendlessAPI.Service;
using NicamicsApp.Models.UserRequest;
using NicamicsApp.Service;

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
        LoadComic(comicId);
    }

    private async void LoadComic(string comicId)
    {
        try
        {
            var response = await _comicService.ObtenerComicPorId(comicId);

            lblDesc.Text = response.descripcion;
            lblName.Text = response.nombre;
            lblPrecio.Text = $"C$ {response.precio}";
            imgPortada.Source = !string.IsNullOrEmpty(response.imagenPortada) ? response.imagenPortada : null;

            CambiarImagenFavorito(IpAddress.userId,comicId);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void CambiarImagenFavorito(string userId, string comicId)
    {
        var response = await _userServices.VerificarComicEnFavoritos(userId, comicId);
        if (response)
        {
            imgBtnFav.Source = ImageSource.FromFile("favorito_rojo.png");
        }
        else
        {
            imgBtnFav.Source = ImageSource.FromFile("favorito_blanco.png");
        }
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

    private async void imgBtnFav_Clicked(object sender, EventArgs e)
    {
        try
        {
            var request = new AgregarFavoritoRequest
            {
                UserId = IpAddress.userId,
                NuevoFavorito = _comicId
            };

            var response = await _userServices.AgregarEliminarComicFavorito(request);

            CambiarImagenFavorito(IpAddress.userId, _comicId);
            Console.WriteLine(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error {ex.Message}");
        }
    }
}