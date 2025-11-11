using System.Windows.Controls;

namespace GLLRV.DesktopApp.Views.Pages
{
    public partial class ChamadosPendentesPage : Page
    {
        public ChamadosPendentesPage()
        {
            InitializeComponent();

            // Só pra preencher com dados fake por enquanto
            ChamadosDataGrid.ItemsSource = new[]
            {
                new { Usuario = "Gustavo", NivelPrioridade = "Alto", Dificuldade = "Média", DataAbertura = "11/11/2025", Horario = "15:30" },
                new { Usuario = "Vinícius", NivelPrioridade = "Médio", Dificuldade = "Baixa", DataAbertura = "11/11/2025", Horario = "16:00" }
            };
        }
    }
}
