using System.Collections.Generic;
using System.Windows.Controls;

namespace GLLRV.DesktopApp.Views.Pages
{
    public partial class ChamadosPendentesPage : UserControl
    {
        public ChamadosPendentesPage()
        {
            InitializeComponent();
            CarregarChamadosFake();
        }

        private void CarregarChamadosFake()
        {
            var dados = new List<ChamadoGridItem>
            {
                new ChamadoGridItem
                {
                    Usuario = "GUSTAVO M",
                    NivelPrioridade = "ALTA",
                    Dificuldade = "MÉDIA",
                    Data = "04/05/2025",
                    Horario = "15:03"
                }
            };

            ChamadosGrid.ItemsSource = dados;
        }
    }

    public class ChamadoGridItem
    {
        public string Usuario { get; set; } = "";
        public string NivelPrioridade { get; set; } = "";
        public string Dificuldade { get; set; } = "";
        public string Data { get; set; } = "";
        public string Horario { get; set; } = "";
    }
}
