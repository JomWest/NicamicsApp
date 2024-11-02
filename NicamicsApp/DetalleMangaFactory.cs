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

        public DetalleMangaFactory(IServiceProvider serviceProvider, ComicService comicService)
        {
            _serviceProvider = serviceProvider;
            _comicService = comicService;
        }

        public DetalleManga Create(string comicId)
        {
            return new DetalleManga(_serviceProvider, _comicService, comicId);
        }
    }
}
