using System.Windows;
using System.Windows.Controls;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views.Pages.Configuracoes
{
    public partial class ConfiguracoesPage : UserControl
    {
        private readonly Usuario _usuario;

        public ConfiguracoesPage(Usuario usuarioLogado)
        {
            InitializeComponent();
            _usuario = usuarioLogado;

            // Preenche os campos com dados atuais
            NomeUsuarioTextBox.Text = _usuario.NomeUsuario;
            TelefoneTextBox.Text = _usuario.Telefone;
            EmailTextBox.Text = _usuario.Email;
        }

        private void CadastrarButton_Click(object sender, RoutedEventArgs e)
        {
            // SENHA
            if (!string.IsNullOrWhiteSpace(SenhaAntigaPasswordBox.Password))
            {
                // Verifica senha antiga
                if (!PasswordHasher.Verify(SenhaAntigaPasswordBox.Password, _usuario.PasswordHash, _usuario.PasswordSalt))
                {
                    MessageBox.Show("Senha antiga incorreta.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (NovaSenhaPasswordBox.Password != RepitaSenhaPasswordBox.Password)
                {
                    MessageBox.Show("A nova senha não coincide nos dois campos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Atualiza senha
                PasswordHasher.CreateHash(NovaSenhaPasswordBox.Password, out string hash, out string salt);
                _usuario.PasswordHash = hash;
                _usuario.PasswordSalt = salt;
            }

            // FRASE DE SEGURANÇA
            if (!string.IsNullOrWhiteSpace(FraseSegurancaTextBox.Text))
            {
                PasswordHasher.CreateHash(FraseSegurancaTextBox.Text, out string hash, out string salt);
                _usuario.FraseSegurancaHash = hash;
                _usuario.FraseSegurancaSalt = salt;
            }

            // TELEFONE
            _usuario.Telefone = TelefoneTextBox.Text.Trim();

            // EMAIL
            _usuario.Email = EmailTextBox.Text.Trim();

            // Foto não implementada ainda

            // Salva o usuario modificado
            UsuarioStorage.SalvarOuAtualizarTecnico(_usuario);

            MessageBox.Show("Configurações atualizadas com sucesso!",
                "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            // Opcional: apenas limpa
            TelefoneTextBox.Text = _usuario.Telefone;
            EmailTextBox.Text = _usuario.Email;

            SenhaAntigaPasswordBox.Password = "";
            NovaSenhaPasswordBox.Password = "";
            RepitaSenhaPasswordBox.Password = "";
            FraseSegurancaTextBox.Text = "";
        }
    }
}
