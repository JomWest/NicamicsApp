using NicamicsApp.ViewModels;
using System.ComponentModel;

namespace NicamicsApp;

public partial class CompraDetalle : ContentPage
{
    private int cantidad = 1;
    IServiceProvider _serviceProvider;
    CartViewModel _cartviewmodel;
    public CompraDetalle(IServiceProvider serviceProvider)
	{
        _serviceProvider = serviceProvider;
        _cartviewmodel = _serviceProvider.GetService<CartViewModel>();
        BindingContext = _cartviewmodel;
        _cartviewmodel.LoadCart();
        _cartviewmodel.LoadAddresses();
        InitializeComponent();

        _cartviewmodel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private async void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CartViewModel.Mensaje))
        {
            var viewModel = (CartViewModel)BindingContext;
            if (!string.IsNullOrEmpty(viewModel.Mensaje))
            {
                await DisplayAlert("Mensaje", viewModel.Mensaje, "OK");
                viewModel.Mensaje = string.Empty;
                var _compradetalle = _serviceProvider.GetService<MainPage>();
                Navigation.InsertPageBefore(_compradetalle, this);
                await Navigation.PopAsync();

            }
        }
    }

    private async void DireccionesTapped(object sender, EventArgs e)
    {
        var _addressPage = _serviceProvider.GetService<adddireccion>();
        Navigation.InsertPageBefore(_addressPage, this);
        await Navigation.PopAsync();
    }

    private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
    {
        double newValue = e.NewValue;
        //LabelCantidad.Text = $"{newValue}";
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
        //LabelCantidad.Text = cantidad.ToString();
    }
    
    private async void CloseMenu()
    {
        await BottomMenu.TranslateTo(0, 700, 200, Easing.SinInOut);

        BottomMenu.IsVisible = false;

        MainContent.IsVisible = true;
    }

    private async void PagoBtn(object sender, EventArgs e)
    {
        MainContent.IsVisible = false;


        BottomMenu.IsVisible = true;

        await BottomMenu.TranslateTo(0, 100, 250, Easing.SinInOut);
    }
    private async void OnTappedSalidaMenu(object sender, EventArgs e)
    {
        CloseMenu();
    }

    private async void imgArrowBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}