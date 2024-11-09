using Microcharts;
using NicamicsApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NicamicsApp.Service
{
    public class CartService
    {
        private readonly HttpClient _httpClient;

        public CartService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri($"http://{IpAddress.ip}")
            };
        }

        // Obtener carrito
        public async Task<Cart?> ObtenerCarrito(string userId, string token)
        {
            try
            {
                var url = $"/api/Cart/usuario/{userId}";
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Cart>();
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

        // Agregar un ítem al carrito
        public async Task<string> CrearCarrito(Cart cart, string userId, string token)
        {
            try
            {
                var url = $"/api/Cart";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsJsonAsync(url, cart);
                if (response.IsSuccessStatusCode)
                {
                    return "Carrito creado exitosamente.";
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

        public async Task<string> AgregarCarrito(CartItem cartItem, string cartId)
        {
            try
            {
                var url = $"/api/Cart/{cartId}/items";

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", IpAddress.token);

                var response = await _httpClient.PostAsJsonAsync(url, cartItem);

                if (response.IsSuccessStatusCode)
                {
                    return "Item agregado al carrito exitosamente.";
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

        // Eliminar un ítem del carrito
        public async Task<string> EliminarDelCarrito(string comicId, string userId, string token)
        {
            try
            {
                var url = $"/api/Cart/{userId}/{comicId}";  

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return "Item eliminado del carrito exitosamente.";
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
    }
}
