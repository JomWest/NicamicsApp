using System.Net.Mail;
using System.Net;
using NicamicsApp.Service;
using NicamicsApp.Models;
using Weborb.Client;

namespace NicamicsApp;

public partial class RestablecerContraseña : ContentPage
{

    private const string SmtpServer = "smtp.gmail.com";
    private const int SmtpPort = 587;
    private const string SenderEmail = "juan132y@gmail.com";
    private const string AppPassword = "kuzisugjaadiiexk";

    private readonly UserServices _services;
    public RestablecerContraseña(UserServices userServices)
	{
		InitializeComponent();
        _services = userServices;

    }

    private async Task<bool> EnviarCorreoDeRestablecimientoAsync(string email, string resetCode, User user)
    {
        try
        {
            var client = new SmtpClient(SmtpServer, SmtpPort)
            {
                Credentials = new NetworkCredential(SenderEmail, AppPassword),
                EnableSsl = true
            };

            string mensaje = $"Utiliza el siguiente código para restablecer tu contraseña: {resetCode}";

            var mailMessage = new MailMessage
            {
                From = new MailAddress(SenderEmail),
                Subject = "Solicitud de restablecimiento de contraseña",
                Body = mensaje
            };
            mailMessage.To.Add(email);

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(resetCode);
            user.contraseña = hashedPassword;

            bool exito = await _services.ActualizarUsuario(user);

            if (exito)
            {
                await client.SendMailAsync(mailMessage);
                return true;
            }
            return false;

            
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        // Validar si el campo de correo electrónico está vacío
        if (string.IsNullOrWhiteSpace(EmailEntry.Text))
        {
            await DisplayAlert("Mensaje","Por favor ingresa tu correo electrónico.","OK");
            return;
        }

        string email = EmailEntry.Text.Trim().ToLower();

        try
        {
            var response = await _services.ObtenerUsuarioPorCorreo(email);

            if (response == null)
            {
                await DisplayAlert("Mensaje", "No se encontró ningún usuario con el correo ingresado", "OK");
                return;
            }

            // Generar un código de restablecimiento temporal o enlace de restablecimiento
            string resetCode = Guid.NewGuid().ToString().Replace("-", "");

            

            // Llamada a la función para enviar el correo de restablecimiento
            bool exito = await EnviarCorreoDeRestablecimientoAsync(EmailEntry.Text, resetCode, response);


            if (exito)
            {
               await DisplayAlert("Mensaje", "Solicitud enviada correctamente. Revisa tu correo.", "OK");
            }
            else
            {
                await DisplayAlert("Mensaje", "Hubo un problema al enviar la solicitud. Inténtalo nuevamente.","OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Mensaje",$"Error al procesar la solicitud: {ex.Message}", "OK");
        }
    }
}