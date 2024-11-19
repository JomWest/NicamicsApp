using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NicamicsApp.Models;
using NicamicsApp.Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace NicamicsApp.ViewModels;

public partial class InventarioViewModel : ObservableObject
{
    private readonly ComicService _comicService;

    public InventarioViewModel(ComicService comicService)
    {
        _comicService = comicService;
        LoadComicVendedor();
    }

    [ObservableProperty]
    private ObservableCollection<Comic> comics = new();


    [ObservableProperty]
    private Comic selectedComic;

    [ObservableProperty]
    private string comicNombre;

    [ObservableProperty]
    private string comicDescripcion;

    [ObservableProperty]
    private double comicPrecio;

    [ObservableProperty]
    private int comicStock;

    [ObservableProperty]
    private string comicImagenPortada;

    [ObservableProperty]
    private bool isEditing = false; 


    public async Task LoadComicVendedor()
    {
        try
        {
            var comic = await _comicService.ObternercodigoporIdVendedor(IpAddress.userId, IpAddress.token);

            if (comic != null)
            {
                Comics.Clear();
                Comics.Add(comic);
            }
            else
            {
                Console.WriteLine("No comic found for the given vendor.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading comics: {ex.Message}");
        }
    }

    // Comando para iniciar la edición
    [RelayCommand]
    public void EditarComic(Comic comic)
    {
        if (comic == null) return;

        SelectedComic = comic;
        comicNombre = comic.nombre;
        ComicDescripcion = comic.descripcion;
        ComicPrecio = comic.precio;
        ComicStock = comic.stock;
        ComicImagenPortada = comic.imagenPortada;

        IsEditing = true;
    }

    [RelayCommand]
    public async Task GuardarCambios()
    {
        if (SelectedComic == null) return;

        SelectedComic.nombre = ComicNombre;
        SelectedComic.descripcion = ComicDescripcion;
        SelectedComic.precio = comicPrecio;
        SelectedComic.stock = ComicStock;
        SelectedComic.imagenPortada = ComicImagenPortada;


        var resultado = await _comicService.ActualizarComic(selectedComic.comicId,SelectedComic);

        if (resultado)
        {
            await App.Current.MainPage.DisplayAlert("Éxito", "Cómic actualizado correctamente.", "OK");
            IsEditing = false; 
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Error", "No se pudo actualizar el cómic.", "OK");
        }
    }


    [RelayCommand]
    public void CancelarEdicion()
    {
        IsEditing = false; 
    }
}
