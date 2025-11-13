using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GLLRV.DesktopApp.Views.Pages.Relatorios
{
    public partial class RelatoriosPage : UserControl
    {
        public RelatoriosPage() => InitializeComponent();

        private static T? FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            var p = VisualTreeHelper.GetParent(child);
            while (p != null && p is not T) p = VisualTreeHelper.GetParent(p);
            return p as T;
        }
        private void Go(UserControl page)
        {
            var frame = FindParent<Frame>(this);
            if (frame != null) frame.Content = page;
        }

        private void Atendimentos_Click(object s, RoutedEventArgs e) => Go(new RelatorioAtendimentosPage());
        private void TempoAtendimento_Click(object s, RoutedEventArgs e) => Go(new RelatorioTempoAtendimentoPage());
        private void AvaliacaoAtendimento_Click(object s, RoutedEventArgs e) => Go(new RelatorioAvaliacaoAtendimentoPage());
        private void AvaliacaoTecnicos_Click(object s, RoutedEventArgs e) => Go(new RelatorioAvaliacaoTecnicosPage());
    }
}
