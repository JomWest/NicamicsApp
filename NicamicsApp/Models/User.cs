using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models
{
    public class User
    {
        public string id { get; set; }

        public string nombre { get; set; } = null!;

        public string foto { get; set; } = null!;

        public int edad { get; set; }

        public string correo { get; set; } = null!;

        public string contraseña { get; set; } = null!;

        public string tipoUsuario { get; set; } = null!;

        public List<string> favoritos { get; set; } = new List<string>();

        public List<string> orders { get; set; } = new List<string>();
    }
}
