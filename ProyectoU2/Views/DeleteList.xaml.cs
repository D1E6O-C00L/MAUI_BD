using Microsoft.Maui.Controls;
using ProyectoU2.Entities;  // Importamos la clase Usuario
using System.Collections.Generic;
using System.Linq;

namespace ProyectoU2.Views
{
    public partial class DeleteList : ContentPage
    {
        public DeleteList()
        {
            InitializeComponent();
            CargarUsuariosDadoDeBaja();  // Llamamos a la función para cargar los usuarios dados de baja
        }

        private async void CargarUsuariosDadoDeBaja()
        {
            try
            {
                var response = await App.SupabaseClient
                    .From<Usuario>()
                    .Where(u => u.activo == false)
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

    }
}
