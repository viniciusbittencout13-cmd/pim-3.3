using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public class JsonUserStore
    {
        private readonly string _filePath;

        public JsonUserStore()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var dataDir = Path.Combine(baseDir, "data");
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);

            _filePath = Path.Combine(dataDir, "usuarios.json");
        }

        // =======================
        // SEED: cria usuário padrão
        // =======================
        public static void EnsureSeedUser()
        {
            var store = new JsonUserStore();
            var usuarios = store.LoadAllInternal();

            if (usuarios.Any())
                return;

            var usuarioPadrao = new Usuario
            {
                Username = "vinicius",
                NomeCompleto = "Vinicius Bittencourt",
                // se o seu Nivel for string, ok; se for int, mude para 2
                Nivel = "Nível 2",
                Categoria = "Servidores / Rede",
                // IMPORTANTE: tipo de usuário para validar Nível 2 técnico
                Tipo = "Tecnico",
                PasswordHash = HashPassword("admin"),
                PrimeiroAcesso = true,
                Ativo = true,
                FraseSeguranca = "primeiro acesso"
            };

            usuarios.Add(usuarioPadrao);
            store.SaveAllInternal(usuarios);
        }

        // =======================
        // LOGIN
        // =======================
        public Usuario? ValidarLogin(string username, string senha)
        {
            var usuarios = LoadAllInternal();
            var senhaHash = HashPassword(senha);

            return usuarios.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                && u.PasswordHash == senhaHash
                && u.Ativo);
        }

        public Usuario? GetByUsername(string username)
        {
            return LoadAllInternal()
                .FirstOrDefault(u =>
                    u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public void Update(Usuario usuario)
        {
            var usuarios = LoadAllInternal();
            var existing = usuarios.FirstOrDefault(u =>
                u.Username.Equals(usuario.Username, StringComparison.OrdinalIgnoreCase));

            if (existing == null)
            {
                usuarios.Add(usuario);
            }
            else
            {
                existing.NomeCompleto = usuario.NomeCompleto;
                existing.Nivel = usuario.Nivel;
                existing.Categoria = usuario.Categoria;
                existing.PasswordHash = usuario.PasswordHash;
                existing.PrimeiroAcesso = usuario.PrimeiroAcesso;
                existing.Ativo = usuario.Ativo;
                existing.FraseSeguranca = usuario.FraseSeguranca;
                existing.Tipo = usuario.Tipo;
            }

            SaveAllInternal(usuarios);
        }

        private List<Usuario> LoadAllInternal()
        {
            if (!File.Exists(_filePath))
                return new List<Usuario>();

            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
                return new List<Usuario>();

            return JsonSerializer.Deserialize<List<Usuario>>(json)
                   ?? new List<Usuario>();
        }

        private void SaveAllInternal(List<Usuario> usuarios)
        {
            var json = JsonSerializer.Serialize(usuarios,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}
