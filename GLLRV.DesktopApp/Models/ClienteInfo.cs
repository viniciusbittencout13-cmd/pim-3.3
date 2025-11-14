using System;

namespace GLLRV.DesktopApp.Models
{
    public class ClienteInfo
    {
        public string Cpf { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Funcao { get; set; } = string.Empty;
        public string NomeUsuario { get; set; } = string.Empty;
        public string SenhaPrimeiroAcesso { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
