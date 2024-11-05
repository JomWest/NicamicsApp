using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models.AuthRequest
{
    public class LoginRequest
    {
        public string Correo { get; set; }
        public string Contraseña { get; set; }

        public LoginRequest(string correo, string contraseña)
        { 
            this.Correo = correo.Trim().ToLower();
            this.Contraseña = contraseña.Trim();
        }
    }
}
