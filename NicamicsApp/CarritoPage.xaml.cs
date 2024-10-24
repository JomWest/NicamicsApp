namespace NicamicsApp;

public partial class CarritoPage : ContentPage
{
    private int cantidad = 1;
    public CarritoPage()
	{
		InitializeComponent();
	}
    private async void OnImageTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
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
}