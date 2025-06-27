using System;
using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;
using ProyectoU2.Entities;
using System.Linq; // Para Any()

namespace ProyectoU2.Views
{
    public partial class AddView : ContentPage
    {
        public AddView()
        {
            InitializeComponent();
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(CorreoEntry.Text) ||
                !Regex.IsMatch(CorreoEntry.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                DisplayAlert("Error", "Ingresa un correo válido.", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(TelefonoEntry.Text) ||
                TelefonoEntry.Text.Length != 10 ||
                !TelefonoEntry.Text.All(char.IsDigit))
            {
                DisplayAlert("Error", "El teléfono debe tener 10 dígitos.", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(NombreEntry.Text))
            {
                DisplayAlert("Error", "El nombre no puede estar vacío.", "OK");
                return false;
            }

            return true;
        }

        private async void OnAgregarClicked(object sender, EventArgs e)
        {
            if (!ValidarFormulario()) return;

            var nuevoUsuario = new Usuario
            {
                nombre = NombreEntry.Text.Trim(),
                correo = CorreoEntry.Text.Trim(),
                telefono = TelefonoEntry.Text.Trim(),
                activo = true,
                fecha_creacion = DateTime.UtcNow
            };

            try
            {
                var response = await App.SupabaseClient
                                        .From<Usuario>()
                                        .Insert(nuevoUsuario);

                if (response.Models is { Count: > 0 })
                {
                    await DisplayAlert("Éxito", "Usuario agregado exitosamente.", "OK");
                    LimpiarCampos();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo agregar el usuario.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Hubo un error: {ex.Message}", "OK");
            }
        }

        private void LimpiarCampos()
        {
            NombreEntry.Text = string.Empty;
            CorreoEntry.Text = string.Empty;
            TelefonoEntry.Text = string.Empty;
        }

        // Este método es mejor moverlo a DeleteList.xaml.cs
        private async void CargarUsuariosDadoDeBaja()
        {
            try
            {
                var response = await App.SupabaseClient
                    .From<Usuario>()
                    .Where(u => u.activo == true)
                    .Get();

                if (response.Models != null && response.Models.Count > 0)
                {
                    BajasCollectionView.ItemsSource = response.Models;
                }
                else
                {
                    await DisplayAlert("Sin usuarios", "No hay usuarios dados de baja.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Hubo un error: {ex.Message}", "OK");
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarUsuariosDadoDeBaja();
        }

    }
}
