﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models.ReporteModels
{
    public class VentasPorCategoria
    {
        public string Categoria { get; set; }

        public int CantidadVendida { get; set; }

        public double TotalVenta { get; set; }
    }
}