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

        public RegisterRequest(string username, string password, string email, string tipoUser) 
        { 
            this.Username = username.Trim();
            this.Password = password.Trim();
            this.Email = email.Trim().ToLower();
            this.TipoUsuario = tipoUser.Trim();
        }

    }
}
