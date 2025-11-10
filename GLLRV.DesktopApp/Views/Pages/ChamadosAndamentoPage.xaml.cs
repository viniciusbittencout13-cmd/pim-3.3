using System.Windows.Controls;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp.Views.Pages
{
    public partial class ChamadosAndamentoPage : Page
    {
        public ChamadosAndamentoPage()
        {
            InitializeComponent();
            ChamadosGrid.ItemsSource = JsonDataStore.GetAndamento();
        }
    }
}
