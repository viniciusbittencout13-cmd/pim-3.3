using System.Windows.Controls;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views.Pages
{
    public partial class ChamadosAndamentoPage : UserControl
    {
        public ChamadosAndamentoPage()
        {
            InitializeComponent();
            // DataGrid DE ANDAMENTO
            ChamadosGrid.ItemsSource = JsonDataStore.GetAndamento();
        }
    }
}
