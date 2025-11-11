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
            //UserNameText.Text = _usuario.Username;
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

        private void UsuariosButton_Click(object sender, RoutedEventArgs e)
{
    PageTitleText.Text = "USUÁRIOS";
    MainContentFrame.Content = new UsuariosPage(_usuario);
}

private void RelatoriosButton_Click(object sender, RoutedEventArgs e)
{
    PageTitleText.Text = "RELATÓRIOS";
    MainContentFrame.Content = new RelatoriosPage(_usuario);
}

private void ChamadosPendentesButton_Click(object sender, RoutedEventArgs e)
{
    PageTitleText.Text = "CHAMADOS PENDENTES";
    MainContentFrame.Content = new ChamadosPendentesPage(_usuario);
}

private void HistoricoChamadosButton_Click(object sender, RoutedEventArgs e)
{
    PageTitleText.Text = "HISTÓRICO DE CHAMADOS";
    MainContentFrame.Content = new HistoricoChamadosPage(_usuario);
}

private void ChamadosAndamentoButton_Click(object sender, RoutedEventArgs e)
{
    PageTitleText.Text = "CHAMADOS EM ANDAMENTO";
    MainContentFrame.Content = new ChamadosAndamentoPage(_usuario);
}

private void ConfiguracoesButton_Click(object sender, RoutedEventArgs e)
{
    PageTitleText.Text = "CONFIGURAÇÕES";
    MainContentFrame.Content = new ConfiguracoesPage(_usuario);
}

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
