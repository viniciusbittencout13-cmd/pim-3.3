using System.Windows;
using System.Windows.Controls;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Views.Pages;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Views.Pages.Usuarios
{
    private readonly JsonUserStore _userStore = new JsonUserStore();
    public partial class EditarTecnicoPage : UserControl
    {
        public EditarTecnicoPage()
        {
            InitializeComponent();
        }

        private void CarregarButton_Click(object sender, RoutedEventArgs e)
        {
            var cpf = BuscaCpfTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(cpf))
            {
                MessageBox.Show("Informe o CPF para buscar o técnico.",
                    "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var tecnico = UsuarioStorage.GetTecnicoPorCpf(cpf);
            if (tecnico == null)
            {
                MessageBox.Show("Técnico não encontrado.",
                    "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Preenche campos
            CpfTextBox.Text         = tecnico.Cpf;
            NomeTextBox.Text        = tecnico.NomeCompleto;
            TelefoneTextBox.Text    = tecnico.Telefone;
            UserNameTextBox.Text    = tecnico.NomeUsuario;
            SenhaPasswordBox.Password = tecnico.SenhaPrimeiroAcesso;
            EmailTextBox.Text       = tecnico.Email;
            CategoriaTextBox.Text   = tecnico.CategoriaChamados;

            // Seleciona nível
            NivelComboBox.SelectedIndex = -1;
            for (int i = 0; i < NivelComboBox.Items.Count; i++)
            {
                if (NivelComboBox.Items[i] is ComboBoxItem item &&
                    (item.Content?.ToString() ?? "") == tecnico.NivelTecnico)
                {
                    NivelComboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        private void SalvarButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CpfTextBox.Text))
            {
                MessageBox.Show("CPF não pode ficar vazio.",
                    "Campos obrigatórios", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var nivel = (NivelComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "1";

            var tecnico = new TecnicoInfo
            {
                Cpf                 = CpfTextBox.Text.Trim(),
                NomeCompleto        = NomeTextBox.Text.Trim(),
                Telefone            = TelefoneTextBox.Text.Trim(),
                NomeUsuario         = UserNameTextBox.Text.Trim(),
                SenhaPrimeiroAcesso = SenhaPasswordBox.Password,
                Email               = EmailTextBox.Text.Trim(),
                CategoriaChamados   = CategoriaTextBox.Text.Trim(),
                NivelTecnico        = nivel
            };

            UsuarioStorage.AddOrUpdateTecnico(tecnico);

            // depois de salvar o tecnico alterado
var usuario = _userStore.GetByUsername(tecnico.NomeUsuario);
if (usuario != null)
{
    usuario.NomeCompleto = tecnico.NomeCompleto;
    usuario.Nivel        = $"Nível {tecnico.NivelTecnico}";
    usuario.Categoria    = tecnico.CategoriaChamados;
    // se quiser permitir trocar senha aqui:
    if (!string.IsNullOrWhiteSpace(tecnico.SenhaPrimeiroAcesso))
        usuario.PasswordHash = JsonUserStore.HashPassword(tecnico.SenhaPrimeiroAcesso);

    _userStore.Update(usuario);
}

            MessageBox.Show("Dados do técnico atualizados com sucesso!",
                "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
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
