using System.Windows;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views
{
    public partial class FirstAccessWindow : Window
    {
        private readonly Usuario _usuario;
        private readonly Auth _auth = new Auth();

        public FirstAccessWindow(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            UsernameLabel.Content = _usuario.NomeUsuario;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var novaSenha = NewPasswordBox.Password ?? "";
            var confirmar = ConfirmPasswordBox.Password ?? "";
            var frase = SecurityPhraseTextBox.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(novaSenha) ||
                string.IsNullOrWhiteSpace(confirmar) ||
                string.IsNullOrWhiteSpace(frase))
            {
                MessageBox.Show("Preencha todos os campos.", "Atenção",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (novaSenha != confirmar)
            {
                MessageBox.Show("As senhas não conferem.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!_auth.AtualizarPrimeiroAcesso(_usuario, novaSenha, frase))
            {
                MessageBox.Show("Erro ao atualizar dados do usuário.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Senha cadastrada com sucesso!", "Sucesso",
                MessageBoxButton.OK, MessageBoxImage.Information);

            var main = new MainWindow(_usuario);
            main.Show();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            Close();
        }
    }
}
