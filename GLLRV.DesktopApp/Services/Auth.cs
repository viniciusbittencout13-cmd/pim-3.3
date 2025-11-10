using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public class Auth
    {
        private readonly JsonUserStore _store = new JsonUserStore();

        // Autenticação principal
        public Usuario? Autenticar(string username, string password, out string mensagemErro)
        {
            mensagemErro = string.Empty;

            var user = _store.GetByUsername(username);
            if (user == null || !user.Ativo)
            {
                mensagemErro = "Usuário não encontrado ou inativo.";
                return null;
            }

            var hash = JsonUserStore.HashPassword(password);
            if (user.PasswordHash != hash)
            {
                mensagemErro = "Usuário não encontrado ou senha incorreta.";
                return null;
            }

            return user;
        }

        // Versão sem mensagem de erro (compatibilidade)
        public Usuario? Autenticar(string username, string password)
        {
            return Autenticar(username, password, out _);
        }

        // Alias compatível com código antigo
        public Usuario? Login(string username, string password, out string mensagemErro)
        {
            return Autenticar(username, password, out mensagemErro);
        }

        // Usado na tela de primeiro acesso:
        // atualiza senha, frase de segurança e marca que não é mais primeiro acesso.
        public bool AtualizarPrimeiroAcesso(Usuario usuario, string novaSenha, string fraseSeguranca)
        {
            if (usuario == null) return false;
            if (string.IsNullOrWhiteSpace(novaSenha)) return false;

            usuario.PasswordHash = JsonUserStore.HashPassword(novaSenha);
            usuario.FraseSeguranca = fraseSeguranca ?? string.Empty;
            usuario.PrimeiroAcesso = false;

            _store.Update(usuario);
            return true;
        }
    }
}
