
namespace NicamicsApp.Models
{
    // Clase 'Direccion' que representa una dirección física de un usuario o entidad
    public class Direccion
    {
        // Propiedad 'departamento' que almacena el nombre del departamento (estado/provincia) de la dirección
        // Por defecto, la propiedad se inicializa con una cadena vacía.
        public string departamento { get; set; } = "";

        // Propiedad 'municipio' que almacena el nombre del municipio (ciudad/localidad) dentro del departamento
        // También se inicializa con una cadena vacía de manera predeterminada.
        public string municipio { get; set; } = "";

        // Propiedad 'direccion' que almacena la dirección específica (calle, número, etc.) dentro del municipio
        // Se inicializa con una cadena vacía de forma predeterminada.
        public string direccion { get; set; } = "";
    }
}
