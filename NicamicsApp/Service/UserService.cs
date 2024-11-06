using NicamicsApp.Models;
using NicamicsApp.Models.UserRequest;
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

        public async Task<User> ObtenerUsuarioPorCorreo(string correo)
        {
            try
            {
                // Construir la URL con los parámetros de consulta
                var url = $"/api/User/{correo}";

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

        public async Task<bool> ActualizarUsuario(User user)
        {
            try
            {
                var url = $"/api/User/{user.id}";
                var response = await _httpClient.PutAsJsonAsync(url, user);

                Console.WriteLine($"USER {user}");
                Console.WriteLine($"CODIGO ${response.IsSuccessStatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
              throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<List<Comic>> ObtenerComicsFavoritos(string userId)
        {
            try
            {
                var url = $"/api/User/ObtenerComicsFavoritos?userId={userId}";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadFromJsonAsync<List<Comic>>();
                    return responseData;
                }
                else
                {
                    return new List<Comic>();
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

        public async Task<bool> VerificarComicEnFavoritos(string userId, string comicId)
        {
            try
            {
                var url = $"/api/User/VerificarComicEnFavoritos?userId={userId}&comicId={comicId}";

                var response = await _httpClient.GetAsync(url);

                Console.WriteLine($"Respuesta {response.StatusCode} {response.ReasonPhrase}");
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejo de error de red
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Manejo de otros errores
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<string> AgregarEliminarComicFavorito(AgregarFavoritoRequest agregarFavoritoRequest)
        {
            try
            {
                var url = "/api/User/agregarFavorito";

                var response = await _httpClient.PutAsJsonAsync(url, agregarFavoritoRequest);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return responseData;
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejo de error de red
                return $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Manejo de otros errores
                return $"Error: {ex.Message}";
            }
        }
    }
}
