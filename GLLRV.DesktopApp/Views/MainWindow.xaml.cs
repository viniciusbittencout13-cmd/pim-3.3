using System;
using System.Windows;
using System.Windows.Threading;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly Usuario _usuario;

        public MainWindow(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            CarregarUsuarioNoTopo();
            IniciarRelogio();
            AbrirChamadosPendentes();
        }

        public MainWindow()
        {
            InitializeComponent();
            _usuario = new Usuario
            {
                Username = "vinicius",
                NomeCompleto = "Vinicius Bittencourt",
                Nivel = "2",
                Categoria = "Servidores e gerenciamento de rede"
            };
            CarregarUsuarioNoTopo();
            IniciarRelogio();
            AbrirChamadosPendentes();
        }

        private void CarregarUsuarioNoTopo()
        {
            UserNameText.Text = _usuario.NomeCompleto;
            UserLevelText.Text = $"TÉCNICO: NÍVEL {_usuario.Nivel}";
            UserCategoryText.Text = $"CATEGORIA: {_usuario.Categoria}";

            var partes = (_usuario.NomeCompleto ?? _usuario.Username).Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string iniciais =
                partes.Length >= 2 ? $"{partes[0][0]}{partes[1][0]}".ToUpper() :
                _usuario.Username.Length > 0 ? _usuario.Username[0].ToString().ToUpper() : "U";

            InitialsText.Text = iniciais;
        }

        private void IniciarRelogio()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (_, _) =>
            {
                DateTimeText.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            };
            timer.Start();
        }

        private void AbrirChamadosPendentes()
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock
            {
                Text = "Chamados pendentes (tela em construção)",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 18
            };
        }

        private void UsuariosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock { Text = "Usuários (tela em construção)", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 18 };
        }

        private void RelatoriosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock { Text = "Relatórios (tela em construção)", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 18 };
        }

        private void ChamadosPendentesButton_Click(object sender, RoutedEventArgs e)
        {
            AbrirChamadosPendentes();
        }

        private void HistoricoChamadosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock { Text = "Histórico de chamados (tela em construção)", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 18 };
        }

        private void ChamadosAndamentoButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock { Text = "Chamados em andamento (tela em construção)", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 18 };
        }

        private void ConfiguracaoButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new System.Windows.Controls.TextBlock { Text = "Configurações (tela em construção)", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 18 };
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
