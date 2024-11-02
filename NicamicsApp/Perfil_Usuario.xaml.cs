namespace NicamicsApp;

public partial class Perfil_Usuario : ContentPage
{
    IServiceProvider _serviceProvider;
    public Perfil_Usuario(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
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
