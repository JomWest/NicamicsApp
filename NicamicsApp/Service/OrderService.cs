using NicamicsApp.Models;
using NicamicsApp.Models.ModelOrderMapper;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NicamicsApp.Service
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService()
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


        public async Task<List<OrderMapper>> ObtenerOrdenesPorIdUsuario(string userId, string token)
        {
            try
            {
                var url = $"/api/Order/usuario/{userId}";
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<OrderMapper>>();
                }
                else
                {
                    return new List<OrderMapper>();
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


        public async Task<Order> ObtenerOrdenPorId(string orderId, string token)
        {
            try
            {
                var url = $"/api/Order/{orderId}";
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Order>();
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


        public async Task<string> CrearOrden(Order order, string token)
        {
            try
            {
                var url = $"/api/Order";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsJsonAsync(url, order);

                if (response.IsSuccessStatusCode)
                {
                    return "Orden creada exitosamente.";
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Error de solicitud HTTP: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<List<orderDetailJson>?> ObtenerPedidosPorVendedor(string vendedorId, string token)

        {
            try
            {
                // Construir la URL con el vendedorId como parámetro
                var url = $"/api/Order/pedidosPorVendedor?vendedorId={vendedorId}";
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Hacer la solicitud GET
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Deserializar la respuesta en una lista de OrderDetail
                    return await response.Content.ReadFromJsonAsync<List<orderDetailJson>>();
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

        public async Task<ResumenOrderDto?> ObtenerResumenOrdenPorOrderDetailId(string orderDetailId, string token)
        {
            try
            {
                // Construir la URL con el OrderDetailId
                var url = $"/api/Order/resumen/{orderDetailId}";
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Hacer la solicitud GET
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Deserializar la respuesta en un ResumenOrdenDto
                    return await response.Content.ReadFromJsonAsync<ResumenOrderDto>();
                }
                else
                {
                    return null; // En caso de respuesta no exitosa
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


    }
}
