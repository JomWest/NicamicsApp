using System;

namespace NicamicsApp.Models
{
    public class Address
    {
        public string Id { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Street { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Departamento { get; set; } = null!;

        public int Numero { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
