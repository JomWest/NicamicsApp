using NicamicsApp.Service;
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

        public MainPageFactory(IServiceProvider serviceProvider, UserServices userService, ComicService comicService)
        {
            _serviceProvider = serviceProvider;
            _userService = userService;
            _comicService = comicService;
        }

        public MainPage Create()
        {
            var perfilUsuario = _serviceProvider.GetService<Perfil_Usuario>();
            var carritoPage = _serviceProvider.GetService<CarritoPage>();
            var detalleManga = new DetalleMangaFactory(_serviceProvider, _comicService, _userService);

            return new MainPage(_comicService, _userService, perfilUsuario, carritoPage, detalleManga);
        }
    }
}
