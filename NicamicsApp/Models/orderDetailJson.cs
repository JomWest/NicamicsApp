using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models
{
    public class orderDetailJson
    {
        public string orderDetailId { get; set; }
        public double precio { get; set; }
        public int cantidad { get; set; }
        public string comicId { get; set; }

        public string imgurl { get; set; }
        public string nombrecomic { get; set; }
        public string vendedorId { get; set; }
    }
}
