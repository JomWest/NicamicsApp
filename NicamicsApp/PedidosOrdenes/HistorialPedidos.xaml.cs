using System.ComponentModel;

namespace NicamicsApp.PedidosOrdenes;

public partial class HistorialPedidos : ContentPage
{
    private HistorialPedidosViewModel _historialPedidosViewModel;
    private DetallePedidoFactory _detallePedidoFactory;
    
    public HistorialPedidos(HistorialPedidosViewModel historialPedidosViewModel, DetallePedidoFactory detallePedidoFactory)
	{
		InitializeComponent();
		BindingContext = historialPedidosViewModel;
        _historialPedidosViewModel = historialPedidosViewModel;
        _detallePedidoFactory = detallePedidoFactory;
        historialPedidosViewModel.PropertyChanged += ViewModel_PropertyChanged;
	}

    private async void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {


        if (e.PropertyName == nameof(_historialPedidosViewModel.OrderDetail))
        {
            // Verifica si hay un cómic seleccionado
            if (_historialPedidosViewModel.OrderDetail != null)
            {
                // Navega a la página de detalle
                await Navigation.PushAsync(_detallePedidoFactory.Create(_historialPedidosViewModel.OrderDetail));
            }
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _historialPedidosViewModel.LoadPedidos();
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}