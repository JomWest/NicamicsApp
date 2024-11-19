using NicamicsApp.ViewModels;

namespace NicamicsApp;

public partial class ComicEditPage : ContentPage
{

	IServiceProvider _serviceProvider;
	InventarioViewModel _inventarioViewModel;
	public ComicEditPage(IServiceProvider serviceProvider, InventarioViewModel inventarioViewModel)
	{
		InitializeComponent();
		_serviceProvider = serviceProvider;
		_inventarioViewModel = inventarioViewModel;
	}

	private async void backtapped(object sender, EventArgs e)
	{
        var _perfiluser = _serviceProvider.GetService<PageInventario>();
        Navigation.InsertPageBefore(_perfiluser, this);
        await Navigation.PopAsync();
    }
}