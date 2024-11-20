using BackendlessAPI.Service;
using NicamicsApp.Service;
using NicamicsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp
{
    public class ComicEditFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ComicService _comicService;
        private readonly InventarioViewModel _inventarioViewModel;
        private readonly ComicEditViewModel _comicEditViewModel;
        public ComicEditFactory(IServiceProvider serviceProvider, InventarioViewModel inventarioViewModel, ComicEditViewModel comicEditViewModel, ComicService comicService)
        {
            _serviceProvider = serviceProvider;
            _comicService = comicService;
            _comicEditViewModel = comicEditViewModel;
            _inventarioViewModel = inventarioViewModel;
        }

        public ComicEditPage Create(string comicId)
        {
            return new ComicEditPage(_serviceProvider, _inventarioViewModel, _comicService, _comicEditViewModel,comicId);
        }

    }
}
