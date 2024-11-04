using BackendlessAPI;
using NicamicsApp.Reportes;

namespace NicamicsApp
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            var loginPage = serviceProvider.GetService<LoginPage>();
            var comicPage = serviceProvider.GetService<AddComicPage>();
            var reportePage = serviceProvider.GetService<ReporteComicsMasVendidos>();

            MainPage = new NavigationPage(loginPage);
        }
    }
}
