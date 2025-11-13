using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;              // <-- IMPORTANTE
using GLLRV.DesktopApp.Views.Pages.Relatorios; // <-- onde está a RelatoriosPage

namespace GLLRV.DesktopApp.Views.Pages.Relatorios
{
    public partial class RelatorioGenericoPage : Page
    {
        private readonly string _titulo;

        public RelatorioGenericoPage(string titulo)
        {
            InitializeComponent();
            _titulo = titulo;

            // Título que aparece dentro da própria página (se você estiver usando)
            PageTitleText.Text = titulo;

            // Título que o MainWindow mostra lá em cima (porque ele lê o Tag da Page)
            this.Tag = $"RELATÓRIOS - {titulo.ToUpper()}";
        }

        private void VoltarButton_Click(object sender, RoutedEventArgs e)
        {
            // Volta para a tela de escolha de relatórios
            NavigationService?.Navigate(new RelatoriosPage());
        }
    }
}
