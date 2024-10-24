namespace NicamicsApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {

        }
        private void OnProfileImageTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Perfil_Usuario());
        }
        private async void OnCarritoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CarritoPage()); 
        }
        private async void CompraTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DetalleManga());
        }
    }

}
