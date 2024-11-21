using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models.ModelOrderMapper
{
    public class OrderMapper
    {
        public string OrderId { get; set; }
        public double Total { get; set; }
        public DateOnly Fecha { get; set; }

        public string comprador { get; set; }

        public string userId { get; set; }
        public Direccion direccion { get; set; }
        public tarjetaCredito tarjetaCredito { get; set; }
        public List<orderDetailJson> orderDetail { get; set; } = new List<orderDetailJson>();
    }
}
