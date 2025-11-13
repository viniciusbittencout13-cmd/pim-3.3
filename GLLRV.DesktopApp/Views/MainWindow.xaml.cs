using System;
using System.Windows;
using System.Windows.Threading;
using GLLRV.DesktopApp.Views.Pages;
using GLLRV.DesktopApp.Views.Pages.Relatorios;
using GLLRV.DesktopApp.Models; // se quiser preencher infos do usuário

namespace GLLRV.DesktopApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _clockTimer = new DispatcherTimer();
        private readonly Usuario? _usuario;

        // Construtor padrão (usado pelo StartupUri) 
        public MainWindow() : this(null) { }

        // Construtor opcional com usuário (caso o login chame new MainWindow(usuario))
        public MainWindow(Usuario? usuario)
        {
            InitializeComponent();
            _usuario = usuario;

            // Preenche infos do cartão (se tiver vindo do login)
            if (_usuario != null)
            {
                // Ajuste estes campos se quiser exibir outros dados
                UserLevelText.Text = $"Técnico - Nível {_usuario.Nivel}";
                UserCategoryText.Text = _usuario.Categoria ?? "Categoria";
            }

            // Página inicial
            MainContentFrame.Navigate(new ChamadosPendentesPage());

            // Relógio
            _clockTimer.Interval = TimeSpan.FromSeconds(1);
            _clockTimer.Tick += (_, __) =>
                ClockText.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
            _clockTimer.Start();
        }

        private void UsuariosButton_Click(object sender, RoutedEventArgs e)
            => MainContentFrame.Navigate(new UsuariosPage());

        public void RelatoriosButton_Click(object sender, RoutedEventArgs e)
            => MainContentFrame.Navigate(new RelatoriosPage());

        private void ChamadosPendentesButton_Click(object sender, RoutedEventArgs e)
            => MainContentFrame.Navigate(new ChamadosPendentesPage());

        private void HistoricoChamadosButton_Click(object sender, RoutedEventArgs e)
            => MainContentFrame.Navigate(new HistoricoChamadosPage());

        private void ChamadosAndamentoButton_Click(object sender, RoutedEventArgs e)
            => MainContentFrame.Navigate(new ChamadosAndamentoPage());

        // Faltava esse handler no seu build
        private void ConfiguracaoButton_Click(object sender, RoutedEventArgs e)
            => MainContentFrame.Navigate(new ConfiguracoesPage());

        private void SairButton_Click(object sender, RoutedEventArgs e)
            => Close();
    }
}
