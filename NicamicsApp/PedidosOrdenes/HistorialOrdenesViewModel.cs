﻿using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class HistorialOrdenesViewModel : ObservableObject
    {
        private OrderService _orderService;
        public HistorialOrdenesViewModel(OrderService orderService)
        { 
        _orderService = orderService;
        }

        [ObservableProperty]
        private ObservableCollection<orderDetailJson> ordenes = new ObservableCollection<orderDetailJson>();

        [ObservableProperty]
        private orderDetailJson? _orderDetailSelected = null;

        [ObservableProperty]
        private string _mensaje = "";

        public async Task LoadOrders()
        {
            try
            {
                Ordenes = new ObservableCollection<orderDetailJson>();
                var response = await _orderService.ObtenerOrdenesPorIdUsuario(IpAddress.userId, IpAddress.token);

                if (response.Count > 0)
                {
                    for(int i = 0; i < response.Count; i++)
                    {
                        for (int j = 0; j < response[i].orderDetail.Count; j++)
                        {
                            Ordenes.Add(response[i].orderDetail[j]);
                        }
                    }
                }
                else
                {
                    Mensaje = "No se encontraron órdenes";
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
        }

        [RelayCommand]
        public void selectedDetail(orderDetailJson orderDetail)
        {
            OrderDetailSelected = null;
            OrderDetailSelected = orderDetail;
        }

    }
}
