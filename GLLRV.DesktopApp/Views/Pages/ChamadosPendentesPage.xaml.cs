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
            var lista = new List<ChamadoPendenteItem>
            {
                new()
                {
                    Usuario = "Vinicius",
                    Prioridade = "Alta",
                    Dificuldade = "Média",
                    Data = "05/05/2025",
                    Horario = "14:30"
                },
                new()
                {
                    Usuario = "Gustavo",
                    Prioridade = "Média",
                    Dificuldade = "Baixa",
                    Data = "05/05/2025",
                    Horario = "15:00"
                }
            };

            ChamadosGrid.ItemsSource = lista;
        }

        private class ChamadoPendenteItem
        {
            public string Usuario { get; set; } = "";
            public string Prioridade { get; set; } = "";
            public string Dificuldade { get; set; } = "";
            public string Data { get; set; } = "";
            public string Horario { get; set; } = "";
        }
    }
}
