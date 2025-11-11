using System.Collections.Generic;
using System.Windows.Controls;

namespace GLLRV.DesktopApp.Views.Pages
{
    public partial class ChamadosPendentesPage : Page
    {
        public ChamadosPendentesPage()
        {
            InitializeComponent();

            // Mock de dados de teste
            ChamadosGrid.ItemsSource = new List<dynamic>
            {
                new { Usuario = "VINICIUS", NivelPrioridade = "ALTA", Dificuldade = "MÉDIA", Data = "05/05/2025", Horario = "14:30" }
            };
        }
    }
}
