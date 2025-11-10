using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public class Auth
    {
        private readonly JsonUserStore _store = new JsonUserStore();

        // Método principal de autenticação
        public Usuario Autenticar(string username, string password, out string mensagemErro)
        {
            mensagemErro = null;

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

        // Alias compatível (sem mensagemErro)
        public Usuario Autenticar(string username, string password)
        {
            string erro;
            return Autenticar(username, password, out erro);
        }

        // Atualiza o status de primeiro acesso (usado ao trocar a senha)
        public void AtualizarPrimeiroAcesso(Usuario usuario)
        {
            if (usuario == null) return;

            usuario.PrimeiroAcesso = false;
            _store.Update(usuario);
        }

        // Método compatível com código legado
        public Usuario Login(string username, string password, out string mensagemErro)
            => Autenticar(username, password, out mensagemErro);
    }
}
