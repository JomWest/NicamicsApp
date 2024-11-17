using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NicamicsApp.Models;
using System.Text.Json;
using NicamicsApp.Service;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace NicamicsApp.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        private readonly CartService _cartService;
        private readonly AddressService _addressService;
        private readonly OrderService _orderService;
        private readonly TarifaService _tarifaService;

        public CartViewModel(CartService cartService, AddressService addressService,
            OrderService orderService, TarifaService tarifaService)
        {
            _cartService = cartService;
            _addressService = addressService;
            _orderService = orderService;
            _tarifaService = tarifaService;
        }

        [ObservableProperty]
        private string _cardNumber = "";

        [ObservableProperty]
        private string _cardHolder = "";


        [ObservableProperty]
        private string _expiryMonth = "";
   
        [ObservableProperty]
        private string _expiryYear = "";




    [ObservableProperty]
        private ObservableCollection<CartItem> cartItems = new();

        [ObservableProperty]
        private string cartId;

        [ObservableProperty]
        private double totalCart;

        [ObservableProperty]
        private Address selectedAddress;

        [ObservableProperty]
        private ObservableCollection<Address> addresses = new();

        [ObservableProperty]
        private string _addressName = "";

        [ObservableProperty]
        private string _city = "";

        [ObservableProperty]
        private string _state = "";

        [ObservableProperty]
        private int _numero = 0;

        [ObservableProperty]
        private string _nombre = "";

        [ObservableProperty]
        private string _nombreComic = "";

  

        [ObservableProperty]
        private string portada = "";




        [ObservableProperty]
        private string _envio;

        [ObservableProperty]
        private double _precio ;


        [ObservableProperty]
        private string _comicId = "";

        [ObservableProperty]
        private string _vendedorId = "";


        [ObservableProperty]
        private string mensaje = "";


        [ObservableProperty]
        private string _errorMessage = "";


        public async Task LoadAddresses()
        {
            try
            {
                var response = await _addressService.ObtenerDireccionesUsuarioTodas(IpAddress.userId, IpAddress.token);

                if (response != null)
                {
                    Addresses = new ObservableCollection<Address>(response);

                    // Opcional: Verificar los datos
                    foreach (var address in Addresses)
                    {
                        Console.WriteLine(response);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar direcciones: {ex.Message}");
            }
        }


        public async Task LoadCart()
        {
            try
            {
                var cart = await _cartService.ObtenerCarrito(IpAddress.userId, IpAddress.token);

                if(cart != null)
                {
                    CartItems = new ObservableCollection<CartItem>(cart.Items);
                    CartId = cart.Id;
                    foreach (var item in cart.Items)
                    {
                        Console.WriteLine($"ImagenPortada{item.imagenPortada}  ,NombreComic: {item.nombreComic}, Precio: {item.Precio}, Cantidad: {item.Cantidad}");
                    }
                }
                else
                {
                    CartItems = new ObservableCollection<CartItem>();
                    TotalCart = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el carrito: {ex.Message}");
            }
        }


        [RelayCommand]
        public async Task<string> CrearOrden()
        {
            try
            {

                Console.WriteLine($"Total del carrito: {TotalCart}");
                Console.WriteLine($"Número de items en el carrito: {CartItems.Count}");


                if (SelectedAddress == null)
                {
                    throw new Exception("Por favor seleccione una dirección de envío.");
                }
                Console.WriteLine($"Dirección seleccionada: {SelectedAddress.Nombre}, {SelectedAddress.City}, {selectedAddress.Numero}");


                Order order = new Order
                {
                    OrderId = "",
                    userId = IpAddress.userId,
                    comprador = IpAddress.nombreusuario,
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    Total = TotalCart,
                    direccion = new Direccion
                    {
                        nombre = SelectedAddress.Nombre,
                        departamento = SelectedAddress.Departamento,
                        municipio = SelectedAddress.City,
                        direccion = SelectedAddress.Street,
                        numero = SelectedAddress.Numero
                    },
                    tarjetaCredito = new tarjetaCredito
                    {
                        Cardnumbre = CardNumber,
                        FechaExpiracion = ExpiryMonth + "/" + ExpiryYear,
                        CardHolder = CardHolder
                    },
                    orderDetail = new List<orderDetail>()
                };

                foreach (var item in CartItems)
                {
                    Console.WriteLine($"Agregando item al detalle: {item.nombreComic}, Precio: {item.Precio}, Cantidad: {item.Cantidad}");
                    var orderDetail = new orderDetail
                    {
                        precio = item.Precio,
                        cantidad = item.Cantidad,
                        nombrecomic = item.nombreComic,
                        imgurl = item.imagenPortada,    
                        comicId = item.ComicId,
                        vendedorId = item.VendedorID
                    };
                    order.orderDetail.Add(orderDetail);
                }
                Console.WriteLine($"Order: {JsonConvert.SerializeObject(order, Formatting.Indented)}");

                // Crear la orden
                var response = await _orderService.CrearOrden(order, IpAddress.token);
                if (!response.Contains("Error"))
                {
                    Mensaje = "Has hecho tu compra con exito!, Gracias por confiar en NiCamics!";
                    var lastMessage = await _cartService.EliminarCarrito(IpAddress.userId);
                
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la orden: {ex.Message}");
                return ex.Message;
            }
        }


        public double CalculateTotalCart()
        {
            double total = 0;
            foreach (var item in CartItems)
            {
                total += item.Cantidad * item.Precio;
            }
            TotalCart = total;
            return total;
        }

        [RelayCommand]
        public async Task<double> CalculateTotalCart2(Address? address)
        {
            double total = 0;
            double envioTotal = 0;

            // Verificar si hay direcciones disponibles
            if (Addresses == null || !Addresses.Any())
            {
                Envio = "C$ 0";
                TotalCart = CalculateTotalCart();
                return 0;
            }

            foreach (var item in CartItems)
            {
                double? costoEnvio;
                // Calcular el costo del envío para este item

                if (address?.Departamento == "")
                {
                    costoEnvio = await CalcularCostoEnvio(item.VendedorID, Addresses[0].Departamento);
                }
                else
                {
                    costoEnvio = await CalcularCostoEnvio(item.VendedorID, address?.Departamento!);
                }
                

                // Sumar el costo del envío si no es nulo
                if (costoEnvio.HasValue)
                {
                    envioTotal += costoEnvio.Value;
                }

                // Sumar el costo de los productos al total
                total += item.Cantidad * item.Precio;
            }
            SelectedAddress = address!;
            Envio = $"C$ {envioTotal.ToString("F2")}";
            TotalCart = total + envioTotal;
            return total + envioTotal;
        }

        private async Task<double?> CalcularCostoEnvio(string vendedorId, string departamentoDestino)
        {
            try
            {
                // Obtener el departamento del vendedor
                var departamentoVendedor = await _tarifaService.ObtenerDepartamentoUsuario(vendedorId);

                if (departamentoVendedor != null)
                {
                    // Obtener el precio de la tarifa entre los departamentos
                    return await _tarifaService.ObtenerPrecioTarifa(departamentoVendedor, departamentoDestino, IpAddress.token);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción relacionada con el cálculo del envío
                Console.WriteLine($"Error al calcular el costo de envío: {ex.Message}");
            }

            // Retornar null si no se pudo calcular el costo de envío
            return null;
        }


        [RelayCommand]
        public async void RemoveFromCart(CartItem item)
        {
            try
            {
                var response = await _cartService.EliminarDelCarrito(CartId, item.ComicId, IpAddress.token);

                if (response.Contains("Error"))
                {
                    Mensaje = response;
                }
                else
                {
                    CartItems.Remove(item);
                    TotalCart = CalculateTotalCart();
                    Mensaje = response;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar del carrito: {ex.Message}");
            }
        }
    }
}