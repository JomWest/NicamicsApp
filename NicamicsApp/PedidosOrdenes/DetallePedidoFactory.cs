using NicamicsApp.Models;
using NicamicsApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.PedidosOrdenes
{
    public class DetallePedidoFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DetallePedidoViewModel _detalleOrdenViewModel;
        private readonly OrderService _orderService;
        public DetallePedidoFactory(IServiceProvider serviceProvider, OrderService orderService)
        {
            _serviceProvider = serviceProvider;
            _orderService = orderService;
        }

        public DetallePedido Create(orderDetailJson orderDetail)
        {
            return new DetallePedido(_serviceProvider, _orderService, orderDetail);
        }
    }
}
