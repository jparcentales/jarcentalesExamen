using System.Globalization;

namespace jarcentalesExamen.Views;

public partial class ResumenPage : ContentPage
{
    public ResumenPage(
        string usuario,
        string nombre,
        string apellido,
        int edad,
        DateTime fecha,
        string ciudad,
        string pais,
        decimal montoInicial,
        decimal pagoMensual,
        decimal pagoTotal)
    {
        InitializeComponent();

        UsuarioConectadoLabel.Text = $"Usuario conectado: {usuario}";
        NombreLabel.Text = nombre;
        ApellidoLabel.Text = apellido;
        EdadLabel.Text = edad.ToString(CultureInfo.CurrentCulture);
        FechaLabel.Text = fecha.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
        CiudadLabel.Text = ciudad;
        PaisLabel.Text = pais;
        MontoInicialLabel.Text = montoInicial.ToString("C2", CultureInfo.CurrentCulture);
        PagoMensualLabel.Text = pagoMensual.ToString("C2", CultureInfo.CurrentCulture);
        PagoTotalLabel.Text = pagoTotal.ToString("C2", CultureInfo.CurrentCulture);
    }
}
