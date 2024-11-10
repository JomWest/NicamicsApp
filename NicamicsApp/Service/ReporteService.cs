using NicamicsApp.Models.ReporteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Service
{
    public class ReporteService
    {
        private readonly HttpClient _httpClient;

        public ReporteService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri($"http://{IpAddress.ip}") // Usa la IP de tu máquina o dirección correcta
            };
        }

        public async Task<List<ComicsMasVendidos>> GetComicsMasVendidosAsync(string vendedorId, string startDate, string endDate)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ComicsMasVendidos>>(
                    $"/api/Reportes/productos-mas-vendidos?idVendedor={vendedorId}&startDate={startDate}&endDate={endDate}"
                );

                return response ?? new List<ComicsMasVendidos>();
            }
            catch (HttpRequestException ex)
            {
                // Aquí puedes registrar el error y manejarlo de forma más específica si es necesario
                Console.WriteLine($"Error al conectar con la API: {ex.Message}");
                return new List<ComicsMasVendidos>(); // Devuelve una lista vacía como respuesta por defecto
            }
            catch (Exception ex)
            {
                // Manejar otros tipos de excepciones inesperadas
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return new List<ComicsMasVendidos>();
            }
        }

        public async Task<List<VentasDeComicPorMes>> GetTotalVentasComicPorMes(string comicId, int año)
       {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<VentasDeComicPorMes>>(
                    $"/api/Reportes/total-ventas-comic-por-mes?comicId={comicId}&year={año}"
                );

                return response ?? new List<VentasDeComicPorMes>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al conectar con la API: {ex.Message}");
                return new List<VentasDeComicPorMes>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return new List<VentasDeComicPorMes>();
            }
        }

        public async Task<List<VentasPorCategoria>> GetVentasPorCategoriaAsync(string vendedorId, string startDate, string endDate)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<VentasPorCategoria>>(
                    $"/api/Reportes/ventas-por-categoria?idVendedor={vendedorId}&startDate={startDate}&endDate={endDate}"
                );

                return response ?? new List<VentasPorCategoria>();
            }
            catch (HttpRequestException ex)
            {
                // Aquí puedes registrar el error y manejarlo de forma más específica si es necesario
                Console.WriteLine($"Error al conectar con la API: {ex.Message}");
                return new List<VentasPorCategoria>(); // Devuelve una lista vacía como respuesta por defecto
            }
            catch (Exception ex)
            {
                // Manejar otros tipos de excepciones inesperadas
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return new List<VentasPorCategoria>();
            }
        }

        public async Task<double> GetVentasTotalesPorFecha(string vendedorId, string startDate, string endDate)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<double>(
                    $"/api/Reportes/ventas-total-por-fecha?idVendedor={vendedorId}&startDate={startDate}&endDate={endDate}"
                );

                return response;
            }
            catch (HttpRequestException ex)
            {
                // Aquí puedes registrar el error y manejarlo de forma más específica si es necesario
                Console.WriteLine($"Error al conectar con la API: {ex.Message}");
                return 0; // Devuelve una lista vacía como respuesta por defecto
            }
            catch (Exception ex)
            {
                // Manejar otros tipos de excepciones inesperadas
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return 0;
            }
        }

        public async Task<List<ComicsMejorValorados>> GetComicsMejorValoradosAsync(string vendedorId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ComicsMejorValorados>>(
                    $"/api/Reportes/productos-mejor-valorados?idVendedor={vendedorId}"
                );

                return response ?? new List<ComicsMejorValorados>();
            }
            catch (HttpRequestException ex)
            {
                // Aquí puedes registrar el error y manejarlo de forma más específica si es necesario
                Console.WriteLine($"Error al conectar con la API: {ex.Message}");
                return new List<ComicsMejorValorados>(); // Devuelve una lista vacía como respuesta por defecto
            }
            catch (Exception ex)
            {
                // Manejar otros tipos de excepciones inesperadas
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return new List<ComicsMejorValorados>();
            }
        }

    }
}
