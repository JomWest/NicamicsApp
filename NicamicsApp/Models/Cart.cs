using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models
{
    public class Cart
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public double TotalCart { get; set; } = 0;
    }

    public class CartItem
    {
        public string ComicId { get; set; }
        public int Cantidad { get; set; }
        public double Precio {  get; set; }
    }

}
