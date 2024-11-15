using NicamicsApp.Models;
using Newtonsoft.Json;          
using System.Text;             
using System.Net.Http;
using System.Net.Http.Json;
using NicamicsApp.Service;

namespace NicamicsApp;

public partial class AddComicPage : ContentPage
{
    int stock;
    string imageUrl = string.Empty;
    FileResult? photoToUpload = null;
    private readonly string applicationId = "6811ED10-B9CA-4692-895B-D155D30D93CF";
    private readonly string apiKey = "EA901995-4C55-4759-8D58-193BA7F8D167";

    List<string> generos = new List<string>
        {
            "Accion",
            "Aventura",
            "Comedia",
            "Drama",
            "Fantasia",
            "Horror",
            "Misterio",
            "Romance",
            "Ciencia Ficcion",
            "Deportes",
            "Supernatural",
            "Suspenso",
            "Mecha",
            "Historico"
        };

    ComicService _comicService;
    public AddComicPage(ComicService comicService)
	{
		InitializeComponent();
        stock = Convert.ToInt32(lblStock.Text);
        _comicService = comicService;

        comboBox.ItemsSource = generos;
    }

    private async void btnUploadImage_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Verifica si el dispositivo admite la selección de fotos
            if (MediaPicker.IsCaptureSupported)
            {
                // Abre la galería para seleccionar una foto
                var photo = await MediaPicker.PickPhotoAsync();

                if (photo != null)
                {
                    // Cargar la imagen seleccionada en el control Image
                    var stream = await photo.OpenReadAsync();
                    imgUploadedImage.Source = ImageSource.FromStream(() => stream);
                    photoToUpload = photo;
                }
            }
            else
            {
                await DisplayAlert("Error", "La galería no está disponible en este dispositivo.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar la imagen: {ex.Message}", "OK");
        }
    }

    private async Task<string> UploadImageToBackendless(FileResult photo)
    {
        try
        {
            using (var stream = await photo.OpenReadAsync())
            {
                // Verifica si el archivo ya existe
                string fileName = photo.FileName;
                string url = $"https://api.backendless.com/{applicationId}/{apiKey}/files/nicamicsImages/{fileName}";
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("application-id", applicationId);
                client.DefaultRequestHeaders.Add("secret-key", apiKey);

                var responseCheck = await client.GetAsync(url);
                if (responseCheck.IsSuccessStatusCode)
                {
                    // Si el archivo existe, modifica el nombre del archivo
                    fileName = $"{Path.GetFileNameWithoutExtension(photo.FileName)}_{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
                    url = $"https://api.backendless.com/{applicationId}/{apiKey}/files/nicamicsImages/{fileName}";
                }

                var content = new MultipartFormDataContent();
                var imageContent = new StreamContent(stream);
                imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

                // Agrega la imagen a la solicitud
                content.Add(imageContent, "file", fileName);

                // Realiza la solicitud de subida
                var responseUpload = await client.PostAsync(url, content);

                if (responseUpload.IsSuccessStatusCode)
                {
                    return url; // Retorna la URL de la imagen subida
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo subir la imagen", "OK");
                    return string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Excepción al subir la imagen: {ex.Message}", "OK");
            return string.Empty;
        }
    }

    private void btnLess_Clicked(object sender, EventArgs e)
    {
        if (stock == 1)
        {
            return;
        }
        stock--;
        
        lblStock.Text = stock.ToString();
    }

    private void btnPlus_Clicked(object sender, EventArgs e)
    {
        stock++;
        lblStock.Text = stock.ToString();
    }

    private async void btnSubir_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Verifica si la URL de la imagen está disponible
            if (string.IsNullOrEmpty(photoToUpload?.FileName))
            {
                await DisplayAlert("Error", "Sube una imagen antes de enviar el formulario", "OK");
                return;
            }

            if (string.IsNullOrEmpty(comboBox.SelectedItem.ToString()))
            {
                await DisplayAlert("Error", "Seleccione un género", "OK");
                return;
            }

            imageUrl = await UploadImageToBackendless(photoToUpload);

            // Crea el objeto Comic
            var comic = new Comic
            {
                comicId = "",
                nombre = entryNombre.Text,
                stock = stock,
                imagenPortada = imageUrl, 
                descripcion = entryDesc.Text,
                categoria = comboBox.SelectedItem.ToString(),
                nombrevendedor = IpAddress.nombreusuario,
                precio = double.Parse(entryPrecio.Text),
                vendedorId = IpAddress.userId,
                capitulos = 10,
                imagenes = new List<string> {} 
            };
            // Envía la solicitud POST a tu API
            var response = await _comicService.CrearComic(comic, IpAddress.token);

            if (!response.Contains("Error"))
            {
                await DisplayAlert("Éxito", "Cómic guardado correctamente", "OK");
            }
            else
            {
                await DisplayAlert("Error", $"{response}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo enviar el formulario: {ex.Message}", "OK");
        }
    }

    private async void imgArrowBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}