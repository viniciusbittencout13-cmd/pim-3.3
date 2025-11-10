using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public class Auth
    {
        private readonly JsonUserStore _store = new JsonUserStore();

        public Usuario Login(string username, string password, out string error)
        {
            error = null;

            var user = _store.GetByUsername(username);
            if (user == null || !user.Ativo)
            {
                error = "Usuário não encontrado ou inativo.";
                return null;
            }

            var hash = JsonUserStore.HashPassword(password);
            if (user.PasswordHash != hash)
            {
                error = "Usuário não encontrado ou senha incorreta.";
                return null;
            }

            return user;
        }
    }
}
