using System.Windows;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views
{
    public partial class LoginWindow : Window
    {
        private readonly Auth _auth = new Auth();
        private readonly JsonUserStore _store = new JsonUserStore();

        public LoginWindow()
{
    InitializeComponent();

    // Garante que exista pelo menos 1 usuário (vinicius/admin)
    JsonUserStore.EnsureSeedUser();
}

        private void EntrarButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text?.Trim() ?? "";
            var senha = PasswordBox.Password ?? "";

            var usuario = _auth.Autenticar(username, senha);

            if (usuario == null)
            {
                MessageBox.Show("Usuário não encontrado ou senha incorreta.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (usuario.PrimeiroAcesso)
            {
                var first = new FirstAccessWindow(usuario);
                first.Show();
                Close();
                return;
            }

            AbrirMain(usuario);
        }

        private void AbrirMain(Usuario usuario)
        {
            var main = new MainWindow(usuario);
            main.Show();
            Close();
        }

        private void EsqueciSenhaButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Informe o nome de usuário para recuperar a senha.",
                    "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var usuario = _store.GetByUsername(username);
            if (usuario == null)
            {
                MessageBox.Show("Usuário não encontrado.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show($"Frase de segurança:\n\n{usuario.FraseSeguranca}",
                "Lembrete de senha", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
