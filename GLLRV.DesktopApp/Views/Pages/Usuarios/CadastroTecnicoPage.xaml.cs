using System.Windows;
using System.Windows.Controls;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Views.Pages;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Views.Pages.Usuarios
{
    public partial class CadastroTecnicoPage : UserControl
{
    private readonly UsuarioStorage _storage = new UsuarioStorage();
    private readonly JsonUserStore _userStore = new JsonUserStore(); // ⬅ NOVO

    public CadastroTecnicoPage()
    {
        InitializeComponent();
    }

    private void CadastrarButton_Click(object sender, RoutedEventArgs e)
    {
        // 1) Monta o técnico (como você já fazia)
        var tecnico = new TecnicoInfo
        {
            Cpf               = CpfTextBox.Text.Trim(),
            NomeCompleto      = NomeTextBox.Text.Trim(),
            Telefone          = TelefoneTextBox.Text.Trim(),
            NivelTecnico      = NivelComboBox.Text,      // "1", "2", "3" etc.
            NomeUsuario       = NomeUsuarioTextBox.Text.Trim(),
            SenhaPrimeiroAcesso = SenhaPasswordBox.Password,
            Email             = EmailTextBox.Text.Trim(),
            CategoriaChamados = CategoriaComboBox.Text
        };

        // salva no tecnicos.json (código que você já tinha)
        _storage.SalvarOuAtualizarTecnico(tecnico);

        // 2) Criar também o USUÁRIO DE LOGIN em usuarios.json
        var usuario = new Usuario
        {
            Username       = tecnico.NomeUsuario,
            NomeUsuario    = tecnico.NomeUsuario,   // se existir essa prop.
            NomeCompleto   = tecnico.NomeCompleto,
            Nivel          = $"Nível {tecnico.NivelTecnico}",
            Categoria      = tecnico.CategoriaChamados,
            PasswordHash   = JsonUserStore.HashPassword(tecnico.SenhaPrimeiroAcesso),
            PrimeiroAcesso = true,     // vai cair na tela de primeiro acesso depois
            Ativo          = true,
            FraseSeguranca = "",       // pode pedir isso depois numa tela própria
            Tipo           = "Tecnico" // se você tiver essa prop no modelo
        };

        _userStore.Update(usuario);

        MessageBox.Show("Técnico cadastrado com sucesso!", "Sucesso",
            MessageBoxButton.OK, MessageBoxImage.Information);

        // aqui você limpa os campos se quiser
    }
}

        private void LimparCampos()
        {
            NomeTextBox.Text = "";
            TelefoneTextBox.Text = "";
            CpfTextBox.Text = "";
            UserNameTextBox.Text = "";
            SenhaPasswordBox.Password = "";
            EmailTextBox.Text = "";
            CategoriaTextBox.Text = "";
            NivelComboBox.SelectedIndex = -1;
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow main)
            {
                main.MainContentFrame.Navigate(new UsuariosPage());
            }
        }
    }
}
