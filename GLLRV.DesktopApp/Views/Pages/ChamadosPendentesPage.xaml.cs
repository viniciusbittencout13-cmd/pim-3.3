using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace GLLRV.DesktopApp.Views.Pages
{
    public partial class ChamadosPendentesPage : UserControl
    {
        private List<ChamadoListaItem> _todosChamados = new();

        public ChamadosPendentesPage()
        {
            InitializeComponent();
            CarregarChamadosFake(); // depois trocamos pro JSON real
            AplicarFiltros();
        }

        // ---- Dados mock só pra UI ficar viva ----
        private void CarregarChamadosFake()
        {
            _todosChamados = new List<ChamadoListaItem>
            {
                new() { Numero = "CH-0001", Cliente = "Empresa Alpha", Assunto = "Erro ao acessar sistema", Prioridade = "Alta",    DataAbertura = "10/11/2025 09:15", SLA = "Vencendo hoje", Status = "Pendente" },
                new() { Numero = "CH-0002", Cliente = "Loja Beta",     Assunto = "Impressora offline",      Prioridade = "Média",  DataAbertura = "09/11/2025 14:02", SLA = "Dentro do prazo", Status = "Pendente" },
                new() { Numero = "CH-0003", Cliente = "Clínica Gama",  Assunto = "Servidor não liga",       Prioridade = "Crítica",DataAbertura = "10/11/2025 08:01", SLA = "Vencido", Status = "Pendente" },
                new() { Numero = "CH-0004", Cliente = "Escritório Z",  Assunto = "Backup com falha",       Prioridade = "Alta",    DataAbertura = "08/11/2025 16:45", SLA = "Dentro do prazo", Status = "Pendente" }
            };
        }

        // ---- Filtros / busca ----
        private void AplicarFiltros()
        {
            var textoBusca = (SearchBox.Text ?? "").Trim().ToLowerInvariant();

            var prioridade = (PrioridadeFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();
            var sla = (SlaFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();

            var query = _todosChamados.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(textoBusca))
            {
                query = query.Where(c =>
                    (c.Numero ?? "").ToLower().Contains(textoBusca) ||
                    (c.Cliente ?? "").ToLower().Contains(textoBusca) ||
                    (c.Assunto ?? "").ToLower().Contains(textoBusca));
            }

            if (!string.IsNullOrWhiteSpace(prioridade) && !prioridade.StartsWith("Todas"))
            {
                query = query.Where(c => string.Equals(c.Prioridade, prioridade, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(sla) && !sla.StartsWith("Todos"))
            {
                if (sla.Contains("Vencidos"))
                    query = query.Where(c => c.SLA.Contains("Vencido", StringComparison.OrdinalIgnoreCase));
                else if (sla.Contains("Vencendo hoje"))
                    query = query.Where(c => c.SLA.Contains("Vencendo hoje", StringComparison.OrdinalIgnoreCase));
                else if (sla.Contains("Dentro do prazo"))
                    query = query.Where(c => c.SLA.Contains("Dentro do prazo", StringComparison.OrdinalIgnoreCase));
            }

            ChamadosGrid.ItemsSource = query.ToList();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            AplicarFiltros();
        }

        private void Filtro_Changed(object sender, SelectionChangedEventArgs e)
        {
            AplicarFiltros();
        }

        // Classe só para montar a lista (não conflita com seu Model Chamado)
        private class ChamadoListaItem
        {
            public string Numero { get; set; } = "";
            public string Cliente { get; set; } = "";
            public string Assunto { get; set; } = "";
            public string Prioridade { get; set; } = "";
            public string DataAbertura { get; set; } = "";
            public string SLA { get; set; } = "";
            public string Status { get; set; } = "";
        }
    }
}
