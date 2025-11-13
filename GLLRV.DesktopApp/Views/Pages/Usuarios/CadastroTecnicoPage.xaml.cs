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
        private readonly JsonUserStore _userStore = new JsonUserStore(); // <-- NOVO
        
        public CadastroTecnicoPage()
        {
            InitializeComponent();
        }

        private void CadastrarButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NomeTextBox.Text) ||
                string.IsNullOrWhiteSpace(CpfTextBox.Text) ||
                string.IsNullOrWhiteSpace(UserNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(SenhaPasswordBox.Password))
            {
                MessageBox.Show("Preencha pelo menos Nome, CPF, Usuário e Senha.",
                    "Campos obrigatórios", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var nivel = (NivelComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "1";

            var tecnico = new TecnicoInfo
            {
                NomeCompleto        = NomeTextBox.Text.Trim(),
                Cpf                 = CpfTextBox.Text.Trim(),
                Telefone            = TelefoneTextBox.Text.Trim(),
                NivelTecnico        = nivel,
                NomeUsuario         = UserNameTextBox.Text.Trim(),
                SenhaPrimeiroAcesso = SenhaPasswordBox.Password,
                Email               = EmailTextBox.Text.Trim(),
                CategoriaChamados   = CategoriaTextBox.Text.Trim()
            };

            UsuarioStorage.AddOrUpdateTecnico(tecnico);

            var usuario = new Usuario
            {
                Username       = tecnico.NomeUsuario,
                NomeUsuario    = tecnico.NomeUsuario,
                NomeCompleto   = tecnico.NomeCompleto,
                Nivel          = $"Nível {tecnico.NivelTecnico}",
                Categoria      = tecnico.CategoriaChamados,
                PasswordHash   = JsonUserStore.HashPassword(tecnico.SenhaPrimeiroAcesso),
                PrimeiroAcesso = true,
                Ativo          = true,
                FraseSeguranca = ""
            };

            _userStore.Update(usuario);

            MessageBox.Show("Técnico cadastrado com sucesso!",
                "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            LimparCampos();
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
