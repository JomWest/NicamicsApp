using System;
using System.Collections.Generic;

namespace NicamicsApp.Models
{

    public class Order
    {
        public string OrderId { get; set; }
        public double Total { get; set; }
        public DateOnly Fecha { get; set; }
        public string usuarioId { get; set; }
        public direccion direccion { get; set; }
        public tarjetaCredito tarjetaCredito { get; set; }
        public List<orderDetail> orderDetail { get; set; } = new List<orderDetail>();
    }

    public class direccion
    {
        public string nombre { get; set; }
        public string departamento { get; set; }
        public string municipio { get; set; }
        public string direccionn { get; set; }
        public int numero { get; set; }
    }

    public class tarjetaCredito
    {
        public string Cardnumbre { get; set; }
        public string FechaExpiracion { get; set; }
        public string CardHolder { get; set; }
    }

    public class orderDetail
    {
        public string orderDetailId { get; set; }
        public double precio { get; set; }
        public int cantidad { get; set; }
        public string comicId { get; set; }
        public string vendedorId { get; set; }
    }
}