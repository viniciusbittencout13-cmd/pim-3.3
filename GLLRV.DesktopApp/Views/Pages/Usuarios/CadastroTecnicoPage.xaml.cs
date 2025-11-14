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
            CpfTextBox.Text         = string.Empty;
            NomeTextBox.Text        = string.Empty;
            TelefoneTextBox.Text    = string.Empty;
            NomeUsuarioTextBox.Text = string.Empty;
            SenhaPasswordBox.Password = string.Empty;
            EmailTextBox.Text       = string.Empty;
            CategoriaTextBox.Text   = string.Empty;
            NivelComboBox.SelectedIndex = -1;
        }

        private void CadastrarButton_Click(object sender, RoutedEventArgs e)
        {
            // CPF obrigatório
            var cpf = (CpfTextBox.Text ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(cpf))
            {
                MessageBox.Show("Informe o CPF do técnico.",
                    "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // nível técnico
            var nivelItem = NivelComboBox.SelectedItem as ComboBoxItem;
            var nivel = nivelItem != null
                ? (nivelItem.Content?.ToString() ?? string.Empty)
                : string.Empty;

            var tecnico = new UsuarioStorage.TecnicoInfo
            {
                Cpf                 = cpf,
                NomeCompleto        = (NomeTextBox.Text        ?? string.Empty).Trim(),
                Telefone            = (TelefoneTextBox.Text    ?? string.Empty).Trim(),
                NivelTecnico        = nivel,
                NomeUsuario         = (NomeUsuarioTextBox.Text ?? string.Empty).Trim(),
                SenhaPrimeiroAcesso = SenhaPasswordBox.Password ?? string.Empty,
                Email               = (EmailTextBox.Text       ?? string.Empty).Trim(),
                CategoriaChamados   = (CategoriaTextBox.Text   ?? string.Empty).Trim()
            };

            UsuarioStorage.SalvarOuAtualizarTecnico(tecnico);

            MessageBox.Show("Técnico cadastrado com sucesso.",
                "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            // limpa os campos depois de salvar
            CancelarButton_Click(sender, e);
        }
    }
}
