using System.Collections.Generic;
using System.Windows.Controls;

namespace GLLRV.DesktopApp.Views.Pages
{
    public partial class ChamadosPendentesPage : Page
    {
        public ChamadosPendentesPage()
        {
            InitializeComponent();

            // Dados de exemplo / depois você troca pelo serviço real
            var chamados = new List<dynamic>
            {
                new { Usuario = "Gustavo",  NivelPrioridade = "Alto",  Dificuldade = "Média", Data = "11/11/2025", Horario = "15:30" },
                new { Usuario = "Vinícius", NivelPrioridade = "Médio", Dificuldade = "Baixa", Data = "11/11/2025", Horario = "16:00" }
            };

            ChamadosDataGrid.ItemsSource = chamados;
        }
    }
}
