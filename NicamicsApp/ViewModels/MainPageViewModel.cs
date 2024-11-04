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
            LoadComics();
            ComicMasVendidoFunc();
        }


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

        public async void LoadUser()
        {
            try
            {
                var user = await _userService.ObtenerUsuarioPorId(IpAddress.userId);
                if (user != null){
                    FotoUsuario = user.foto;
                    NombreUsuario = user.nombre;
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
                var comicsList = await _comicService.Obtener20Comics();
                Comics = new ObservableCollection<Comic>(comicsList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }

        private async void ComicMasVendidoFunc()
        {
            try
            {
                var response = await _comicService.ComicConMasVentas();

                if(response != null)
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

    }
}
