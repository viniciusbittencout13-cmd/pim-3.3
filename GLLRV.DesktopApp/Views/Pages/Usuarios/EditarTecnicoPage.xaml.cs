using System.Windows;
using System.Windows.Controls;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views.Pages.Usuarios
{
    public partial class EditarTecnicoPage : UserControl
    {
        private readonly JsonUserStore _userStore = new JsonUserStore();
        private TecnicoInfo? _tecnicoAtual;

        public EditarTecnicoPage()
        {
            InitializeComponent();
        }

        // Botão que BUSCA o técnico (pode ser por CPF ou usuário – adaptei para CPF)
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var cpf = CpfTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(cpf))
            {
                MessageBox.Show("Informe o CPF para buscar o técnico.",
                    "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _tecnicoAtual = UsuarioStorage.ObterTecnicoPorCpf(cpf);
            if (_tecnicoAtual == null)
            {
                MessageBox.Show("Técnico não encontrado.",
                    "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Preenche os campos com os dados encontrados
            NomeTextBox.Text        = _tecnicoAtual.NomeCompleto;
            TelefoneTextBox.Text    = _tecnicoAtual.Telefone;
            NivelComboBox.Text      = _tecnicoAtual.NivelTecnico;
            NomeUsuarioTextBox.Text = _tecnicoAtual.NomeUsuario;
            EmailTextBox.Text       = _tecnicoAtual.Email;
            CategoriaComboBox.Text  = _tecnicoAtual.CategoriaChamados;
            SenhaPasswordBox.Password = _tecnicoAtual.SenhaPrimeiroAcesso;
        }

        // Botão que SALVA as alterações
        private void SalvarButton_Click(object sender, RoutedEventArgs e)
        {
            if (_tecnicoAtual == null)
            {
                MessageBox.Show("Nenhum técnico carregado para edição.",
                    "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // 1) Atualiza o técnico no tecnicos.json
            _tecnicoAtual.NomeCompleto        = NomeTextBox.Text.Trim();
            _tecnicoAtual.Telefone            = TelefoneTextBox.Text.Trim();
            _tecnicoAtual.NivelTecnico        = NivelComboBox.Text;
            _tecnicoAtual.NomeUsuario         = NomeUsuarioTextBox.Text.Trim();
            _tecnicoAtual.Email               = EmailTextBox.Text.Trim();
            _tecnicoAtual.CategoriaChamados   = CategoriaComboBox.Text;
            _tecnicoAtual.SenhaPrimeiroAcesso = SenhaPasswordBox.Password;

            UsuarioStorage.SalvarOuAtualizarTecnico(_tecnicoAtual);

            // 2) Sincroniza com usuarios.json
            var usuario = _userStore.GetByUsername(_tecnicoAtual.NomeUsuario);
            if (usuario != null)
            {
                usuario.NomeCompleto = _tecnicoAtual.NomeCompleto;
                usuario.Nivel        = $"Nível {_tecnicoAtual.NivelTecnico}";
                usuario.Categoria    = _tecnicoAtual.CategoriaChamados;

                if (!string.IsNullOrWhiteSpace(_tecnicoAtual.SenhaPrimeiroAcesso))
                {
                    usuario.PasswordHash = JsonUserStore.HashPassword(
                        _tecnicoAtual.SenhaPrimeiroAcesso);
                }

                _userStore.Update(usuario);
            }

            MessageBox.Show("Técnico atualizado com sucesso!", "Sucesso",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            // aqui você pode limpar os campos ou navegar para outra página
        }
    }
}
