using System;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public class Auth
    {
        private readonly JsonUserStore _store = new JsonUserStore();

        public Usuario? Autenticar(string username, string senha)
        {
            var user = _store.GetByUsername(username);
            if (user == null)
                return null;

            var hash = JsonUserStore.GerarHash(senha ?? string.Empty);
            if (!string.Equals(user.SenhaHash, hash, StringComparison.OrdinalIgnoreCase))
                return null;

            return user;
        }

        public bool AtualizarPrimeiroAcesso(Usuario usuario, string novaSenha, string fraseSeguranca)
        {
            if (usuario == null)
                return false;

            usuario.SenhaHash = JsonUserStore.GerarHash(novaSenha);
            usuario.FraseSeguranca = fraseSeguranca ?? string.Empty;
            usuario.PrimeiroAcesso = false;

            _store.Update(usuario);
            return true;
        }
    }
}
