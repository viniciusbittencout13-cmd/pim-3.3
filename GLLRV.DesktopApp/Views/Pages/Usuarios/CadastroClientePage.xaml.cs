using System.Windows;
using System.Windows.Controls;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Views.Pages;

namespace GLLRV.DesktopApp.Views.Pages.Usuarios
{
    public partial class CadastroClientePage : UserControl
    {
        public CadastroClientePage()
        {
            InitializeComponent();
        }

        private void CadastrarButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NomeTextBox.Text) ||
                string.IsNullOrWhiteSpace(CpfTextBox.Text))
            {
                MessageBox.Show("Preencha pelo menos Nome e CPF.",
                    "Campos obrigatórios", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var cliente = new ClienteInfo
            {
                NomeCompleto        = NomeTextBox.Text.Trim(),
                Cpf                 = CpfTextBox.Text.Trim(),
                Telefone            = TelefoneTextBox.Text.Trim(),
                Funcao              = FuncaoTextBox.Text.Trim(),
                NomeUsuario         = UserNameTextBox.Text.Trim(),
                SenhaPrimeiroAcesso = SenhaPasswordBox.Password,
                Email               = EmailTextBox.Text.Trim()
            };

            UsuarioStorage.AddOrUpdateCliente(cliente);

            MessageBox.Show("Cliente cadastrado com sucesso!",
                "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            LimparCampos();
        }

        private void LimparCampos()
        {
            NomeTextBox.Text = "";
            TelefoneTextBox.Text = "";
            CpfTextBox.Text = "";
            FuncaoTextBox.Text = "";
            UserNameTextBox.Text = "";
            SenhaPasswordBox.Password = "";
            EmailTextBox.Text = "";
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
