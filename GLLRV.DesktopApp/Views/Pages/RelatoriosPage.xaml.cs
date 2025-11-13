using System.Windows;
using System.Windows.Controls;

namespace GLLRV.DesktopApp.Views.Pages
{
    public partial class RelatoriosPage : UserControl
    {
        public RelatoriosPage()
        {
            InitializeComponent();
        }

        private void Tempo_Click(object sender, RoutedEventArgs e) =>
            Navigate(new RelatorioGenericoPage("TEMPO DE ATENDIMENTO"));

        private void AvaliacaoAtend_Click(object sender, RoutedEventArgs e) =>
            Navigate(new RelatorioGenericoPage("AVALIAÇÃO DOS ATENDIMENTOS"));

        private void AvaliacaoTec_Click(object sender, RoutedEventArgs e) =>
            Navigate(new RelatorioGenericoPage("AVALIAÇÃO DOS TÉCNICOS"));

        private void Atendimentos_Click(object sender, RoutedEventArgs e) =>
            Navigate(new RelatorioGenericoPage("ATENDIMENTOS"));

        // Usa o método do MainWindow para trocar o conteúdo do frame
        private void Navigate(UserControl page)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw?.Navigate(page);
        }
    }
}
