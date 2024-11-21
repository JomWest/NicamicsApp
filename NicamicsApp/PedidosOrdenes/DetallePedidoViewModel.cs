using CommunityToolkit.Mvvm.ComponentModel;
using NicamicsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.PedidosOrdenes
{
    public partial class DetallePedidoViewModel: ObservableObject
    {
        public DetallePedidoViewModel(orderDetailJson orderDetailJson) 
        {
            OrderDetail = orderDetailJson;
            Total = orderDetailJson.cantidad * orderDetailJson.precio;
        }

        [ObservableProperty]
        private orderDetailJson? _orderDetail = null;

        [ObservableProperty]
        private double _total;
    }
}
