using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NicamicsApp.Models;
using NicamicsApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.ViewModels
{
    public partial class ComicEditViewModel : ObservableObject
    {

        private readonly ComicService _comicService;
        private readonly string applicationId = "6811ED10-B9CA-4692-895B-D155D30D93CF";
        private readonly string apiKey = "EA901995-4C55-4759-8D58-193BA7F8D167";
        string imageUrl = string.Empty;

        public ComicEditViewModel(ComicService comicService)
        {
            _comicService = comicService;
        }

        [ObservableProperty]
        private Comic _comic = new Comic();

        [ObservableProperty]
        public string _mensaje = "";

        [ObservableProperty]
        public FileResult? imageResult = null;

        public async Task LoadComic(string comicId)
        {
            try
            {
                var response = await _comicService.ObtenerComicPorId(comicId, IpAddress.token);

                if (response != null)
                {
                    Comic = response;
                }
                else
                {
                    Mensaje = "No fue posible cargar el comic";
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
        }

        [RelayCommand]
        public async Task SaveComic()
        {
            try
            {
                if (string.IsNullOrEmpty(Comic.nombre))
                {
                    Mensaje = "El campo nombre no puede estar vacío";
                    return;
                }
                if (string.IsNullOrEmpty(Comic.descripcion))
                {
                    Mensaje = "El descripción no puede estar vacío";
                    return;
                }
                if (string.IsNullOrEmpty(Comic.precio.ToString()))
                {
                    Mensaje = "El campo precio no puede estar vacío";
                    return;
                }
                if (string.IsNullOrEmpty(Comic.stock.ToString()))
                {
                    Mensaje = "El campo stock no puede estar vacío";
                    return;
                }

                if (Comic.precio <= 0)
                {
                    Mensaje = "El precio del comic no puede ser menor o igual que 0";
                    return;
                }

                if (Comic.stock < 0)
                {
                    Mensaje = "El stock no puede ser menor que 0";
                    return;
                }

                if (ImageResult != null)
                {
                    imageUrl = await UploadImageToBackendless(ImageResult, Comic.comicId);

                    if (imageUrl != null && imageUrl != "")
                    {
                        Comic.imagenPortada = imageUrl;
                    }
                    else
                    {
                        return;
                    }
                }

                var response = await _comicService.ActualizarComic(Comic.comicId, Comic);

                if (response)
                {
                    Mensaje = "El comic se actualizó con éxito";
                }
                else
                {
                    Mensaje = "Hubo un error al actualizar el comic";
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
        }

        private async Task<string> UploadImageToBackendless(FileResult photo, string comicId)
        {
            try
            {
                // Validar entrada
                if (photo == null || string.IsNullOrEmpty(comicId))
                {
                    Mensaje = "Foto o ID del cómic inválidos.";
                    return string.Empty;
                }

                // Generar nuevo nombre de archivo basado en comicId
                string fileExtension = Path.GetExtension(photo.FileName);
                string fileName = $"{comicId}_comicPortada{fileExtension}";
                string newFileUrl = $"https://api.backendless.com/{applicationId}/{apiKey}/files/nicamicsImages/{fileName}";

                // Configurar el cliente HTTP
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("application-id", applicationId);
                client.DefaultRequestHeaders.Add("secret-key", apiKey);

                // Eliminar imagen existente si se proporciona la URL
                if (!string.IsNullOrEmpty(Comic.imagenPortada))
                {
                    var responseDelete = await client.DeleteAsync(Comic.imagenPortada);
                    if (!responseDelete.IsSuccessStatusCode && responseDelete.StatusCode != System.Net.HttpStatusCode.NotFound)
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

                    var responseUpload = await client.PostAsync(newFileUrl, content);
                    if (responseUpload.IsSuccessStatusCode)
                    {
                        return newFileUrl; // Retorna la nueva URL de la imagen
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
