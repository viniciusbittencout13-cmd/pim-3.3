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
        private readonly DispatcherTimer _timer;

        public MainWindow(Usuario usuario)
        {
            _usuario = usuario;
            InitializeComponent();

            // Atualiza título inicial e página inicial
            PageTitleText.Text = "CHAMADOS PENDENTES";
            MainContentFrame.Content = new ChamadosPendentesPage();

            // Relógio no topo
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += (_, __) =>
            {
                DateTimeText.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
            };
            _timer.Start();
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void UsuariosButton_Click(object sender, RoutedEventArgs e)
        {
            PageTitleText.Text = "USUÁRIOS";
            MainContentFrame.Content = new UsuariosPage();
        }

        private void RelatoriosButton_Click(object sender, RoutedEventArgs e)
        {
            PageTitleText.Text = "RELATÓRIOS";
            MainContentFrame.Content = new RelatoriosPage();
        }

        private void ChamadosPendentesButton_Click(object sender, RoutedEventArgs e)
        {
            PageTitleText.Text = "CHAMADOS PENDENTES";
            MainContentFrame.Content = new ChamadosPendentesPage();
        }

        private void HistoricoChamadosButton_Click(object sender, RoutedEventArgs e)
        {
            PageTitleText.Text = "HISTÓRICO DE CHAMADOS";
            MainContentFrame.Content = new HistoricoChamadosPage();
        }

        private void ChamadosAndamentoButton_Click(object sender, RoutedEventArgs e)
        {
            PageTitleText.Text = "CHAMADOS EM ANDAMENTO";
            MainContentFrame.Content = new ChamadosAndamentoPage();
        }

        private void ConfiguracoesButton_Click(object sender, RoutedEventArgs e)
        {
            PageTitleText.Text = "CONFIGURAÇÕES";
            MainContentFrame.Content = new ConfiguracoesPage();
        }
    }
}
