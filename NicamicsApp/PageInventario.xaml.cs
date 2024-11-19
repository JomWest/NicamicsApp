using NicamicsApp.ViewModels;

namespace NicamicsApp;

public partial class PageInventario : ContentPage
{
    IServiceProvider _serviceProvider;
    InventarioViewModel _viewModel;
    public PageInventario(IServiceProvider serviceProvider,InventarioViewModel viewModel)
	{
		InitializeComponent();
        _serviceProvider = serviceProvider;
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadComicVendedor();
    }

    private async void clickedback(object sender,EventArgs e)
    {
        var _perfiluser = _serviceProvider.GetService<Perfil_Usuario>();
        Navigation.InsertPageBefore(_perfiluser, this);
        await Navigation.PopAsync();
    }

}