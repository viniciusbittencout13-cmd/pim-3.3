using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views.Pages
{
    public partial class RelatorioGenericoPage : UserControl
    {
        public RelatorioGenericoPage(string titulo)
        {
            InitializeComponent();
            ReportTitleText.Text = titulo;

            // preenche técnicos existentes
            var tecnicos = JsonDataStore.LoadChamados()
                                        .Select(c => c.Responsavel)
                                        .Where(s => !string.IsNullOrWhiteSpace(s))
                                        .Distinct()
                                        .OrderBy(s => s)
                                        .ToList();
            CbTecnico.Items.Add("Todos");
            foreach (var t in tecnicos) CbTecnico.Items.Add(t);
            CbTecnico.SelectedIndex = 0;

            Loaded += (_, __) => RenderChart();
            SizeChanged += (_, __) => RenderChart(); // redimensiona
        }

        private void OnFilterChanged(object sender, EventArgs e) => RenderChart();

        private void RenderChart()
        {
            if (ChartArea.ActualWidth <= 0 || ChartArea.ActualHeight <= 0) return;

            var dados = JsonDataStore.LoadChamados();

            // Filtro por data: usa DataAbertura como referência
            if (DpDe.SelectedDate is DateTime de)
                dados = dados.Where(c => c.DataAbertura >= de.Date).ToList();
            if (DpAte.SelectedDate is DateTime ate)
                dados = dados.Where(c => c.DataAbertura <= ate.Date.AddDays(1).AddTicks(-1)).ToList();

            // Filtro por período do dia
            var periodo = (CbPeriodo.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (periodo?.StartsWith("Manhã") == true)
                dados = dados.Where(c => c.DataAbertura.Hour >= 0 && c.DataAbertura.Hour < 12).ToList();
            else if (periodo?.StartsWith("Tarde") == true)
                dados = dados.Where(c => c.DataAbertura.Hour >= 12 && c.DataAbertura.Hour < 18).ToList();
            else if (periodo?.StartsWith("Noite") == true)
                dados = dados.Where(c => c.DataAbertura.Hour >= 18 && c.DataAbertura.Hour <= 23).ToList();

            // Filtro por técnico
            if (CbTecnico.SelectedItem is string tec && tec != "Todos")
                dados = dados.Where(c => string.Equals(c.Responsavel, tec, StringComparison.OrdinalIgnoreCase)).ToList();

            // (Categoria / Prioridade ainda não existem no modelo -> ignorados por enquanto)

            // Agrupa por responsável (série única de barras)
            var grupos = dados.GroupBy(c => string.IsNullOrWhiteSpace(c.Responsavel) ? "—" : c.Responsavel)
                              .Select(g => new { Tecnico = g.Key, Qtde = g.Count() })
                              .OrderBy(g => g.Tecnico)
                              .ToList();

            // Evita divisão por zero
            if (grupos.Count == 0) grupos = new[] { new { Tecnico = "Sem dados", Qtde = 0 } }.ToList();

            // Desenha
            ChartArea.Children.Clear();

            double w = ChartArea.ActualWidth;
            double h = ChartArea.ActualHeight;

            // Eixo X
            var eixo = new Line { X1 = 40, Y1 = h - 30, X2 = w - 10, Y2 = h - 30, Stroke = Brushes.Gray, StrokeThickness = 1 };
            ChartArea.Children.Add(eixo);

            int max = Math.Max(1, grupos.Max(g => g.Qtde));
            double colWidth = (w - 80) / grupos.Count;

            for (int i = 0; i < grupos.Count; i++)
            {
                double x = 40 + i * colWidth + colWidth * 0.15;
                double barW = colWidth * 0.70;
                double barH = (h - 80) * grupos[i].Qtde / max;

                var rect = new Rectangle
                {
                    Width = barW,
                    Height = barH,
                    Fill = Brushes.SteelBlue,
                };
                Canvas.SetLeft(rect, x);
                Canvas.SetTop(rect, (h - 30) - barH);
                ChartArea.Children.Add(rect);

                var lbl = new TextBlock
                {
                    Text = grupos[i].Tecnico,
                    RenderTransform = new RotateTransform(0),
                    FontSize = 12
                };
                Canvas.SetLeft(lbl, x);
                Canvas.SetTop(lbl, h - 26);
                ChartArea.Children.Add(lbl);

                var val = new TextBlock
                {
                    Text = grupos[i].Qtde.ToString(),
                    FontWeight = FontWeights.Bold,
                    FontSize = 12
                };
                Canvas.SetLeft(val, x + barW / 2 - 6);
                Canvas.SetTop(val, (h - 35) - barH);
                ChartArea.Children.Add(val);
            }
        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.MainWindow as MainWindow;
            mw?.Navigate(new RelatoriosPage());
        }

        private void Exportar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exportar relatório: em construção 🙂", "Relatórios",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
