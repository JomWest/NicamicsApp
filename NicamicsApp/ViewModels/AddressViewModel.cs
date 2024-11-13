
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NicamicsApp.Models;
using NicamicsApp.Service;
using SocketIOClient.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace NicamicsApp.ViewModels
{
    public partial class AddressViewModel : ObservableObject
    {
        private readonly AddressService _addressService;
        private readonly string _userId;
        private readonly INavigation _navigation;
        public ObservableCollection<string> Departamento { get; set; }

        public AddressViewModel(AddressService addressService)
        {
            _addressService = addressService;
            LoadAddresses();
            cargardepartamentos();


        }

        [ObservableProperty]
        private ObservableCollection<Address> addresses = new ObservableCollection<Address>();


        [ObservableProperty]
        private string _addressName = "";

        [ObservableProperty]
        private string _city = "";

        [ObservableProperty]
        private string _state = "";

        [ObservableProperty]
        private string _numero ;

        [ObservableProperty]
        private string _nombre = "";

        [ObservableProperty]
        private string _departamentoselect;

        [ObservableProperty]
        private string mensaje = "";


        [ObservableProperty]
        private string _errorMessage = "";


        public async void cargardepartamentos()
        {
            Departamento = new ObservableCollection<string>
            {
               "Boaco", "Carazo", "Chinandega", "Chontales", "Estelí",
            "Granada", "Jinotega", "León", "Madriz", "Managua",
            "Masaya", "Matagalpa", "Nueva Segovia", "Rivas", "Río San Juan",
            "Región Autónoma de la Costa Caribe Norte", "Región Autónoma de la Costa Caribe Sur"
            };
        }

        public async void LoadAddresses()
        {
            try
            {
                var response = await _addressService.ObtenerDireccionesUsuario(_userId,IpAddress.token);

                if (response != null)
                {
                    Nombre = response[0].Nombre;
                    AddressName = response[0].Street;
                    City = response[0].City;
                    State = response[0].Departamento;
                    Numero = response[0].Numero.ToString();
                    Departamentoselect = Departamento[0];
                    
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        [RelayCommand]
        public async Task<string> CrearAddres()
        {
            try
            {
                Address address = new Address
                {
                    Id = "",
                    UserId = IpAddress.userId,
                    Nombre = Nombre,
                    Street = AddressName,
                    City = City,
                    Departamento = Departamentoselect,
                    Numero = Convert.ToInt32(Numero),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                var response = await _addressService.CrearDireccion(address, IpAddress.token);

                if (response.Contains("Id"))
                {
                    Mensaje = "Direccion guardada con exito";
                    await _navigation.PopAsync();
                }
                else
                {
                    Mensaje = response;
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return ex.Message;
            }
        }



        [RelayCommand]
        public async Task DeleteAddress(string addressId)
        {
            try
            {
                var response = await _addressService.EliminarDireccion(addressId,IpAddress.token);

                if (response != null)
                {
                    LoadAddresses(); 
                }
                else
                {
                    Console.WriteLine("Error al eliminar la dirección.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

  
    }
}
