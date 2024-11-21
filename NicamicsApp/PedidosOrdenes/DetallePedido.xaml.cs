using NicamicsApp.Models;
using NicamicsApp.Service;

namespace NicamicsApp.PedidosOrdenes;

public partial class DetallePedido : ContentPage
{
	public DetallePedido(IServiceProvider serviceProvider, OrderService orderService 
	      ,orderDetailJson orderDetail)
	{
		InitializeComponent();
		var viewModel = new DetallePedidoViewModel(orderDetail);
		BindingContext = viewModel;

    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
	}
}