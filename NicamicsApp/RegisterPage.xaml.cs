using NicamicsApp.Service;

namespace NicamicsApp;

public partial class RegisterPage : ContentPage
{
    IServiceProvider _serviceProvider;
    AuthService _authService;
    public RegisterPage(IServiceProvider serviceProvider, AuthService authService)
	{
		InitializeComponent();
        _serviceProvider = serviceProvider;
        _authService = authService;
	}

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
    }

    private async void btnRegister_Clicked(object sender, EventArgs e)
    {
        string correo = EmailEntry.Text;

        string password = PasswordEntry.Text;

        string repeatPassword = RepeatPasswordEntry.Text;

        string username = UsernameEntry.Text;

        if(password != repeatPassword)
        {
            await DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
            return;
        }

        var response = await _authService.RegisterUser(username, password, correo);

        if (response.Contains("Success")) 
        {
            await DisplayAlert("Éxito", "Cuenta creada exitosamente", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", $"{response}", "OK");
        }
    }
}