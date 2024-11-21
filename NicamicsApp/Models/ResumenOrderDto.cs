using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models
{
    public class ResumenOrderDto { 
    public string CorreoComprador { get; set; } = string.Empty;
    public string NombreCompletoComprador { get; set; } = string.Empty;
    public string FotoPerfilComprador { get; set; } = string.Empty;
    public string DepartamentoComprador { get; set; } = string.Empty;
    public string MunicipioComprador { get; set; } = string.Empty;
    public string DireccionComprador { get; set; } = string.Empty;
    public double PrecioEnvio { get; set; }
}
}
