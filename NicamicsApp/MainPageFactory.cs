using NicamicsApp.Service;
using NicamicsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp
{
   public class MainPageFactory
    {
    private readonly IServiceProvider _serviceProvider;
    private readonly UserServices _userService;
    private readonly ComicService _comicService;
    private readonly CartService _cartService;
    private readonly MainPageViewModel _mainPageViewModel;
    private readonly Perfil_Usuario _perfilUsuario;
    private readonly CarritoPage _carritoPage;

    private readonly DetalleMangaFactory _detalleMangaFactory;

    public MainPageFactory(IServiceProvider serviceProvider,UserServices userService ,CartService cartService, ComicService comicService, MainPageViewModel mainPageViewModel, Perfil_Usuario perfilUsuario, CarritoPage carritoPage ,DetalleMangaFactory detalleMangaFactory)
    {
            _serviceProvider = serviceProvider;
        _userService = userService;
        _cartService = cartService;
        _comicService = comicService;
        _mainPageViewModel = mainPageViewModel;
        _perfilUsuario = perfilUsuario;
        _carritoPage = carritoPage;
        _detalleMangaFactory = detalleMangaFactory;
    }

    public MainPage Create()
    {
            var perfilUsuario = _serviceProvider.GetService<Perfil_Usuario>();
            var carritoPage = _serviceProvider.GetService<CarritoPage>();
            var detalleManga = new DetalleMangaFactory(_serviceProvider, _cartService, _comicService, _userService);
            return new MainPage(_serviceProvider,_comicService, _userService , _detalleMangaFactory, _mainPageViewModel);
    }
}

}
