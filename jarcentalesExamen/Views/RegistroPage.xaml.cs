using System.Globalization;

namespace jarcentalesExamen.Views;

public partial class RegistroPage : ContentPage
{
    private const decimal CostoTotalCurso = 1500m;
    private const decimal NumeroCuotas = 4m;
    private const decimal RecargoPorCuota = CostoTotalCurso * 0.04m;
    private readonly string usuarioConectado;
    private decimal montoInicial;
    private decimal pagoMensual;

    public RegistroPage(string usuario)
    {
        InitializeComponent();
        usuarioConectado = usuario;
        UsuarioConectadoLabel.Text = $"Usuario conectado: {usuarioConectado}";
        FechaPicker.Date = DateTime.Today;
    }

    private async void OnCalcularPagoClicked(object sender, EventArgs e)
    {
        if (!decimal.TryParse(MontoInicialEntry.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out montoInicial) ||
            montoInicial < 0 || montoInicial > CostoTotalCurso)
        {
            await DisplayAlert("Dato inválido", $"El monto inicial debe estar entre 0 y {CostoTotalCurso:C2}.", "Aceptar");
            PagoMensualEntry.Text = string.Empty;
            return;
        }

        decimal saldo = CostoTotalCurso - montoInicial;
        pagoMensual = saldo / NumeroCuotas + RecargoPorCuota;
        PagoMensualEntry.Text = pagoMensual.ToString("C2", CultureInfo.CurrentCulture);
    }

    private async void OnVerResumenClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NombreEntry.Text) ||
            string.IsNullOrWhiteSpace(ApellidoEntry.Text) ||
            string.IsNullOrWhiteSpace(EdadEntry.Text) ||
            PaisPicker.SelectedIndex < 0 ||
            CiudadPicker.SelectedIndex < 0 ||
            string.IsNullOrWhiteSpace(MontoInicialEntry.Text) ||
            string.IsNullOrWhiteSpace(PagoMensualEntry.Text))
        {
            await DisplayAlert("Campos incompletos", "Complete todos los campos y calcule el pago mensual.", "Aceptar");
            return;
        }

        if (!int.TryParse(EdadEntry.Text, out int edad) || edad <= 0)
        {
            await DisplayAlert("Dato inválido", "Ingrese una edad válida.", "Aceptar");
            return;
        }

        if (!decimal.TryParse(MontoInicialEntry.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out montoInicial) ||
            montoInicial < 0 || montoInicial > CostoTotalCurso)
        {
            await DisplayAlert("Dato inválido", "Ingrese un monto inicial válido.", "Aceptar");
            return;
        }

        if (pagoMensual <= 0)
        {
            await DisplayAlert("Pago pendiente", "Calcule el pago mensual antes de ver el resumen.", "Aceptar");
            return;
        }

        decimal pagoTotal = montoInicial + pagoMensual * NumeroCuotas;

        await Navigation.PushAsync(new ResumenPage(
            usuarioConectado,
            NombreEntry.Text.Trim(),
            ApellidoEntry.Text.Trim(),
            edad,
            FechaPicker.Date ?? DateTime.Today,
            CiudadPicker.SelectedItem.ToString()!,
            PaisPicker.SelectedItem.ToString()!,
            montoInicial,
            pagoMensual,
            pagoTotal));
    }
}
