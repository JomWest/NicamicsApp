using BackendlessAPI.Service;
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
    public partial class DetalleOrdenViewModel : ObservableObject
    {
        private UserServices _userServices;
        private OrderService _orderService;
        public DetalleOrdenViewModel(orderDetailJson orderDetail, UserServices userServices, OrderService orderService)
        {
            OrderDetail = orderDetail;
            _userServices = userServices;
            _orderService = orderService;
            Total = orderDetail.precio * orderDetail.cantidad;
        }

        [ObservableProperty]
        private orderDetailJson? _orderDetail;

        [ObservableProperty]
        private double _total;

        [ObservableProperty]
        private User _userVendedor;

        [ObservableProperty]
        private ResumenOrderDto? _resumenOrderDto;

        public async Task LoadVendedor(string vendedorId)
        {
            try
            {
                var response = await _userServices.ObtenerUsuarioPorId(vendedorId);

                if (response != null)
                {
                    UserVendedor = response;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }


        public async Task LoadExtraInfo(string orderDetail)
        {
            try
            {
                var response = await _orderService.ObtenerResumenOrdenPorOrderDetailId(orderDetail, IpAddress.token);

                if (response != null)
                {
                    ResumenOrderDto = response;
                    Total += ResumenOrderDto.PrecioEnvio;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
