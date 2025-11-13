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

        private void CadastrarButton_Click(object sender, RoutedEventArgs e)
        {
            // 1) Monta o objeto do técnico (arquivo tecnicos.json)
            var tecnico = new TecnicoInfo
            {
                Cpf                 = CpfTextBox.Text.Trim(),
                NomeCompleto        = NomeTextBox.Text.Trim(),
                Telefone            = TelefoneTextBox.Text.Trim(),
                NivelTecnico        = NivelComboBox.Text,
                NomeUsuario         = NomeUsuarioTextBox.Text.Trim(),
                SenhaPrimeiroAcesso = SenhaPasswordBox.Password,
                Email               = EmailTextBox.Text.Trim(),
                CategoriaChamados   = CategoriaComboBox.Text
            };

            // Salva/atualiza no tecnicos.json (classe ESTÁTICA)
            UsuarioStorage.SalvarOuAtualizarTecnico(tecnico);

            // 2) Cria/atualiza o usuário de login em usuarios.json
            var usuario = new Usuario
            {
                Username       = tecnico.NomeUsuario,
                NomeUsuario    = tecnico.NomeUsuario,
                NomeCompleto   = tecnico.NomeCompleto,
                Nivel          = $"Nível {tecnico.NivelTecnico}",
                Categoria      = tecnico.CategoriaChamados,
                PasswordHash   = JsonUserStore.HashPassword(tecnico.SenhaPrimeiroAcesso),
                PrimeiroAcesso = true,
                Ativo          = true,
                FraseSeguranca = ""
            };

            _userStore.Update(usuario);

            MessageBox.Show("Técnico cadastrado com sucesso!", "Sucesso",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            // Se quiser, limpa os campos aqui
        }
    }
}
