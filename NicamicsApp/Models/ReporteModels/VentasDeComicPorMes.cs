using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models.ReporteModels
{
    public class VentasDeComicPorMes
    {
        public string NombreComic { get; set; }
        public int Mes { get; set; }
        public double TotalVentas { get; set; }
    }
}
