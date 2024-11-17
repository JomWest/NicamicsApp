using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NicamicsApp.Models;
using NicamicsApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weborb.Client;

namespace NicamicsApp.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {

        private readonly ComicService _comicService;
        private readonly UserServices _userService;

        public MainPageViewModel(ComicService comicService, UserServices userService)
        {
            _comicService = comicService;
            _userService = userService;

            Categorias = new List<Categoria>
            {
        new Categoria("Inicio", true),
        new Categoria("Accion", false),
        new Categoria("Aventura", false),
        new Categoria("Comedia", false),
        new Categoria("Drama", false),
        new Categoria("Fantasia", false),
        new Categoria("Horror", false),
        new Categoria("Misterio", false),
        new Categoria("Romance", false),
        new Categoria("Ciencia Ficcion", false),
        new Categoria("Deportes", false),
        new Categoria("Supernatural", false),
        new Categoria("Suspenso", false),
        new Categoria("Mecha", false),
        new Categoria("Historico", false)
    };
        }

        [ObservableProperty]
        private List<Categoria> _categorias;

        [ObservableProperty]
        private bool _isRefreshing = false;

        [ObservableProperty]
        private string nombreUsuario = "";

        [ObservableProperty]
        private string fotoUsuario = "https://backendlessappcontent.com/6811ED10-B9CA-4692-895B-D155D30D93CF/C6C17909-5D3E-4B34-B9D9-771B4D9817A0/files/nicamicsImages/defaultProfile.jpeg";

        [ObservableProperty]
        private ObservableCollection<Comic> comics = new();

        [ObservableProperty]
        private string mensaje = "";

        [ObservableProperty]
        private Comic? comicMasVendido;

        [ObservableProperty]
        private string selectedComic;

        [ObservableProperty]
        private string _selectedCategoria;

        [ObservableProperty]
        private string _nombreBusqueda;

        public void InitializeData()
        {
            LoadUser();
            LoadComics();
            ComicMasVendidoFunc();
        }

        public async void LoadUser()
        {
            try
            {
                var user = await _userService.ObtenerUsuarioPorId(IpAddress.userId);
                if (user != null)
                {
                    FotoUsuario = user.foto;
                    NombreUsuario = user.nombre;
                    IpAddress.nombreusuario = user.nombre;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ViewModelMessageError {ex.Message}");
            }
        }

        private async void LoadComics()
        {
            try
            {
                var comicsList = await _comicService.Obtener20Comics(IpAddress.token);
                Comics = new ObservableCollection<Comic>(comicsList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task LoadComicsPorNombre()
        {
            try
            {
                var comicsList = await _comicService.Obtener20ComicsPorNombre(NombreBusqueda, IpAddress.token);
                Comics = new ObservableCollection<Comic>(comicsList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task LoadComicsPorCategoria(string categoria)
        {
            try
            {
                var response = await _comicService.Obtener20ComicsPorCategoria(categoria, IpAddress.token);

                if (response.Count == 0)
                {
                    Mensaje = "";
                    Mensaje = $"No se encontraron comics en la categoría: {categoria}";
                }
                else
                {
                    Comics = new ObservableCollection<Comic>(response);
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }

        }

        private async void ComicMasVendidoFunc()
        {
            if (string.IsNullOrEmpty(IpAddress.token))
            {
                Console.WriteLine("El token no está disponible");
                return;
            }
            try
            {
                var response = await _comicService.ComicConMasVentas(IpAddress.token);

                if (response != null)
                {
                    ComicMasVendido = response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }

        [RelayCommand]
        public void SelectComic(string comicId)
        {
            SelectedComic = comicId;
        }

        [RelayCommand]
        private void ExecuteRefreshCommand()
        {
            // Empezamos el refresco
            IsRefreshing = true;

            try
            {
                NombreBusqueda = "";
                InitializeData();
            }
            finally
            {
                // Detenemos el refresco
                IsRefreshing = false;
            }
        }

    }
}
