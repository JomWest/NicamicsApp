using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models.AuthRequest
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string TipoUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public Direccion Departamento { get; set; }

        public RegisterRequest(string nombreCompleto,string username, string password, string email, string tipoUser, string departamento) 
        { 
            this.NombreCompleto = nombreCompleto;
            this.Username = username.Trim();
            this.Password = password.Trim();
            this.Email = email.Trim().ToLower();
            this.TipoUsuario = tipoUser.Trim();
            this.Departamento = new Direccion
            {
                departamento = departamento,
                numero = 0,
                municipio = "",
                nombre = nombreCompleto,
                direccion = ""
            };
        }

    }
}
