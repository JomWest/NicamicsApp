using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models.ComicRequest
{
    public class ComicMasVendidoCategoriaResp
    {
        public string ComicId { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string ImagenPortada { get; set; } = string.Empty; 
        public string Descripcion { get; set; } = string.Empty;  
        public int TotalVentas { get; set; } 
    }
}
