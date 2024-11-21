using CommunityToolkit.Mvvm.ComponentModel;
using NicamicsApp.Models;
using NicamicsApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.PedidosOrdenes
{
    public partial class DetallePedidoViewModel: ObservableObject
    {
        private OrderService _orderService;
        public DetallePedidoViewModel(orderDetailJson orderDetailJson, OrderService orderService) 
        {
            _orderService = orderService;
            OrderDetail = orderDetailJson;
            Total = orderDetailJson.cantidad * orderDetailJson.precio;
        }

        [ObservableProperty]
        private orderDetailJson? _orderDetail = null;

        [ObservableProperty]
        private ResumenOrderDto? _resumenOrderDto = null;

        [ObservableProperty]
        private double _total;

        public async Task LoadInfo(string orderDetailId)
        {
            try
            {
                var response = await _orderService.ObtenerResumenOrdenPorOrderDetailId(orderDetailId, IpAddress.token);

                if (response != null)
                {
                    ResumenOrderDto = response;
                    Total += ResumenOrderDto.PrecioEnvio;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
