using System.Windows;
using System.Windows.Controls;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Views.Pages;

namespace GLLRV.DesktopApp.Views.Pages.Usuarios
{
    public partial class EditarClientePage : UserControl
    {
        // <- PASSO 3: campo para guardar o cliente carregado
        private UsuarioStorage.ClienteInfo? _clienteAtual;

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

            // nome correto do método no UsuarioStorage: ObterClientePorCpf
            var cliente = UsuarioStorage.ObterClientePorCpf(cpf);
            if (cliente == null)
            {
                MessageBox.Show("Cliente não encontrado.",
                    "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _clienteAtual = cliente; // guarda para usar no Salvar

            CpfTextBox.Text           = cliente.Cpf;
            NomeTextBox.Text          = cliente.NomeCompleto;
            TelefoneTextBox.Text      = cliente.Telefone;
            FuncaoTextBox.Text        = cliente.Funcao;
            UserNameTextBox.Text      = cliente.NomeUsuario;
            SenhaPasswordBox.Password = cliente.SenhaPrimeiroAcesso;
            EmailTextBox.Text         = cliente.Email;
        }

        private void SalvarButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CpfTextBox.Text))
            {
                MessageBox.Show("CPF não pode ficar vazio.",
                    "Campos obrigatórios", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // se não tiver carregado ninguém, cria um novo
            var cliente = _clienteAtual ?? new UsuarioStorage.ClienteInfo();

            cliente.Cpf                 = CpfTextBox.Text.Trim();
            cliente.NomeCompleto        = NomeTextBox.Text.Trim();
            cliente.Telefone            = TelefoneTextBox.Text.Trim();
            cliente.Funcao              = FuncaoTextBox.Text.Trim();
            cliente.NomeUsuario         = UserNameTextBox.Text.Trim();
            cliente.SenhaPrimeiroAcesso = SenhaPasswordBox.Password;
            cliente.Email               = EmailTextBox.Text.Trim();

            UsuarioStorage.AddOrUpdateCliente(cliente);
            _clienteAtual = cliente;

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
