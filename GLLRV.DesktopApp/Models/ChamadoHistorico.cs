namespace GLLRV.DesktopApp.Models
{
    public class ChamadoHistorico
    {
        public string Usuario { get; set; } = "";
        public string NivelPrioridade { get; set; } = "";
        public string Dificuldade { get; set; } = "";
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public TimeSpan HorarioInicio { get; set; }
        public TimeSpan HorarioTermino { get; set; }

        // Guardamos o técnico responsável (não aparece na sua tabela, mas fica disponível)
        public string Tecnico { get; set; } = "";
    }
}
