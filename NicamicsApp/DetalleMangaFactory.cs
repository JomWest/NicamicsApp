using NicamicsApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp
{
    public class DetalleMangaFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ComicService _comicService;
        private readonly UserServices _userServices;
        private readonly CartService _cartService;

        public DetalleMangaFactory(IServiceProvider serviceProvider, CartService cartService,ComicService comicService, UserServices userServices)
        {
            _serviceProvider = serviceProvider;
            _comicService = comicService;
            _userServices = userServices;
            _cartService = cartService;
        }

        public DetalleManga Create(string comicId)
        {
            return new DetalleManga(_serviceProvider, _comicService, _userServices, _cartService, comicId);
        }
    }
}
