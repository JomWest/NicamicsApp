using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace NicamicsApp.Models
{

    public class Order
    {
        public string OrderId { get; set; }
        public double Total { get; set; }
        public DateOnly Fecha { get; set; }

        public string comprador { get; set; } 

        public string userId { get; set; }
        public Direccion direccion { get; set; }
        public tarjetaCredito tarjetaCredito { get; set; }
        public List<orderDetail> orderDetail { get; set; } = new List<orderDetail>();
    }

    public class tarjetaCredito
    {
        public string cardnumbre { get; set; }
        public string FechaExpiracion { get; set; }
        public string CardHolder { get; set; }
    }

    public class orderDetail
    {
        public double precio { get; set; }
        public int cantidad { get; set; }
        public string comicId { get; set; }

        public string imgurl { get; set; }
        public string nombrecomic { get; set; } 
        public string vendedorId { get; set; }
    }
}