namespace GLLRV.DesktopApp.Models
{
    public class Usuario
    {
        public string NomeUsuario { get; set; } = "";
        public string SenhaHash { get; set; } = "";
        public string NomeCompleto { get; set; } = "";
        public string Nivel { get; set; } = "";
        public string Categoria { get; set; } = "";
        public string FraseSeguranca { get; set; } = "";
    }
}
