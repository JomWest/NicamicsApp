using BackendlessAPI.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NicamicsApp.Models;
using NicamicsApp.Models.UserRequest;
using NicamicsApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NicamicsApp.ViewModels
{
    public partial class PerfilUsuarioViewModel : ObservableObject
    {
        private readonly UserServices _userServices;
        private User _user;
        string imageUrl = string.Empty;

        private readonly string applicationId = "6811ED10-B9CA-4692-895B-D155D30D93CF";
        private readonly string apiKey = "EA901995-4C55-4759-8D58-193BA7F8D167";

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
        private string _correousaer;

        [ObservableProperty]
        private string _selectedComic = "";

        [ObservableProperty]
        private ObservableCollection<Comic> comics = new();

        [ObservableProperty]
        private bool _isVendedor;

        [ObservableProperty]
        private string _mensaje = "";

        [ObservableProperty]
        private string _contraseña = "";

        [ObservableProperty]
        private FileResult? _fileResult = null;

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

                if (user != null)
                {
                    _user = user;
                    FotoUsuario = user.foto;
                    NombreUsuario = user.nombre;
                    Correousaer = user.correo;
                    NombreCompleto = user.nombreCompleto;
                    Contraseña = user.contraseña;
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
            SelectedComic = "";
            SelectedComic = comicId;
        }

        [RelayCommand]
        public async Task AgregarEliminarFavoritoComic(string comicId)
        {
            try
            {
                var request = new AgregarFavoritoRequest
                {
                    UserId = IpAddress.userId,
                    NuevoFavorito = comicId
                };

                var response = await _userServices.AgregarEliminarComicFavorito(request);

                LoadComics();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task ActualizarUsuario()
        {
            try
            {
                if (string.IsNullOrEmpty(NombreCompleto))
                {
                    Mensaje = "El campo nombre no puede estar vacío";
                    return;
                }
                if (string.IsNullOrEmpty(NombreUsuario))
                {
                    Mensaje = "El campo nombre de usuario no puede estar vacío";
                    return;
                }
                if (string.IsNullOrEmpty(Correousaer))
                {
                    Mensaje = "El campo correo electrónico no puede estar vacío";
                    return;
                }

                if (!ValidarCorreo(Correousaer))
                {
                    Mensaje = "El correo electrónico ingresado no es válido";
                    return;
                }

                if (FileResult != null)
                {
                    imageUrl = await UploadImageToBackendless(FileResult,_user.id);

                    if(imageUrl != null && imageUrl != "")
                    {
                        FotoUsuario = imageUrl;
                    }
                    else
                    {
                        return;
                    }
                }

                LoadUser();

                _user.nombre = NombreCompleto;
                _user.foto = FotoUsuario;
                _user.correo = Correousaer;
                _user.nombre = NombreUsuario;

                bool resultado = await _userServices.ActualizarUsuario(_user);

                if (resultado)
                {
                    Mensaje = "Perfil actualizado exitosamente.";
                }
                else
                {
                    Mensaje = "Error al actualizar el perfil.";
                }
            }
            catch (Exception ex)
            {
                Mensaje = $"Error al actualizar el perfil: {ex.Message}";
            }
        }

        public bool ValidarCorreo(string correo)
        {
            string patronCorreo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(correo, patronCorreo))
            {
                return false;
            }

            return true;
        }

        private async Task<string> UploadImageToBackendless(FileResult photo, string userId)
        {
            try
            {
                // Validar entrada
                if (photo == null || string.IsNullOrEmpty(userId))
                {
                    Mensaje = "Foto o ID de usuario inválidos.";
                    return string.Empty;
                }

                // Nombre esperado para la imagen del usuario
                string fileName = $"{userId}_perfil{Path.GetExtension(photo.FileName)}";
                string fileUrl = $"https://api.backendless.com/{applicationId}/{apiKey}/files/nicamicsImages/{fileName}";
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("application-id", applicationId);
                client.DefaultRequestHeaders.Add("secret-key", apiKey);

                // Verificar si la imagen ya existe
                var responseCheck = await client.GetAsync(fileUrl);
                if (responseCheck.IsSuccessStatusCode)
                {
                    // Si el archivo existe, eliminarlo primero
                    var responseDelete = await client.DeleteAsync(fileUrl);
                    if (!responseDelete.IsSuccessStatusCode)
                    {
                        Mensaje = "No se pudo eliminar la imagen existente.";
                        return string.Empty;
                    }
                }

                // Subir la nueva imagen
                using (var stream = await photo.OpenReadAsync())
                {
                    var content = new MultipartFormDataContent();
                    var imageContent = new StreamContent(stream);
                    imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

                    content.Add(imageContent, "file", fileName);

                    var responseUpload = await client.PostAsync(fileUrl, content);
                    if (responseUpload.IsSuccessStatusCode)
                    {
                        return fileUrl; // Retorna la URL de la imagen subida
                    }
                    else
                    {
                        Mensaje = "No se pudo subir la imagen.";
                        return string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje = $"Excepción al subir la imagen: {ex.Message}";
                return string.Empty;
            }
        }



    }
}
