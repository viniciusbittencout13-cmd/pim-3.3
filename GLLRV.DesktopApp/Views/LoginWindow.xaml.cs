using System.Windows;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views
{
    public partial class LoginWindow : Window
    {
        private readonly JsonUserStore _store = new JsonUserStore();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameTextBox.Text.Trim();
            var password = PasswordBox.Password;

            var user = Auth.Login(username, password, _store);
            if (user == null)
            {
                MessageBox.Show("Usuário ou senha incorretos.", "Erro",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user.PrimeiroAcesso)
            {
                var first = new FirstAccessWindow(user, _store);
                first.Show();
            }
            else
            {
                var main = new MainWindow(user);
                main.Show();
            }

            Close();
        }
    }
}
