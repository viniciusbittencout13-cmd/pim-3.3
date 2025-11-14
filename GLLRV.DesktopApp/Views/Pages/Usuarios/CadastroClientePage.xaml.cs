using System.Windows;
using System.Windows.Controls;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views.Pages.Usuarios
{
    public partial class CadastroClientePage : UserControl
    {
        public CadastroClientePage()
        {
            InitializeComponent();
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            CpfTextBox.Text          = string.Empty;
            NomeTextBox.Text         = string.Empty;
            TelefoneTextBox.Text     = string.Empty;
            FuncaoTextBox.Text       = string.Empty;
            NomeUsuarioTextBox.Text = string.Empty;
            SenhaPasswordBox.Password = string.Empty;
            EmailTextBox.Text        = string.Empty;
        }

        private void CadastrarButton_Click(object sender, RoutedEventArgs e)
        {
            var cpf = CpfTextBox.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(cpf))
            {
                MessageBox.Show("Informe o CPF do cliente.",
                    "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var cliente = new UsuarioStorage.ClienteInfo
            {
                Cpf                = cpf,
                NomeCompleto       = NomeTextBox.Text?.Trim()              ?? string.Empty,
                Telefone           = TelefoneTextBox.Text?.Trim()          ?? string.Empty,
                Funcao             = FuncaoTextBox.Text?.Trim()            ?? string.Empty,
                NomeUsuario        = NomeUsuarioTextBox.Text?.Trim()       ?? string.Empty,
                SenhaPrimeiroAcesso = SenhaPasswordBox.Password            ?? string.Empty,
                Email              = EmailTextBox.Text?.Trim()             ?? string.Empty
            };

            UsuarioStorage.AddOrUpdateCliente(cliente);

            MessageBox.Show("Cliente cadastrado com sucesso.",
                "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            CancelarButton_Click(sender, e);
        }
    }
}
