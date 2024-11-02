using NicamicsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Service
{
    public class ComicService
    {
        HttpClient _httpClient;
        public ComicService()
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

        public async Task<List<Comic>> Obtener20Comics()
        {
            try
            {
                var url = "/api/Comic/20-Comics";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Comic>>();
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

        public async Task<string> CrearComic(Comic comic)
        {
            try
            {
                // Construir la URL con los parámetros de consulta
                var url = $"/api/Comic";

                // Realizar la solicitud POST sin contenido adicional en el cuerpo

                var response = await _httpClient.PostAsJsonAsync(url, comic);

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
                return $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<Comic> ObtenerComicPorId(string comicId)
        {
            try
            {
                // Construir la URL con los parámetros de consulta
                var url = $"/api/Comic/{comicId}";

                // Realizar la solicitud POST sin contenido adicional en el cuerpo

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadFromJsonAsync<Comic>();
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
