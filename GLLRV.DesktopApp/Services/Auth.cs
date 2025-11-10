using System.Security.Cryptography;
using System.Text;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public static class Auth
    {
        public static Usuario? UsuarioLogado { get; private set; }

        public static string Sha256Hex(string input)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            var sb = new StringBuilder(hash.Length * 2);
            foreach (var b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public static Usuario? Login(string username, string plainPassword, JsonUserStore store)
        {
            var user = store.GetByUsername(username);
            if (user == null)
                return null;

            var hash = Sha256Hex(plainPassword);
            if (!string.Equals(user.SenhaHash, hash, System.StringComparison.OrdinalIgnoreCase))
                return null;

            UsuarioLogado = user;
            return user;
        }

        public static void AtualizarUsuario(Usuario usuario, JsonUserStore store)
        {
            store.UpdateUsuario(usuario);
            UsuarioLogado = usuario;
        }
    }
}
