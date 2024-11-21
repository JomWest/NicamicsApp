using System.ComponentModel;

namespace NicamicsApp.PedidosOrdenes;

public partial class HistorialOrdenes : ContentPage
{
	private HistorialOrdenesViewModel _historialOrdenes;
    private DetalleOrdenFactory _detalleOrdenFactory;

    public HistorialOrdenes(HistorialOrdenesViewModel historialOrdenesViewModel, DetalleOrdenFactory detalleOrdenFactory)
	{
		InitializeComponent();
        BindingContext = historialOrdenesViewModel;
        _historialOrdenes = historialOrdenesViewModel;
        _detalleOrdenFactory = detalleOrdenFactory;

        historialOrdenesViewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private async void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {


        if (e.PropertyName == nameof(_historialOrdenes.OrderDetailSelected))
        {
            // Verifica si hay un cómic seleccionado
            if (_historialOrdenes.OrderDetailSelected != null)
            {
                // Navega a la página de detalle
                await Navigation.PushAsync(_detalleOrdenFactory.Create(_historialOrdenes.OrderDetailSelected));
            }
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _historialOrdenes.LoadOrders();
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}