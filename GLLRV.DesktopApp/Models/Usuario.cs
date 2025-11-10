using System.Text.Json.Serialization;

namespace GLLRV.DesktopApp.Models
{
    public class Usuario
    {
        // Login / identificação
        public string NomeUsuario { get; set; } = string.Empty;   // usado no código novo
        public string NomeCompleto { get; set; } = string.Empty;

        // Segurança
        public string SenhaHash { get; set; } = string.Empty;
        public string FraseSeguranca { get; set; } = string.Empty;
        public bool PrimeiroAcesso { get; set; } = true;

        // Informações do técnico
        public string Nivel { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;

        // Alias para manter compatibilidade com código antigo que usa "Username"
        [JsonIgnore]
        public string Username
        {
            get => NomeUsuario;
            set => NomeUsuario = value;
        }
    }
}
