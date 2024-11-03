using NicamicsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Service
{
    public class UserServices
    {
        HttpClient _httpClient;

        public UserServices()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri($"https://{IpAddress.ip}:7077") // Usa la IP de tu máquina o dirección correcta
            };
        }

        public async Task<User> ObtenerUsuarioPorId(string userId)
        {
            try
            {
                // Construir la URL con los parámetros de consulta
                var url = $"/api/User/{userId}";

                // Realizar la solicitud POST sin contenido adicional en el cuerpo

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadFromJsonAsync<User>();
                    return responseData;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejo de error de red
                throw new Exception($"Error de solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Manejo de otros errores
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
