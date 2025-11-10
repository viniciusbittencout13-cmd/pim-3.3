using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public class Auth
    {
        private readonly JsonUserStore _store = new JsonUserStore();

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

        // Se em algum lugar chamar Login, deixa como alias:
        public Usuario Login(string username, string password, out string mensagemErro)
            => Autenticar(username, password, out mensagemErro);
    }
}
