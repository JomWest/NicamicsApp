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
    }

    [ObservableProperty]
    private ObservableCollection<Comic> comics = new();


    [ObservableProperty]
    private Comic? selectedComic;

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

    [ObservableProperty]
    private string _mensaje = "";


    public async Task LoadComicVendedor()
    {
        try
        {
            var comic = await _comicService.ObternercodigoporIdVendedor(IpAddress.userId, IpAddress.token);

            if (comic != null)
            {
                Comics = new ObservableCollection<Comic>(comic);
            }
            else
            {
                Mensaje = "No se encontraron comics";
                Console.WriteLine("No comic found for the given vendor.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading comics: {ex.Message}");
        }
    }

    [RelayCommand]
    public void SeleccionarComic(Comic comic)
    {
        Console.WriteLine(comic.nombre);
        SelectedComic = null;
        SelectedComic = comic;
    }



    [RelayCommand]
    public void CancelarEdicion()
    {
        IsEditing = false; 
    }
}
