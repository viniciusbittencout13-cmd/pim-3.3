using System.Windows;
using System.Windows.Controls;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views.Pages.Usuarios
{
    public partial class CadastroTecnicoPage : UserControl
    {
        private readonly JsonUserStore _userStore = new JsonUserStore();

        public CadastroTecnicoPage()
        {
            InitializeComponent();
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            CpfTextBox.Text                 = string.Empty;
            NomeTextBox.Text                = string.Empty;
            TelefoneTextBox.Text            = string.Empty;
            NomeUsuarioCadastroTextBox.Text = string.Empty;
            SenhaPasswordBox.Password       = string.Empty;
            EmailTextBox.Text               = string.Empty;
            CategoriaTextBox.Text           = string.Empty;
            NivelComboBox.SelectedIndex     = -1;
        }

        private void CadastrarButton_Click(object sender, RoutedEventArgs e)
        {
            var cpf        = CpfTextBox.Text?.Trim()                 ?? string.Empty;
            var nome       = NomeTextBox.Text?.Trim()                ?? string.Empty;
            var telefone   = TelefoneTextBox.Text?.Trim()            ?? string.Empty;
            var username   = NomeUsuarioCadastroTextBox.Text?.Trim() ?? string.Empty;
            var senhaPrime = SenhaPasswordBox.Password               ?? string.Empty;
            var email      = EmailTextBox.Text?.Trim()               ?? string.Empty;
            var categoria  = CategoriaTextBox.Text?.Trim()           ?? string.Empty;
            var nivelSel   = (NivelComboBox.SelectedItem as ComboBoxItem)
                                ?.Content?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(cpf) ||
                string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(senhaPrime))
            {
                MessageBox.Show(
                    "CPF, Nome, Nome de usuário e Senha de primeiro acesso são obrigatórios.",
                    "Campos obrigatórios",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // 1) Salva / atualiza em tecnicos.json (para telas de usuário/relatórios)
            var tecnicoInfo = new UsuarioStorage.TecnicoInfo
            {
                Cpf                 = cpf,
                NomeCompleto        = nome,
                Telefone            = telefone,
                NivelTecnico        = nivelSel,
                NomeUsuario         = username,
                SenhaPrimeiroAcesso = senhaPrime,
                Email               = email,
                CategoriaChamados   = categoria
            };
            UsuarioStorage.SalvarOuAtualizarTecnico(tecnicoInfo);

            // 2) Salva / atualiza em usuarios.json (para LOGIN)
            var usuario = new Usuario
            {
                Username       = username,
                NomeUsuario    = username,
                NomeCompleto   = nome,
                Nivel          = string.IsNullOrEmpty(nivelSel) ? "Nível 1" : $"Nível {nivelSel}",
                Categoria      = string.IsNullOrEmpty(categoria) ? "Não informado" : categoria,
                PasswordHash   = JsonUserStore.HashPassword(senhaPrime),
                PrimeiroAcesso = true,   // vai forçar tela de primeiro acesso no primeiro login
                Ativo          = true,
                FraseSeguranca = ""      // pode ser preenchida depois
            };
            _userStore.Update(usuario);

            MessageBox.Show(
                "Técnico cadastrado com sucesso.\n" +
                "Ele poderá entrar com o NOME DE USUÁRIO e a SENHA DE PRIMEIRO ACESSO.",
                "Sucesso",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            CancelarButton_Click(sender, e); // limpa os campos
        }
    }
}
