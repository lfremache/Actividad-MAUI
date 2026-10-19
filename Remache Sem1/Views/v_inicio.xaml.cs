using System;
using Microsoft.Maui.Controls;

namespace Remache_Sem1.Views
{
    public partial class v_inicio : ContentPage
    {
        public v_inicio()
        {
            InitializeComponent();
        }

        private async void OnCalcularClicked(object sender, EventArgs e)
        {
            // 1. Obtener los valores de los Entry
            string nombre = txtNombre.Text?.Trim();
            string apellido = txtApellido.Text?.Trim();
            string edadStr = txtEdad.Text?.Trim();
            string salarioStr = txtSalario.Text?.Trim();

            // 2. Validación obligatoria: Campos vacíos
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) ||
                string.IsNullOrEmpty(edadStr) || string.IsNullOrEmpty(salarioStr))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos de información.", "OK");
                return;
            }

            // 3. Validación obligatoria: Edad válida
            if (!int.TryParse(edadStr, out int edad) || edad <= 0)
            {
                await DisplayAlert("Error de Formato", "Ingrese una edad numérica válida.", "OK");
                return;
            }

            // 4. Validación obligatoria: Salario válido
            if (!decimal.TryParse(salarioStr, out decimal salario) || salario <= 0)
            {
                await DisplayAlert("Error de Formato", "Ingrese un salario numérico válido.", "OK");
                return;
            }

            // 5. Cálculo del Aporte al IESS (9.45%)
            decimal aporte = salario * 0.0945m;

            // 6. Formato de la alerta con saltos de línea (\n)
            string mensaje = $"Bienvenido \"{nombre} {apellido}\"\n" +
                             $"Tienes \"{edad}\" años\n" +
                             $"Tu aporte mensual es \"${aporte:F2}\"";

            // 7. Mostrar la alerta requerida
            await DisplayAlert("Resultado del Cálculo", mensaje, "Aceptar");
        }
    }
}
