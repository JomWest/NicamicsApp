using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models.ReporteModels
{
    public class ComicsMejorValorados
    {
        public string NombreComic { get; set; } = null!;
        public double RatingPromedio { get; set; }
        public int CantidadRatings { get; set; }
    }
}
