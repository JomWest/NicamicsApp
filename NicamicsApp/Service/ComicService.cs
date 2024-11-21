using NicamicsApp.Models;
using NicamicsApp.Models.ComicRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
                BaseAddress = new Uri($"{IpAddress.ip}") // Usa la IP de tu máquina o dirección correcta
            };
        }

        public async Task<List<Comic>> Obtener20Comics(string token)
        {
            try
            {
                var url = "/api/Comic/20-Comics";

                // Agregar el encabezado de autenticación Bearer
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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
                throw new Exception($"Error de solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<List<Comic>> Obtener20ComicsPorNombre(string nombre, string token)
        {
            try
            {
                if (nombre == "")
                {
                    return await Obtener20Comics(token);
                }

                var url = $"/api/Comic/BuscarComicsPorNombre?nombre={nombre.ToLower()}";

                // Agregar el encabezado de autenticación Bearer
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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
                throw new Exception($"Error de solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<List<Comic>> Obtener20ComicsPorCategoria(string categoria, string token)
        {
            try
            {
                if (categoria.ToLower() == "inicio")
                {
                    return await Obtener20Comics(token);
                }

                var url = $"/api/Comic/BuscarComicsPorCategoria?categoria={categoria.ToLower()}";

                // Agregar el encabezado de autenticación Bearer
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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
                throw new Exception($"Error de solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<string> CrearComic(Comic comic, string token)
        {
            try
            {
                var url = "/api/Comic";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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

        public async Task<Comic> ObtenerComicPorId(string comicId, string token)
        {
            try
            {
                var url = $"/api/Comic/{comicId}";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Comic>();
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

        public async Task<List<Comic>> ObternercodigoporIdVendedor(string VendedorID, string token)
        {
            try
            {
                var url = $"/api/Comic/BuscarComicPorIdVendedor?vendedorId={VendedorID}";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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
                throw new Exception($"Error de solicitud HTTP: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


        public async Task<ComicMasVendidoCategoriaResp> ComicConMasVentas(string token)
        {
            try
            {
                var url = "/api/Comic/ComicMasVendido";

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ComicMasVendidoCategoriaResp>();
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

        public async Task<bool> ActualizarComic(string comicId, Comic comicActualizado)
        {
            try
            {
                var url = $"/api/Comic/{comicId}";


                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", IpAddress.token);

                var response = await _httpClient.PutAsJsonAsync(url, comicActualizado);

                return response.IsSuccessStatusCode;
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


        public async Task<ComicMasVendidoCategoriaResp?> ComicConMasVentasPorCategoria(string categoria)
        {
            try
            {
                var url = $"/api/Comic/ComicMasVendidoPorCategoria?categoria={categoria}";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", IpAddress.token);

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ComicMasVendidoCategoriaResp>();
                }
                else
                {
                    return null;
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

        public async Task<bool> EsElegibleParaRating(string userId, string comicId, string token)
        {
            try
            {
                // Establecer el token de autorización en los encabezados
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Realizar la solicitud al API
                var url = $"/api/Comic/esElegibleParaRating/{userId}/{comicId}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<dynamic>();
                    return true; // El usuario es elegible para calificar
                }
                else
                {
                    return false; // El usuario no es elegible para calificar
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar elegibilidad para calificar: {ex.Message}");
            }
        }

    }
}
