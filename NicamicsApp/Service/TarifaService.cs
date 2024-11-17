using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Service
{
    public class TarifaService
    {
        private readonly HttpClient _httpClient;

        public TarifaService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri($"{IpAddress.ip}")
            };
        }


        public async Task<double?> ObtenerPrecioTarifa(string origen, string destino, string token)
        {
            try
            {
                // Construimos la URL con los parámetros de consulta
                var url = $"/api/Tarifas/precio?origen={Uri.EscapeDataString(origen)}&destino={Uri.EscapeDataString(destino)}";

                // Agregamos el token en los encabezados de autorización
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Realizamos la solicitud GET
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Leemos el contenido de la respuesta como JSON
                    var resultado = await response.Content.ReadFromJsonAsync<Dictionary<string, double>>();

                    // Retornamos el valor del precio si existe
                    return resultado != null && resultado.ContainsKey("precio") ? resultado["precio"] : null;
                }
                else
                {
                    // Manejo de errores específicos de la API
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error en la respuesta: {response.StatusCode} - {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<string?> ObtenerDepartamentoUsuario(string userId)
        {
            try
            {
                var url = $"/api/User/{userId}/departamento";

                // Agregar token de autorización
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", IpAddress.token);

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadFromJsonAsync<DepartamentoResponse>();
                    return resultado?.departamento;
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public class DepartamentoResponse
        {
            public string? departamento { get; set; }
        }


    }
}
