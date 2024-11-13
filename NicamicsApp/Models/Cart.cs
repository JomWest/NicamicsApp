using System;
using System.Collections.Generic;
using System.Linq;

namespace NicamicsApp.Models
{
    public class Cart
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public decimal TotalCart
        {
            get
            {
                return Items.Sum(item => item.Cantidad * (decimal)item.Precio);
            }
        }
    }

    public class CartItem
    {
        public string ComicId { get; set; }

        public string VendedorID { get; set; }

        public string imagenPortada { get; set; }
        public string nombreComic { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
    }
}

