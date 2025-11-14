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
            CpfTextBox.Text          = string.Empty;
            NomeTextBox.Text         = string.Empty;
            TelefoneTextBox.Text     = string.Empty;
            NomeUsuarioTextBox.Text  = string.Empty;
            SenhaPasswordBox.Password = string.Empty;
            EmailTextBox.Text        = string.Empty;
            CategoriaTextBox.Text    = string.Empty;
            NivelComboBox.SelectedIndex = -1;
        }

        private void CadastrarButton_Click(object sender, RoutedEventArgs e)
        {
            var cpf        = CpfTextBox.Text.Trim();
            var nome       = NomeTextBox.Text.Trim();
            var telefone   = TelefoneTextBox.Text.Trim();
            var username   = NomeUsuarioTextBox.Text.Trim();
            var senhaPrime = SenhaPasswordBox.Password.Trim();
            var email      = EmailTextBox.Text.Trim();
            var categoria  = CategoriaTextBox.Text.Trim();
            var nivelSel   = (NivelComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(cpf) ||
                string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(senhaPrime))
            {
                MessageBox.Show("CPF, Nome, Nome de Usuário e Senha são obrigatórios.",
                    "Campos obrigatórios", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 🔹 GRAVA NO tecnicos.json
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

            // 🔹 GRAVA NO usuarios.json (LOGIN)
            var usuario = new Usuario
            {
                Username       = username,
                NomeUsuario    = username,
                NomeCompleto   = nome,
                Nivel          = string.IsNullOrWhiteSpace(nivelSel) ? "Nível 1" : $"Nível {nivelSel}",
                Categoria      = string.IsNullOrWhiteSpace(categoria) ? "Não informado" : categoria,
                PasswordHash   = JsonUserStore.HashPassword(senhaPrime),
                PrimeiroAcesso = true,
                Ativo          = true,
                FraseSeguranca = ""
            };

            _userStore.Update(usuario);

            MessageBox.Show("Técnico cadastrado com sucesso!",
                "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            CancelarButton_Click(sender, e); // limpa tudo
        }
    }
}
