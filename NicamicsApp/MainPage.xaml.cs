using BackendlessAPI.Service;
using NicamicsApp.Models;
using NicamicsApp.Service;
using NicamicsApp.ViewModels;
using System.ComponentModel;

namespace NicamicsApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        ComicService _comicService;
        UserServices _userService;

        Perfil_Usuario _perfil;
        CarritoPage _carritoPage;
        DetalleMangaFactory _detalleManga;

        MainPageViewModel _mainPageViewModel;

        public MainPage(ComicService comicService, UserServices userService,
            Perfil_Usuario perfilUsuario,CarritoPage carritoPage,
            DetalleMangaFactory detalleMangaFactory, MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            _comicService = comicService;
            _perfil = perfilUsuario;
            _carritoPage = carritoPage;
            _detalleManga = detalleMangaFactory;
            _userService = userService;
            _perfil = perfilUsuario;

             mainPageViewModel.InitializeData();
            _mainPageViewModel = mainPageViewModel;
            BindingContext = mainPageViewModel;

            _mainPageViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private async void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_mainPageViewModel.SelectedComic))
            {
                // Verifica si hay un cómic seleccionado
                if (_mainPageViewModel.SelectedComic != null && _mainPageViewModel.SelectedComic != "")
                {
                    // Navega a la página de detalle
                    await Navigation.PushAsync(_detalleManga.Create(_mainPageViewModel.SelectedComic));
                    // Limpia el cómic seleccionado después de la navegación
                    _mainPageViewModel.SelectedComic = string.Empty;
                }
            }
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {

        }
        private void OnProfileImageTapped(object sender, EventArgs e)
        {
             Navigation.PushAsync(_perfil);
        }
        private async void OnCarritoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(_carritoPage); 
        }

        private async void CompraTapped(object sender, EventArgs e)
        {
           var detalle = _detalleManga.Create(_mainPageViewModel.ComicMasVendido.comicId);
            await Navigation.PushAsync(detalle);
        }
    }

}
