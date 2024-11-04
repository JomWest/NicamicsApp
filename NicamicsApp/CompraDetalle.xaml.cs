namespace NicamicsApp;

public partial class CompraDetalle : ContentPage
{
    private int cantidad = 1;
    public CompraDetalle()
	{
		InitializeComponent();
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