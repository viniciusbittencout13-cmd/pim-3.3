using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views.Pages.Relatorios
{
    public partial class RelatorioAvaliacaoTecnicosPage : UserControl
    {
        public RelatorioAvaliacaoTecnicosPage()
        {
            InitializeComponent();
            PeriodoCombo.ItemsSource = new[] { "Todos", "Madrugada", "Manhã", "Tarde", "Noite" };
            CategoriaCombo.ItemsSource = new[] { "Todos", "Infraestrutura", "Sistemas", "Rede", "Hardware", "Software", "Outros" };
            PrioridadeCombo.ItemsSource = new[] { "Todas", "Baixa", "Média", "Alta", "Crítica" };
            TecnicoCombo.ItemsSource = JsonDataStore.LoadChamados()
                .Select(c => c.Responsavel).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct()
                .Prepend("Todos").ToList();

            PeriodoCombo.SelectedIndex = CategoriaCombo.SelectedIndex =
                PrioridadeCombo.SelectedIndex = TecnicoCombo.SelectedIndex = 0;
        }

        private static T? FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            var p = VisualTreeHelper.GetParent(child);
            while (p != null && p is not T) p = VisualTreeHelper.GetParent(p);
            return p as T;
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            var frame = FindParent<Frame>(this);
            if (frame != null) frame.Content = new RelatoriosPage();
        }
    }
}
