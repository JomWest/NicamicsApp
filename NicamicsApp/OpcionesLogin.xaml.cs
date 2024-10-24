namespace NicamicsApp
{
    public partial class OpcionesLogin : ContentPage
    {
        public OpcionesLogin()
        {
            InitializeComponent();
        }

        private async void OnTappedLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
