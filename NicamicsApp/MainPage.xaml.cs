using NicamicsApp.Models;
using NicamicsApp.Service;

namespace NicamicsApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        ComicService _comicService;
        Perfil_Usuario _perfil;
        CarritoPage _carritoPage;
        DetalleMangaFactory _detalleManga;

        public MainPage(ComicService comicService, Perfil_Usuario perfilUsuario,CarritoPage carritoPage, DetalleMangaFactory detalleMangaFactory)
        {
            InitializeComponent();
            _comicService = comicService;
            _perfil = perfilUsuario;
            _carritoPage = carritoPage;
            _detalleManga = detalleMangaFactory;
            LoadComics();
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
           var detalle = _detalleManga.Create("");
            await Navigation.PushAsync(detalle);
        }

        private async void LoadComics()
        {
            try
            {
                // Llama a la función para obtener los cómics
                List<Comic> comics = await _comicService.Obtener20Comics();

                foreach (var comic in comics)
                {
                    // Crear el contenedor principal para cada cómic
                    var comicStack = new StackLayout
                    {
                        Margin = new Thickness(10),
                        WidthRequest = 120,
                    };

                    // Crear el marco y la imagen del cómic
                    var comicFrame = new Frame
                    {
                        Padding = 0,
                        CornerRadius = 20,
                        BackgroundColor = string.IsNullOrEmpty(comic.imagenPortada) ? Colors.Blue : Colors.Transparent
                    };

                    var comicImage = new Image
                    {
                        Source = !string.IsNullOrEmpty(comic.imagenPortada) ? comic.imagenPortada : null,
                        WidthRequest = 120,
                        HeightRequest = 180,
                        Aspect = Aspect.AspectFill
                    };

                    comicFrame.Content = comicImage;

                    // Crear el label para el nombre del cómic
                    var comicLabel = new Label
                    {
                        Text = comic.nombre,
                        FontSize = 14,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalTextAlignment = TextAlignment.Center
                    };

                    // Añadir el marco de la imagen y el label al StackLayout del cómic
                    comicStack.Children.Add(comicFrame);
                    comicStack.Children.Add(comicLabel);

                    comicFrame.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(async () =>
                        {
                            // Navegar a la página DetalleManga pasando comicId
                            await Navigation.PushAsync(_detalleManga.Create(comic.comicId));
                        })
                    });

                    // Añadir el StackLayout del cómic al contenedor principal
                    ComicContainer.Children.Add(comicStack);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }

}
