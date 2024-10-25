namespace NicamicsApp
{
    public partial class OpcionesLogin : ContentPage
    {
        IServiceProvider _serviceProvider;
        public OpcionesLogin(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private async void OnTappedLogin(object sender, EventArgs e)
        {
            var loginPage = _serviceProvider.GetService<LoginPage>();
            await Navigation.PushAsync(loginPage);
        }
    }
}
