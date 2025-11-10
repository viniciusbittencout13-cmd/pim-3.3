using System.Windows;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views
{
    public partial class FirstAccessWindow : Window
    {
        private readonly Usuario _user;
        private readonly JsonUserStore _store;

        public FirstAccessWindow(Usuario user, JsonUserStore store)
        {
            InitializeComponent();
            _user = user;
            _store = store;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var senha1 = NewPasswordBox.Password;
            var senha2 = ConfirmPasswordBox.Password;
            var frase = SecurityPhraseTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(senha1) ||
                string.IsNullOrWhiteSpace(senha2) ||
                string.IsNullOrWhiteSpace(frase))
            {
                MessageBox.Show("Preencha todos os campos.", "Atenção",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (senha1 != senha2)
            {
                MessageBox.Show("As senhas não conferem.", "Atenção",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _user.SenhaHash = Auth.Sha256Hex(senha1);
            _user.FraseSeguranca = frase;
            _user.PrimeiroAcesso = false;

            _store.UpdateUsuario(_user);
            Auth.AtualizarUsuario(_user, _store);

            MessageBox.Show("Senha atualizada com sucesso.", "Sucesso",
                MessageBoxButton.OK, MessageBoxImage.Information);

            var main = new MainWindow(_user);
            main.Show();
            Close();
        }
    }
}
