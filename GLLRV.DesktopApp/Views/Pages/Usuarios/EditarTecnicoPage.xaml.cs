using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views.Pages.Usuarios
{
    public partial class EditarTecnicoPage : UserControl
    {
        public EditarTecnicoPage()
        {
            InitializeComponent();
        }

        // BOTÃO "CARREGAR DADOS" (busca pelo CPF digitado em BuscaCpfTextBox)
        private void CarregarButton_Click(object sender, RoutedEventArgs e)
        {
            var cpfBusca = BuscaCpfTextBox.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(cpfBusca))
            {
                MessageBox.Show("Informe o CPF do técnico que deseja editar.",
                    "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var tecnico = UsuarioStorage.ObterTecnicoPorCpf(cpfBusca);
            if (tecnico == null)
            {
                MessageBox.Show("Técnico não encontrado.", "Aviso",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Preenche os campos da tela
            CpfTextBox.Text          = tecnico.Cpf;
            NomeTextBox.Text         = tecnico.NomeCompleto;
            TelefoneTextBox.Text     = tecnico.Telefone;
            NomeUsuarioTextBox.Text  = tecnico.NomeUsuario;
            EmailTextBox.Text        = tecnico.Email;
            CategoriaTextBox.Text    = tecnico.CategoriaChamados;
            SenhaPasswordBox.Password = tecnico.SenhaPrimeiroAcesso ?? string.Empty;

            // Seleciona o nível no ComboBox
            var nivelItem = NivelComboBox
                .Items
                .OfType<ComboBoxItem>()
                .FirstOrDefault(i =>
                    string.Equals(i.Content?.ToString(), tecnico.NivelTecnico,
                        StringComparison.OrdinalIgnoreCase));

            if (nivelItem != null)
                NivelComboBox.SelectedItem = nivelItem;
        }

        // BOTÃO "SALVAR"
        private void SalvarButton_Click(object sender, RoutedEventArgs e)
        {
            var cpf = CpfTextBox.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(cpf))
            {
                MessageBox.Show("CPF não pode ficar em branco.",
                    "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var nivel = (NivelComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? string.Empty;

            var tecnico = new UsuarioStorage.TecnicoInfo
            {
                Cpf                = cpf,
                NomeCompleto       = NomeTextBox.Text?.Trim()        ?? string.Empty,
                Telefone           = TelefoneTextBox.Text?.Trim()    ?? string.Empty,
                NivelTecnico       = nivel,
                NomeUsuario        = NomeUsuarioTextBox.Text?.Trim() ?? string.Empty,
                SenhaPrimeiroAcesso = SenhaPasswordBox.Password      ?? string.Empty,
                Email              = EmailTextBox.Text?.Trim()       ?? string.Empty,
                CategoriaChamados  = CategoriaTextBox.Text?.Trim()   ?? string.Empty
            };

            UsuarioStorage.SalvarOuAtualizarTecnico(tecnico);

            // Sincroniza também com o usuário de login
            var store = new JsonUserStore();

           // tenta achar o usuário pelo NomeUsuario
           var usuario = store.GetByUsername(tecnico.NomeUsuario);
           if (usuario == null)
            {
               usuario = new Usuario
               {
                  Username    = tecnico.NomeUsuario,
                  NomeUsuario = tecnico.NomeUsuario
               };
            }

            usuario.NomeCompleto = tecnico.NomeCompleto;
            usuario.Nivel        = $"Nível {tecnico.NivelTecnico}";
            usuario.Categoria    = tecnico.CategoriaChamados;
            usuario.Ativo        = true;

            // se o campo de senha de primeiro acesso foi preenchido,
            // atualiza a senha de login e marca como primeiro acesso.
            if (!string.IsNullOrWhiteSpace(tecnico.SenhaPrimeiroAcesso))
            {
              usuario.PasswordHash  = JsonUserStore.HashPassword(tecnico.SenhaPrimeiroAcesso);
              usuario.PrimeiroAcesso = true;  // vai obrigar a trocar no próximo login
            }

            store.Update(usuario);

            MessageBox.Show("Dados do técnico salvos com sucesso.",
                "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // BOTÃO "CANCELAR"
        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            // limpa todos os campos
            BuscaCpfTextBox.Text      = string.Empty;
            CpfTextBox.Text           = string.Empty;
            NomeTextBox.Text          = string.Empty;
            TelefoneTextBox.Text      = string.Empty;
            NomeUsuarioTextBox.Text   = string.Empty;
            SenhaPasswordBox.Password = string.Empty;
            EmailTextBox.Text         = string.Empty;
            CategoriaTextBox.Text     = string.Empty;
            NivelComboBox.SelectedIndex = -1;
        }
    }
}
