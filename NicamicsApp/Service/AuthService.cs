using Newtonsoft.Json;
using NicamicsApp.Models;
using NicamicsApp.Models.AuthRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Service
{
    public class AuthService
    {
        HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri($"{IpAddress.ip}") // Usa la IP de tu máquina o dirección correcta
            };
        }

        public async Task<LoginResponse> LoginUser(LoginRequest loginRequest)
        {
            try
            {
                // Construir la URL con los parámetros de consulta
                var url = $"/api/Auth/login";

                // Realizar la solicitud POST sin contenido adicional en el cuerpo

                var response = await _httpClient.PostAsJsonAsync(url, loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseData);
                    return loginResponse;
                }
                else
                {
                    throw new Exception("Credenciales incorrectas");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<string> RegisterUser(RegisterRequest registerRequest)
        {
            try
            {
                var url = $"/api/Auth/register";

                var response = await _httpClient.PostAsJsonAsync(url, registerRequest);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return responseData; // Esto contendrá el token si la autenticación es exitosa
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<bool> ActualizarUsuarioAsync(User user)
        {
            try
            {
                // Validar entrada
                if (user == null || string.IsNullOrEmpty(user.id))
                {
                    throw new ArgumentException("El usuario o su ID no pueden ser nulos.");
                }

                // Construir la URL
                var url = $"/api/Auth/{user.id}";

                // Agregar token al encabezado de autorización
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", IpAddress.token);

                // Enviar solicitud PUT
                var response = await _httpClient.PutAsJsonAsync(url, user);

                // Validar respuesta
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el usuario: {ex.Message}");
            }
        }

        public async Task<User?> ObtenerUsuarioPorCorreoAsync(string correo)
        {
            try
            {
                if (string.IsNullOrEmpty(correo))
                {
                    throw new ArgumentException("El correo no puede ser nulo o vacío.");
                }

                var response = await _httpClient.GetAsync($"api/Auth/correo/{correo}");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<User>();
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine(httpEx.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}
