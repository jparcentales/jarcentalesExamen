namespace jarcentalesExamen.Views;

public partial class LoginPage : ContentPage
{
    private readonly string[,] credenciales =
    {
        { "estudiante", "moviles" },
        { "suda", "2025" }
    };

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnIniciarSesionClicked(object sender, EventArgs e)
    {
        string usuario = UsuarioEntry.Text?.Trim() ?? string.Empty;
        string contrasena = ContrasenaEntry.Text ?? string.Empty;

        bool credencialesValidas =
            (usuario == credenciales[0, 0] && contrasena == credenciales[0, 1]) ||
            (usuario == credenciales[1, 0] && contrasena == credenciales[1, 1]);

        if (!credencialesValidas)
        {
            await DisplayAlert("Error", "El usuario o la contraseña son incorrectos.", "Aceptar");
            return;
        }

        await DisplayAlert("Acceso correcto", "Bienvenido/a.", "Continuar");
        await Navigation.PushAsync(new RegistroPage(usuario));
    }
}
