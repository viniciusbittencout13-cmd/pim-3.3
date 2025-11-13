using System.Windows;
using System.Windows.Controls;

namespace GLLRV.DesktopApp.Views.Pages.Relatorios
{
    public partial class RelatorioGenericoPage : UserControl
    {
        public RelatorioGenericoPage(string titulo)
        {
            InitializeComponent();
            PageTitleText.Text = titulo;
        }

        private void VoltarButton_Click(object sender, RoutedEventArgs e)
        {
            // volta para a tela de escolha dos relatórios
            if (Application.Current.MainWindow is MainWindow mw)
            {
                mw.RelatoriosButton_Click(this, new RoutedEventArgs());
            }
        }
    }
}
