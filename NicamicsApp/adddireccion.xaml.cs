using NicamicsApp.ViewModels;
using NicamicsApp.Service;
using System.ComponentModel;

namespace NicamicsApp;

public partial class adddireccion : ContentPage
{
    private int cantidad = 1;
    IServiceProvider _serviceProvider;
    AddressViewModel _addresviewmodels;

    public adddireccion(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _addresviewmodels = _serviceProvider.GetService<AddressViewModel>();
        BindingContext = _addresviewmodels;
        _addresviewmodels?.cargardepartamentos();

        // Suscribirse al evento PropertyChanged
        _addresviewmodels.PropertyChanged += OnViewModelPropertyChanged;
    }

    private async void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AddressViewModel.Mensaje))
        {
            var viewModel = (AddressViewModel)BindingContext;
            if (!string.IsNullOrEmpty(viewModel.Mensaje))
            {
                if (viewModel.Mensaje != "Direccion guardada con exito")
                {
                    await DisplayAlert("Mensaje", viewModel.Mensaje, "OK");
                    viewModel.Mensaje = string.Empty;
                    return;
                }
                await DisplayAlert("Mensaje", viewModel.Mensaje, "OK");
                viewModel.Mensaje = string.Empty;
                    var _compradetalle = _serviceProvider.GetService<CompraDetalle>();
                    Navigation.InsertPageBefore(_compradetalle, this);
                    await Navigation.PopAsync();

            }
        }
    }
}
