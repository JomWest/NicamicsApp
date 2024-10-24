
using BackendlessAPI;
using BackendlessAPI.Exception;
using System;
using Microsoft.Maui.Controls;

namespace NicamicsApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();


        }

        async void IniciarSesionClicked(object sender, EventArgs e)
        {

                await Navigation.PushAsync(new MainPage());
        }
      
    }
}
