using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models
{
    public class Comic
    {
        public string comicId { get; set; } = "";
        public string nombre { get; set; } = null!;
        public int stock { get; set; }
        public string imagenPortada { get; set; } = null!;
        public string descripcion { get; set; } = null!;
        public string categoria { get; set; } = null!;
        public decimal precio { get; set; }
        public string vendedorId { get; set; } = null!;
        public int capitulos { get; set; }
        public List<string> imagenes { get; set; } = new List<string>();

    }
}
