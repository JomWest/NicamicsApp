using NicamicsApp.Service;

namespace NicamicsApp;

public partial class Perfil_Usuario : ContentPage
{
    IServiceProvider _serviceProvider;
    UserServices _userServices;

    DetalleMangaFactory _detalleManga;
    public Perfil_Usuario(IServiceProvider serviceProvider, UserServices userService, DetalleMangaFactory detalleMangaFactory)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _userServices = userService;
        _detalleManga = detalleMangaFactory;
        LoadUser();
        LoadComics();
    }

    private async void LoadUser()
    {
        try
        {
            var user = await _userServices.ObtenerUsuarioPorId(IpAddress.userId);

            imgFoto.Source = user.foto;
            lblNombre.Text = user.nombre;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    private async void LoadComics()
    {
        try
        {
            var comics = await _userServices.ObtenerComicsFavoritos(IpAddress.userId);

            if (comics != null)
            {
                // Limpiar el contenido anterior
                ComicFavoritos.Children.Clear();

                foreach (var comic in comics)
                {
                    // Crear un Frame para cada cómic
                    var comicFrame = new Frame
                    {
                        CornerRadius = 10,
                        Padding = 10,
                        Margin = new Thickness(5),
                        HasShadow = true,
                        BackgroundColor = Color.FromRgb(255, 255, 255),
                    };

                    comicFrame.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = new Command(async () =>
                        {
                            // Navegar a la página DetalleManga pasando comicId
                             NavegarAlDetalle(comic.comicId);
                        })
                    });

                    // Crear un layout para organizar la imagen y el nombre
                    var comicLayout = new HorizontalStackLayout
                    {
                        Spacing = 10
                    };

                    // Contenedor de la imagen con bordes redondeados o fondo azul
                    View coverView;
                    if (!string.IsNullOrEmpty(comic.imagenPortada))
                    {
                        // Mostrar la imagen de portada si existe
                        coverView = new Image
                        {
                            Source = comic.imagenPortada,
                            Aspect = Aspect.AspectFill,
                            WidthRequest = 80,
                            HeightRequest = 100
                        };
                    }
                    else
                    {
                        // Mostrar un fondo azul si no hay imagen
                        coverView = new BoxView
                        {
                            Color = Color.FromRgb(0, 90, 225),
                            WidthRequest = 80,
                            HeightRequest = 100
                        };
                    }

                    // Envolver la imagen o fondo en un Frame para bordes redondeados
                    var imageContainer = new Frame
                    {
                        WidthRequest = 80,
                        HeightRequest = 100,
                        CornerRadius = 10,
                        Padding = 0,
                        IsClippedToBounds = true,
                        Content = coverView
                    };

                    // Nombre del cómic
                    var comicName = new Label
                    {
                        Text = comic.nombre,
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold,
                        VerticalOptions = LayoutOptions.Start
                    };

                    // Agregar imagen/fondo azul y nombre al layout
                    comicLayout.Children.Add(imageContainer);
                    comicLayout.Children.Add(comicName);

                    // Agregar el layout al Frame
                    comicFrame.Content = comicLayout;

                    // Agregar el Frame al StackLayout
                    ComicFavoritos.Children.Add(comicFrame);

                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async void NavegarAlDetalle(string comicId)
    {
        var detalleComic = _detalleManga.Create(comicId);
        await Navigation.PushAsync(detalleComic);
    }

    private async void OnImageTapped(object sender, EventArgs e)
    {
        var _mainPage = _serviceProvider.GetService<MainPage>();
        await Navigation.PushAsync(_mainPage);
    }

    private async void OnImageTappedMenu(object sender, EventArgs e)
    {
        MainContent.IsVisible = false;

        Overlay.IsVisible = true;
        BottomMenu.IsVisible = true;

        await BottomMenu.TranslateTo(0, 0, 250, Easing.SinInOut);
    }
    private async void OnTappedSalidaMenu(object sender, EventArgs e)
    {
        CloseMenu();
    }


    private async void CloseMenu()
    {
        await BottomMenu.TranslateTo(0, 700, 250, Easing.SinInOut);

        BottomMenu.IsVisible = false;
        Overlay.IsVisible = false;
        MainContent.IsVisible = true;
    }

}
