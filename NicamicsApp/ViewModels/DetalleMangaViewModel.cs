using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NicamicsApp.Models;
using NicamicsApp.Models.UserRequest;
using NicamicsApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weborb.Client;
using static NicamicsApp.Models.Cart;

namespace NicamicsApp.ViewModels
{
    public partial class DetalleMangaViewModel : ObservableObject
    {
        private readonly ComicService _comicService;
        private readonly UserServices _userServices;
        private readonly CartService _cartService;
        private readonly string _comicId;

        public DetalleMangaViewModel(ComicService comicService, UserServices userServices, CartService cartService,string comicId)
        {
            _comicService = comicService;
            _userServices = userServices;
            _cartService = cartService;
            _comicId = comicId;
            LoadComic(comicId);
        }

        [ObservableProperty]
        private string _nombreComic = "";

        [ObservableProperty]
        private string _descripcionComic = "";

        // Propiedad decimal para el valor numérico del precio
        [ObservableProperty]
        private double _precio;

        // Propiedad string para mostrar el precio con formato en la UI
        public string PrecioFormatted => $"C$ {Precio}";

        [ObservableProperty]
        private string _imagenPortada = "";

        [ObservableProperty]
        private string _rating = "1/5";

        [ObservableProperty]
        private string _favImage = "favorito_blanco";

        [ObservableProperty]
        private string _mensaje = "";

        [ObservableProperty]
        public int _cantidad = 0;

        [ObservableProperty]
        public string _idVendedor = "";

        public async void LoadComic(string comicId)
        {
            try
            {
                var response = await _comicService.ObtenerComicPorId(comicId, IpAddress.token);

                if (response != null)
                {
                    IdVendedor = response.vendedorId;
                    NombreComic = response.nombre;
                    Precio = response.precio;
                    DescripcionComic = response.descripcion;
                    ImagenPortada = response.imagenPortada;
                    Rating = $"{response.ratingPromedio.ToString()}/5";
                    CambiarImagenFavorito(IpAddress.userId, comicId);

                    // Notifica a la UI que PrecioFormatted ha cambiado
                    OnPropertyChanged(nameof(PrecioFormatted));
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
        }

        private async void CambiarImagenFavorito(string userId, string comicId)
        {
            try
            {
                var response = await _userServices.VerificarComicEnFavoritos(userId, comicId);
                FavImage = response ? "favorito_rojo" : "favorito_blanco";
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
        }

        [RelayCommand]
        public async void AgregarEliminarFavoritoComic()
        {
            try
            {
                var request = new AgregarFavoritoRequest
                {
                    UserId = IpAddress.userId,
                    NuevoFavorito = _comicId
                };

                var response = await _userServices.AgregarEliminarComicFavorito(request);

                CambiarImagenFavorito(IpAddress.userId, _comicId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }


        [RelayCommand]
        public async Task<string> CrearCarrito()
        {
            try
            {
                Cart cart = new Cart
                {
                    Id = "",
                    UserId = IpAddress.userId,
                    Items = new List<CartItem>(),
                    CreatedAt = DateTime.Now,
                };
                var response = await _cartService.CrearCarrito(cart, cart.UserId, IpAddress.token);

                return response;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return ex.Message;
            }
        }

        [RelayCommand]
        public async void AgregarAlCarrito()
        {
            try
            {
                Cart cart = await _cartService.ObtenerCarrito(IpAddress.userId, IpAddress.token);

                if (cart == null)
                {
                    var message = await CrearCarrito();

                    if (message.Contains("Carrito"))
                    {
                        Cart cart2 = await _cartService.ObtenerCarrito(IpAddress.userId, IpAddress.token);

                        if (cart2 != null)
                        {
                            var item = new CartItem
                            {
                                ComicId = _comicId,
                                VendedorID = IdVendedor,
                                imagenPortada = ImagenPortada,
                                nombreComic = NombreComic,
                                Cantidad = Cantidad,
                                Precio = Precio

                            };
                            await _cartService.AgregarCarrito(item, cart2.Id);
                        }
                    }
                }
                else
                {
                    var item = new CartItem
                    {
                        ComicId = _comicId,
                        VendedorID = IdVendedor,
                        imagenPortada = ImagenPortada,
                        nombreComic = NombreComic,
                        Cantidad = Cantidad,
                        Precio = Precio
                    };

                    await _cartService.AgregarCarrito(item, cart.Id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
    

