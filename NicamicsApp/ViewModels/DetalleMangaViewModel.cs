using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NicamicsApp.Models.UserRequest;
using NicamicsApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.ViewModels
{
    public partial class DetalleMangaViewModel: ObservableObject
    {
        private readonly ComicService _comicService;
        private readonly UserServices _userServices;
        private readonly string _comicId;

        public DetalleMangaViewModel(ComicService comicService, UserServices userServices,string comicId) 
        {
            _comicService = comicService;
            _userServices = userServices;
            _comicId = comicId;
            LoadComic(comicId);
        }

        [ObservableProperty]
        private string _nombreComic = "";

        [ObservableProperty]
        private string _DescripcionComic = "";

        [ObservableProperty]
        private string _precio = "";

        [ObservableProperty]
        private string _imagenPortada = "";

        [ObservableProperty]
        private string _favImage = "favorito_blanco";
        
        [ObservableProperty]
        private string _mensaje = "";

        public async void LoadComic(string comicId)
        {
            try
            {
                var response = await _comicService.ObtenerComicPorId(comicId);

                if (response != null)
                {
                    NombreComic = response.nombre;
                    Precio = $"C$ {response.precio}";
                    DescripcionComic = response.descripcion;
                    ImagenPortada = response.imagenPortada;
                    CambiarImagenFavorito(IpAddress.userId,comicId);
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
                var response = await _userServices.VerificarComicEnFavoritos(userId,comicId);
                if (response)
                {
                    FavImage = "favorito_rojo";
                }
                else
                {
                    FavImage = "favorito_blanco";
                }
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
    }
}
