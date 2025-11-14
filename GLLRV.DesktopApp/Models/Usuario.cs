namespace GLLRV.DesktopApp.Models
{
    public class Usuario
    {
        // Login (login / usuário)
        public string Username { get; set; }

        // Alias para código antigo (usa Username por baixo)
        public string NomeUsuario
        {
            get => Username;
            set => Username = value;
        }

        // Nome para exibir
        public string NomeCompleto { get; set; }

        // Informações extras
        public string Nivel { get; set; }
        public string Categoria { get; set; }

        // Senha (hash)
        public string PasswordHash { get; set; }

        // Se é primeiro acesso (força trocar senha)
        public bool PrimeiroAcesso { get; set; }

        // Se pode logar
        public bool Ativo { get; set; }

        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string FraseSegurancaHash { get; set; } = string.Empty;
        public string FraseSegurancaSalt { get; set; } = string.Empty;
    }
}
