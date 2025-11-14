using System;
using System.Windows;
using System.Windows.Controls;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views.Pages.Usuarios
{
    public partial class CadastroTecnicoPage : UserControl
    {
        public CadastroTecnicoPage()
        {
            InitializeComponent();
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            // limpa todos os campos
            CpfTextBox.Text            = string.Empty;
            NomeTextBox.Text           = string.Empty;
            TelefoneTextBox.Text       = string.Empty;
            NomeUsuarioTextBox.Text    = string.Empty;
            SenhaPasswordBox.Password  = string.Empty;
            EmailTextBox.Text          = string.Empty;
            CategoriaTextBox.Text      = string.Empty;
            NivelComboBox.SelectedIndex = -1;
        }

        private void CadastrarButton_Click(object sender, RoutedEventArgs e)
        {
            var cpf = CpfTextBox.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(cpf))
            {
                MessageBox.Show("Informe o CPF do técnico.",
                    "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            MessageBox.Show("Técnico cadastrado com sucesso.",
                "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            CancelarButton_Click(sender, e); // limpa depois de salvar
        }
    }
}
