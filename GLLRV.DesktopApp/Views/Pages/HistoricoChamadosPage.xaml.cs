using System.Windows.Controls;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views.Pages
{
    public partial class HistoricoChamadosPage : UserControl
    {
        public HistoricoChamadosPage()
        {
            InitializeComponent();
            // DataGrid DO HISTÓRICO
            HistoricoGrid.ItemsSource = JsonDataStore.GetHistorico();
        }
    }
}
