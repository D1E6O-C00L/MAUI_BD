using ProyectoU2.Entities;

namespace ProyectoU2.Views;

public partial class EditarUsuarioModal : ContentPage
{
    private Usuario _usuario;

    public EditarUsuarioModal(Usuario usuario)
    {
        InitializeComponent();
        _usuario = usuario;
        NombreEntry.Text = usuario.nombre;
        CorreoEntry.Text = usuario.correo;
        TelefonoEntry.Text = usuario.telefono;
        ActivoSwitch.IsToggled = usuario.activo;
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        _usuario.nombre = NombreEntry.Text.Trim();
        _usuario.correo = CorreoEntry.Text.Trim();
        _usuario.telefono = TelefonoEntry.Text.Trim();
        _usuario.activo = ActivoSwitch.IsToggled;

        try
        {
            var response = await App.SupabaseClient
                .From<Usuario>()
                .Update(_usuario);
                ;

            if (response.Models != null)
                await DisplayAlert("Éxito", "Usuario actualizado correctamente.", "OK");

            await Navigation.PopModalAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al actualizar: {ex.Message}", "OK");
        }
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
