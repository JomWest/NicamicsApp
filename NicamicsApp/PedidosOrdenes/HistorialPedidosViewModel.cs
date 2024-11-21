using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NicamicsApp.Models;
using NicamicsApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.PedidosOrdenes
{
    public partial class HistorialPedidosViewModel : ObservableObject
    {
        private readonly OrderService _orderService;

        public HistorialPedidosViewModel(OrderService orderService)
        {
            _orderService = orderService;
        }

        [ObservableProperty]
        private ObservableCollection<orderDetailJson> _pedidos = new ObservableCollection<orderDetailJson>();

        [ObservableProperty]
        private orderDetailJson? _orderDetail = null;

        public async Task LoadPedidos()
        {
            Pedidos = new ObservableCollection<orderDetailJson>();

            var response = await _orderService.ObtenerPedidosPorVendedor(IpAddress.userId, IpAddress.token);

            if (response != null)
            {
                Pedidos = new ObservableCollection<orderDetailJson>(response);
            }
        }

        [RelayCommand]
        public void selectedDetail(orderDetailJson orderDetail)
        {
            OrderDetail = null;
            OrderDetail = orderDetail;
        }

    }
}
