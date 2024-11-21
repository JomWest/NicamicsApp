using NicamicsApp.Models;
using NicamicsApp.Service;

namespace NicamicsApp.PedidosOrdenes;

public partial class DetalleOrden : ContentPage
{
	private DetalleOrdenViewModel _viewModel;
    private string vendedorId;
    private string orderDetailId;
	public DetalleOrden(IServiceProvider serviceProvider, OrderService orderService,
		 orderDetailJson orderDetail)
	{
		InitializeComponent();
        var userService = serviceProvider.GetService<UserServices>();
        _viewModel = new DetalleOrdenViewModel(orderDetail, userService,orderService);
        BindingContext = _viewModel;

        vendedorId = orderDetail.vendedorId;
        orderDetailId = orderDetail.orderDetailId;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadVendedor(vendedorId);
        await _viewModel.LoadExtraInfo(orderDetailId);
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
    }
}