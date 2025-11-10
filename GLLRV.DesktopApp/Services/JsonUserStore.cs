using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public class JsonUserStore
    {
        private readonly string _dir;
        private readonly string _file;

        public JsonUserStore()
        {
            _dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "usuarios");
            _file = Path.Combine(_dir, "usuarios.json");

            if (!Directory.Exists(_dir))
                Directory.CreateDirectory(_dir);
        }

        public void EnsureSeedUser()
        {
            if (!File.Exists(_file))
            {
                var admin = CreateDefaultUser();
                SaveUsers(new List<Usuario> { admin });
                return;
            }

            var users = LoadUsers();
            if (users.Count == 0)
            {
                var admin = CreateDefaultUser();
                SaveUsers(new List<Usuario> { admin });
            }
        }

        private static Usuario CreateDefaultUser()
        {
            return new Usuario
            {
                Username = "vinicius",
                NomeCompleto = "Vinicius Bittencourt",
                Nivel = "2",
                Categoria = "Servidores e gerenciamento de rede",
                SenhaHash = Auth.Sha256Hex("admin"),
                PrimeiroAcesso = true,
                FraseSeguranca = string.Empty
            };
        }

        public List<Usuario> LoadUsers()
        {
            if (!File.Exists(_file))
                return new List<Usuario>();

            var json = File.ReadAllText(_file);
            var list = JsonSerializer.Deserialize<List<Usuario>>(json);
            return list ?? new List<Usuario>();
        }

        public void SaveUsers(List<Usuario> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(_file, json);
        }

        public Usuario? GetByUsername(string username)
        {
            return LoadUsers()
                .FirstOrDefault(u =>
                    u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateUsuario(Usuario usuario)
        {
            var users = LoadUsers();
            var idx = users.FindIndex(u =>
                u.Username.Equals(usuario.Username, StringComparison.OrdinalIgnoreCase));

            if (idx >= 0)
                users[idx] = usuario;
            else
                users.Add(usuario);

            SaveUsers(users);
        }
    }
}
