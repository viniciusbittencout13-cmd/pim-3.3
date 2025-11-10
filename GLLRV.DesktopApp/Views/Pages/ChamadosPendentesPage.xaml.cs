using System.Windows.Controls;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views.Pages
{
    public partial class ChamadosPendentesPage : Page
    {
        public ChamadosPendentesPage()
        {
            InitializeComponent();
            ChamadosGrid.ItemsSource = JsonDataStore.GetPendentes();
        }
    }
}
