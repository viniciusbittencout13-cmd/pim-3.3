using System.Windows;
using System.Windows.Controls;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Views.Pages;

namespace GLLRV.DesktopApp.Views.Pages.Usuarios
{
    public partial class EditarClientePage : UserControl
    {
        public EditarClientePage()
        {
            InitializeComponent();
        }

        private void CarregarButton_Click(object sender, RoutedEventArgs e)
        {
            var cpf = BuscaCpfTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(cpf))
            {
                MessageBox.Show("Informe o CPF para buscar o cliente.",
                    "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var cliente = UsuarioStorage.GetClientePorCpf(cpf);
            if (cliente == null)
            {
                MessageBox.Show("Cliente não encontrado.",
                    "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CpfTextBox.Text              = cliente.Cpf;
            NomeTextBox.Text             = cliente.NomeCompleto;
            TelefoneTextBox.Text         = cliente.Telefone;
            FuncaoTextBox.Text           = cliente.Funcao;
            UserNameTextBox.Text         = cliente.NomeUsuario;
            SenhaPasswordBox.Password    = cliente.SenhaPrimeiroAcesso;
            EmailTextBox.Text            = cliente.Email;
        }

        private void SalvarButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CpfTextBox.Text))
            {
                MessageBox.Show("CPF não pode ficar vazio.",
                    "Campos obrigatórios", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var cliente = new ClienteInfo
            {
                Cpf                 = CpfTextBox.Text.Trim(),
                NomeCompleto        = NomeTextBox.Text.Trim(),
                Telefone            = TelefoneTextBox.Text.Trim(),
                Funcao              = FuncaoTextBox.Text.Trim(),
                NomeUsuario         = UserNameTextBox.Text.Trim(),
                SenhaPrimeiroAcesso = SenhaPasswordBox.Password,
                Email               = EmailTextBox.Text.Trim()
            };

            UsuarioStorage.AddOrUpdateCliente(cliente);

            MessageBox.Show("Dados do cliente atualizados com sucesso!",
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
