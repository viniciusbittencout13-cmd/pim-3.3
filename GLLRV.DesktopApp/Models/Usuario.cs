namespace GLLRV.DesktopApp.Models
{
    public class Usuario
    {
        public string Username { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public string FraseSeguranca { get; set; } = string.Empty;
        public bool PrimeiroAcesso { get; set; } = true;
    }
}
