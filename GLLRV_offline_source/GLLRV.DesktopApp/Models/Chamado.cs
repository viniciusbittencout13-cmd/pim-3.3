using System;

namespace GLLRV.DesktopApp.Models
{
    public class Chamado
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Descricao { get; set; } = "";
        public string Status { get; set; } = ""; // Pendente, Andamento, Fechado
        public string Responsavel { get; set; } = "";
        public string Solicitante { get; set; } = "";
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
    }
}
