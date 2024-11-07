using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NicamicsApp.Models;
using NicamicsApp.Service;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace NicamicsApp.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        private readonly CartService _cartService;

        public CartViewModel(CartService cartService)
        {
            _cartService = cartService;
            LoadCart();
        }

        [ObservableProperty]
        private ObservableCollection<CartItem> cartItems = new();

        [ObservableProperty]
        private string mensaje = ""; 

       
        public async void LoadCart()
        {
            try
            {
                
                var cart = await _cartService.ObtenerCarrito(IpAddress.userId, IpAddress.token);
                //cartItems = new ObservableCollection<CartItem>(cart);
                Console.WriteLine("Carrito cargado con éxito"); 
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error al cargar el carrito: {ex.Message}");
            }
        }

      
        /*[RelayCommand]
        public async void AddToCart(CartItem item)
        {
            try
            {
                var response = await _cartService.AgregarAlCarrito(item, IpAddress.userId, IpAddress.token);
                Console.WriteLine(response); 
                LoadCart();  
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error al agregar al carrito: {ex.Message}");
            }
        }*/

       
        [RelayCommand]
        public async void RemoveFromCart(CartItem item)
        {
            try
            {
                var response = await _cartService.EliminarDelCarrito(item.ComicId, IpAddress.userId, IpAddress.token);
                Console.WriteLine(response);
                LoadCart();  
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error al eliminar del carrito: {ex.Message}");
            }
        }
    }
}
