using System.Text.Json.Serialization;

namespace GLLRV.DesktopApp.Models
{
    public class Usuario
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;

        public string SenhaHash { get; set; } = string.Empty;
        public string FraseSeguranca { get; set; } = string.Empty;
        public bool PrimeiroAcesso { get; set; } = true;

        public string Nivel { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;

        // Compatibilidade com código antigo
        [JsonIgnore]
        public string Username
        {
            get => NomeUsuario;
            set => NomeUsuario = value;
        }
    }
}
