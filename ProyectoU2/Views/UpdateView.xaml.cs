using Microsoft.Maui.Controls;
using ProyectoU2.Entities;

namespace ProyectoU2.Views;

public partial class UpdateView : ContentPage
{
    public UpdateView()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            var response = await App.SupabaseClient
                .From<Usuario>()
                .Get();

            if (response.Models != null && response.Models.Count > 0)
                UsuariosCollectionView.ItemsSource = response.Models;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar los usuarios: {ex.Message}", "OK");
        }
    }

    private async void OnEditarClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var usuario = button?.CommandParameter as Usuario;

        if (usuario == null)
            return;

        // Abrimos un modal con los datos
        await Navigation.PushModalAsync(new EditarUsuarioModal(usuario));
    }
}
