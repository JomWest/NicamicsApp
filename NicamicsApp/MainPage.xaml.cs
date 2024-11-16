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
        IServiceProvider _serviceProvider;

        MainPageViewModel _mainPageViewModel;

        public MainPage(IServiceProvider serviceProvider, ComicService comicService, UserServices userService,
            DetalleMangaFactory detalleMangaFactory,MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            _comicService = comicService;
            _detalleManga = detalleMangaFactory;
            _userService = userService;
            _serviceProvider = serviceProvider;

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
            var perfilPage = _serviceProvider.GetRequiredService<Perfil_Usuario>();
            Navigation.PushAsync(perfilPage);
        }
        private async void OnCarritoClicked(object sender, EventArgs e)
        {
            var carritoPage = _serviceProvider.GetRequiredService<CarritoPage>();
            await Navigation.PushAsync(carritoPage); 
        }

        private async void CompraTapped(object sender, EventArgs e)
        {
           var detalle = _detalleManga.Create(_mainPageViewModel.ComicMasVendido.comicId);
            await Navigation.PushAsync(detalle);
        }

        private async void DisplayExitConfirmation()
        {
            var result = await DisplayAlert("Salir", "¿Deseas salir de la aplicación?", "Sí", "No");
            if (result)
            {
                // Si el usuario confirma, cerrar la aplicación
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else
            {
                // Si el usuario elige No, mantener la app abierta
            }
        }

        protected override bool OnBackButtonPressed()
        {
            DisplayExitConfirmation();
            return true;
            
        }
    }

}
