using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Views.Pages;

namespace GLLRV.DesktopApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly Usuario _usuario;

        public MainWindow(Usuario usuario)
        {
            InitializeComponent();

            _usuario = usuario ?? new Usuario
            {
                NomeUsuario = "vinicius",
                NomeCompleto = "Vinicius Bittencourt",
                Nivel = "Nível 2",
                Categoria = "Servidores / Rede"
            };

            CarregarUsuarioNoTopo();
            IniciarRelogio();
            AbrirChamadosPendentes();
        }

        public MainWindow()
            : this(new Usuario
            {
                NomeUsuario = "vinicius",
                NomeCompleto = "Vinicius Bittencourt",
                Nivel = "Nível 2",
                Categoria = "Servidores / Rede"
            })
        {
        }

        private void CarregarUsuarioNoTopo()
        {
            if (_usuario == null) return;

            UserNameText.Text = _usuario.NomeCompleto ?? _usuario.NomeUsuario ?? "-";
            UserLevelText.Text = $"TÉCNICO: {_usuario.Nivel ?? "-"}";
            UserCategoryText.Text = $"CATEGORIA: {_usuario.Categoria ?? "-"}";

            var nome = _usuario.NomeCompleto ?? _usuario.NomeUsuario ?? "";
            var partes = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string iniciais = "?";
            if (partes.Length >= 2)
                iniciais = $"{partes[0][0]}{partes[1][0]}";
            else if (partes.Length == 1)
                iniciais = partes[0][0].ToString();

            InitialsText.Text = iniciais.ToUpperInvariant();
        }

        private void IniciarRelogio()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += (_, _) =>
            {
                DateTimeText.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
            };

            timer.Start();
        }

        private void AbrirChamadosPendentes()
        {
            ContentHost.Content = new ChamadosPendentesPage();
        }

        private void UsuariosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new UsuariosPage();
        }

        private void RelatoriosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new RelatoriosPage();
        }

        private void ChamadosPendentesButton_Click(object sender, RoutedEventArgs e)
        {
            AbrirChamadosPendentes();
        }

        private void HistoricoChamadosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new HistoricoChamadosPage();
        }

        private void ChamadosAndamentoButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new ChamadosAndamentoPage();
        }

        private void ConfiguracaoButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Content = new ConfiguracaoPage();
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void AbrirChamadosPendentes()
{
    ContentArea.Children.Clear();
    ContentArea.Children.Add(new GLLRV.DesktopApp.Views.Pages.ChamadosPendentesPage());
}

private void ChamadosPendentesButton_Click(object sender, RoutedEventArgs e)
{
    AbrirChamadosPendentes();
}
    }
}
