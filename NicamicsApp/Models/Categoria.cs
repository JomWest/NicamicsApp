using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models
{
    public class Categoria
    {
        public string categoria { get; set; } = "";
        public bool isSelected { get; set; } = false;

        public Categoria(string categoria, bool isSelected)
        {
            this.categoria = categoria;
            this.isSelected = isSelected;
        }
    }
}
