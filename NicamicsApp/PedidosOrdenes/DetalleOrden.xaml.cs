using NicamicsApp.Models;
using NicamicsApp.Service;

namespace NicamicsApp.PedidosOrdenes;

public partial class DetalleOrden : ContentPage
{
	public DetalleOrden(IServiceProvider serviceProvider, OrderService orderService,
		DetalleOrdenViewModel detalleOrdenViewModel, orderDetail orderDetail)
	{
		InitializeComponent();
	}

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
    }
}