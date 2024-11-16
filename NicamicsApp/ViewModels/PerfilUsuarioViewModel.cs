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

namespace NicamicsApp.ViewModels
{
    public partial class PerfilUsuarioViewModel: ObservableObject
    {
        UserServices _userServices;
        public PerfilUsuarioViewModel(UserServices userService)
        {
            _userServices = userService;
        }

        [ObservableProperty]
        private string _fotoUsuario;

        [ObservableProperty]
        private string _nombreUsuario;

        [ObservableProperty]
        private string _nombreCompleto;

        [ObservableProperty]
        private string _selectedComic;

        [ObservableProperty]
        private ObservableCollection<Comic> comics = new();

        [ObservableProperty]
        private bool _isVendedor;

        public void InitializeData()
        {
            LoadUser();
            LoadComics();
        }

        public async void LoadUser()
        {
            try
            {
                var user = await _userServices.ObtenerUsuarioPorId(IpAddress.userId);

                if(user != null)
                {
                    FotoUsuario = user.foto;
                    NombreUsuario = user.nombre;
                    NombreCompleto = user.nombreCompleto;
                    IpAddress.tipouser = user.tipoUsuario;

                    IsVendedor = IpAddress.tipouser.ToLower() == "vendedor";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        } 

        public async void LoadComics()
        {
            try
            {
                var comics = await _userServices.ObtenerComicsFavoritos(IpAddress.userId);

                if (comics != null)
                {
                    Comics = new ObservableCollection<Comic>(comics);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        [RelayCommand]
        public void SelectComic(string comicId)
        {
            SelectedComic = comicId;
        }
    }
}
