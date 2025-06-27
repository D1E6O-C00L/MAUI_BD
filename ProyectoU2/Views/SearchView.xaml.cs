using Supabase;
using ProyectoU2.Entities;

namespace ProyectoU2.Views;

public partial class SearchView : ContentPage
{
    public SearchView()
    {
        InitializeComponent();
    }

    private async void OnBuscarClicked(object sender, EventArgs e)
    {
        string termino = BusquedaEntry.Text?.Trim().ToLower();

        if (string.IsNullOrWhiteSpace(termino))
        {
            await DisplayAlert("Campo vacío", "Ingresa un nombre, correo o teléfono.", "OK");
            return;
        }

        try
        {
            var response = await App.SupabaseClient
                .From<Usuario>()
                .Get();

            var resultados = response.Models
                .Where(u =>
                    (!string.IsNullOrEmpty(u.nombre) && u.nombre.ToLower().Contains(termino)) ||
                    (!string.IsNullOrEmpty(u.correo) && u.correo.ToLower().Contains(termino)) ||
                    (!string.IsNullOrEmpty(u.telefono) && u.telefono.ToLower().Contains(termino)))
                .ToList();

            if (resultados.Any())
            {
                ResultadoCollectionView.ItemsSource = resultados;
            }
            else
            {
                await DisplayAlert("Sin resultados", "No se encontró ningún usuario.", "OK");
                ResultadoCollectionView.ItemsSource = null;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al buscar: {ex.Message}", "OK");
        }
    }
}
