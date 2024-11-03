using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models.UserRequest
{
    public class AgregarFavoritoRequest
    {
        public string UserId { get; set; } = null!;
        public string NuevoFavorito { get; set; } = null!;
    }
}
