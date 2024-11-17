using NicamicsApp.ViewModels;

namespace NicamicsApp;

public partial class CarritoPage : ContentPage
{
    private int cantidad = 1;
    IServiceProvider _serviceProvider;
    CartViewModel _cartviewmodel;
    public CarritoPage(IServiceProvider serviceProvider, CartViewModel cartViewModel)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _cartviewmodel = cartViewModel;
        BindingContext = _cartviewmodel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _cartviewmodel.LoadCart();
        _cartviewmodel.CalculateTotalCart();
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

    private async void DetalleCompra(object sender, EventArgs e)
    {
        var _detalleCompra = _serviceProvider.GetService<CompraDetalle>();
        await Navigation.PushAsync(_detalleCompra);
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

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var _detalleCompra = _serviceProvider.GetService<CompraDetalle>();
        await Navigation.PushAsync(_detalleCompra);
    }

    //private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
    //{
    //    double newValue = e.NewValue;
    //    LabelCantidad.Text = $"{newValue}";
    //}
    //// Evento para incrementar la cantidad
    //private void OnIncrementClicked(object sender, EventArgs e)
    //{
    //    cantidad++;
    //    UpdateCantidad();
    //}

    //// Evento para decrementar la cantidad
    //private void OnDecrementClicked(object sender, EventArgs e)
    //{
    //    if (cantidad > 1)
    //    {
    //        cantidad--;
    //    }
    //    UpdateCantidad();
    //}

    //// Actualiza la cantidad mostrada
    //private void UpdateCantidad()
    //{
    //    LabelCantidad.Text = cantidad.ToString();
    //}
}