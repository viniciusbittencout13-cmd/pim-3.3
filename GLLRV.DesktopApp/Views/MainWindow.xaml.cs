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

            _usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));

            // Preenche dados do técnico (se tiver esses campos no Usuario)
            UserNameText.Text = _usuario.Username;
            UserLevelText.Text = $"Técnico - Nível {_usuario.Nivel}";
            UserCategoryText.Text = $"Categoria: {_usuario.Categoria}";

            // Relógio no canto
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (_, _) =>
            {
                DateTimeText.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
            };
            timer.Start();

            // Abre Chamados Pendentes padrão
            AbrirChamadosPendentes();
        }

        private void AbrirChamadosPendentes()
        {
            ContentFrame.Content = new ChamadosPendentesPage();
        }

        private void ChamadosPendentesButton_Click(object sender, RoutedEventArgs e)
        {
            AbrirChamadosPendentes();
        }

        private void UsuariosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = new UsuariosPage();
        }

        private void RelatoriosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = new RelatoriosPage();
        }

        private void HistoricoChamadosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = new HistoricoChamadosPage();
        }

        private void ChamadosAndamentoButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = new ChamadosAndamentoPage();
        }

        private void ConfiguracaoButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Content = new ConfiguracaoPage();
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
