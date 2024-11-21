using Microsoft.Extensions.DependencyInjection;
using NicamicsApp.Models;
using NicamicsApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.PedidosOrdenes
{
    public class DetalleOrdenFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DetalleOrdenViewModel _detalleOrdenViewModel;
        private readonly OrderService _orderService;
        public DetalleOrdenFactory(IServiceProvider serviceProvider,OrderService orderService, DetalleOrdenViewModel detalleOrdenViewModel)
        {
            _serviceProvider = serviceProvider;
            _orderService = orderService;
            _detalleOrdenViewModel = detalleOrdenViewModel;
        }

        public DetalleOrden Create(orderDetail orderDetail)
        {
            return new DetalleOrden(_serviceProvider, _orderService, _detalleOrdenViewModel, orderDetail);
        }
    }
}
