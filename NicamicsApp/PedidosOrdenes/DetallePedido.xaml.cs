using NicamicsApp.Models;
using NicamicsApp.Service;

namespace NicamicsApp.PedidosOrdenes;

public partial class DetallePedido : ContentPage
{
	private readonly DetallePedidoViewModel _detallePedidoViewModel;
    private readonly string orderDetailId;

    public DetallePedido(IServiceProvider serviceProvider, OrderService orderService 
	      ,orderDetailJson orderDetail)
	{
		InitializeComponent();
		var viewModel = new DetallePedidoViewModel(orderDetail,orderService);
		BindingContext = viewModel;
        _detallePedidoViewModel = viewModel;
        orderDetailId = orderDetail.orderDetailId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _detallePedidoViewModel.LoadInfo(orderDetailId);
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
	}
}