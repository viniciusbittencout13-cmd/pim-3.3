using System;
using System.Windows;
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
            _usuario = usuario;
            CarregarUsuarioNoTopo();
            IniciarRelogio();
            AbrirChamadosPendentes();
        }

        private void CarregarUsuarioNoTopo()
        {
            var nome = string.IsNullOrWhiteSpace(_usuario.NomeCompleto)
                ? _usuario.NomeUsuario
                : _usuario.NomeCompleto;

            UserNameText.Text = nome;
            UserLevelText.Text = $"TÉCNICO: NÍVEL {_usuario.Nivel}";
            UserCategoryText.Text = $"CATEGORIA: {_usuario.Categoria}";

            var partes = (nome ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
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

        private void SetPage(object page)
        {
            ContentHost.Content = page;
        }

        private void AbrirChamadosPendentes()
        {
            SetPage(new ChamadosPendentesPage());
        }

        private void UsuariosButton_Click(object sender, RoutedEventArgs e)
        {
            SetPage(new UsuariosPage());
        }

        private void RelatoriosButton_Click(object sender, RoutedEventArgs e)
        {
            SetPage(new RelatoriosPage());
        }

        private void ChamadosPendentesButton_Click(object sender, RoutedEventArgs e)
        {
            AbrirChamadosPendentes();
        }

        private void HistoricoChamadosButton_Click(object sender, RoutedEventArgs e)
        {
            SetPage(new HistoricoChamadosPage());
        }

        private void ChamadosAndamentoButton_Click(object sender, RoutedEventArgs e)
        {
            SetPage(new ChamadosAndamentoPage());
        }

        private void ConfiguracaoButton_Click(object sender, RoutedEventArgs e)
        {
            SetPage(new ConfiguracaoPage());
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
