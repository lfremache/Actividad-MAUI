using System.Text.RegularExpressions;

namespace Remache_Sem1.Views;

public partial class v_inicio : ContentPage
{
    public v_inicio()
    {
        InitializeComponent();
    }

    private async void OnValidarYGuardarClicked(object sender, EventArgs e)
    {
        string numeroId = txtNumeroId.Text?.Trim() ?? "";
        string paterno = txtApellidoPaterno.Text?.Trim() ?? "";
        string materno = txtApellidoMaterno.Text?.Trim() ?? "";
        string nombres = txtNombres.Text?.Trim() ?? "";
        string telefono = txtTelefono.Text?.Trim() ?? "";
        string correo = txtCorreo.Text?.Trim() ?? "";
        string confirmarCorreo = txtConfirmarCorreo.Text?.Trim() ?? "";

        // Verificación de campos obligatorios
        if (string.IsNullOrWhiteSpace(numeroId) || string.IsNullOrWhiteSpace(paterno) ||
            string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(correo) ||
            string.IsNullOrWhiteSpace(confirmarCorreo))
        {
            await DisplayAlert("Atención", "Por favor llene los campos requeridos del formulario.", "OK");
            return;
        }

        // VALIDACIÓN 1: Solo Números (Cédula y Teléfono)
        if (!Regex.IsMatch(numeroId, @"^[0-9]+$"))
        {
            await DisplayAlert("Error", "La identificación debe contener solo números.", "OK");
            return;
        }

        if (!string.IsNullOrEmpty(telefono) && !Regex.IsMatch(telefono, @"^[0-9]+$"))
        {
            await DisplayAlert("Error", "El teléfono debe contener solo números.", "OK");
            return;
        }

        // VALIDACIÓN 2: Solo Letras (Nombres y Apellidos)
        string textoNombresCompleto = $"{paterno} {materno} {nombres}";
        if (!Regex.IsMatch(textoNombresCompleto, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
        {
            await DisplayAlert("Error", "Los nombres y apellidos deben contener únicamente letras.", "OK");
            return;
        }

        // VALIDACIÓN 3: Coincidir los correos
        if (!correo.Equals(confirmarCorreo, StringComparison.OrdinalIgnoreCase))
        {
            await DisplayAlert("Error", "Los correos electrónicos ingresados no coinciden.", "OK");
            return;
        }

        // VALIDACIÓN 4: Enviar todo a un archivo TXT
        try
        {
            string carrera = pckCarrera.SelectedItem?.ToString() ?? "No seleccionada";
            string modalidad = pckModalidad.SelectedItem?.ToString() ?? "No seleccionada";
            string campus = pckCampus.SelectedItem?.ToString() ?? "No seleccionado";

            string contenidoTxt = $"========================================\n" +
                                 $"      REGISTRO DE INSCRIPCIÓN           \n" +
                                 $"========================================\n" +
                                 $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n\n" +
                                 $"[DATOS PERSONALES]\n" +
                                 $"Identificación ({pckTipoId.SelectedItem ?? "CÉDULA"}): {numeroId}\n" +
                                 $"Apellidos: {paterno} {materno}\n" +
                                 $"Nombres: {nombres}\n" +
                                 $"Teléfono: {telefono}\n" +
                                 $"Correo Confirmado: {correo}\n\n" +
                                 $"[INFORMACIÓN ACADÉMICA]\n" +
                                 $"Carrera: {carrera}\n" +
                                 $"Modalidad: {modalidad}\n" +
                                 $"Campus: {campus}\n" +
                                 $"========================================\n";

            string ruta = Path.Combine(FileSystem.AppDataDirectory, "Inscripcion_Registro.txt");
            await File.WriteAllTextAsync(ruta, contenidoTxt);

            lblEstado.Text = $"✓ Archivo TXT generado en: {ruta}";
            await DisplayAlert("Éxito", "Inscripción registrada correctamente y guardada en el archivo TXT.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo crear el archivo TXT: {ex.Message}", "OK");
        }
    }
}