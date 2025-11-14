using System.Windows;
using System.Windows.Controls;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views.Pages.Configuracao
{
    public partial class ConfiguracoesPage : UserControl
    {
        private readonly Usuario _usuario;

        public ConfiguracaoPage(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            // Preenche os campos
            NomeUsuarioTextBox.Text = _usuario.NomeUsuario;
            TelefoneTextBox.Text = _usuario.Telefone;
            EmailTextBox.Text = _usuario.Email;
        }

        private void CadastrarButton_Click(object sender, RoutedEventArgs e)
        {
            // --- ATUALIZAÇÃO DA SENHA ---
            if (!string.IsNullOrWhiteSpace(SenhaAntigaPasswordBox.Password))
            {
                string hashAntigo = JsonUserStore.HashPassword(SenhaAntigaPasswordBox.Password);

                if (hashAntigo != _usuario.PasswordHash)
                {
                    MessageBox.Show("Senha antiga incorreta.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (NovaSenhaPasswordBox.Password != RepitaSenhaPasswordBox.Password)
                {
                    MessageBox.Show("As senhas não coincidem.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _usuario.PasswordHash = JsonUserStore.HashPassword(NovaSenhaPasswordBox.Password);
            }

            // --- ATUALIZAÇÃO DA FRASE DE SEGURANÇA ---
            if (!string.IsNullOrWhiteSpace(FraseSegurancaTextBox.Text))
            {
                _usuario.FraseSeguranca = JsonUserStore.HashPassword(FraseSegurancaTextBox.Text);
            }

            // --- EMAIL E TELEFONE ---
            _usuario.Telefone = TelefoneTextBox.Text.Trim();
            _usuario.Email = EmailTextBox.Text.Trim();

            // FOTO será feita depois

            // --- SALVA AS ALTERAÇÕES ---
            var store = new JsonUserStore();
            store.Update(_usuario);

            MessageBox.Show("Configurações atualizadas com sucesso!",
                "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            TelefoneTextBox.Text = _usuario.Telefone;
            EmailTextBox.Text = _usuario.Email;
            SenhaAntigaPasswordBox.Password = "";
            NovaSenhaPasswordBox.Password = "";
            RepitaSenhaPasswordBox.Password = "";
            FraseSegurancaTextBox.Text = "";
        }
    }
}
