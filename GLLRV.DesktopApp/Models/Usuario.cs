namespace GLLRV.DesktopApp.Models
{
    public class Usuario
    {
        public string Username { get; set; }          // login
        public string NomeCompleto { get; set; }      // nome exibido
        public string Nivel { get; set; }             // Nível 1, 2...
        public string Categoria { get; set; }         // área
        public string PasswordHash { get; set; }      // senha (hash)
        public bool PrimeiroAcesso { get; set; }      // força troca de senha
        public bool Ativo { get; set; }               // se pode logar
    }
}
