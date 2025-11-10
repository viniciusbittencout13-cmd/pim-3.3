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
            EnsureSeedUser();
        }

        private List<Usuario> LoadAll()
        {
            if (!File.Exists(_filePath))
                return new List<Usuario>();

            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
                return new List<Usuario>();

            try
            {
                var list = JsonSerializer.Deserialize<List<Usuario>>(json);
                return list ?? new List<Usuario>();
            }
            catch
            {
                return new List<Usuario>();
            }
        }

        private void SaveAll(List<Usuario> usuarios)
        {
            var json = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(_filePath, json);
        }

        public Usuario? GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var usuarios = LoadAll();
            return usuarios
                .FirstOrDefault(u =>
                    string.Equals(u.NomeUsuario, username, StringComparison.OrdinalIgnoreCase));
        }

        public void Update(Usuario usuario)
        {
            var usuarios = LoadAll();

            var existing = usuarios.FirstOrDefault(u =>
                string.Equals(u.NomeUsuario, usuario.NomeUsuario, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                existing.NomeCompleto = usuario.NomeCompleto;
                existing.SenhaHash = usuario.SenhaHash;
                existing.FraseSeguranca = usuario.FraseSeguranca;
                existing.PrimeiroAcesso = usuario.PrimeiroAcesso;
                existing.Nivel = usuario.Nivel;
                existing.Categoria = usuario.Categoria;
            }
            else
            {
                usuarios.Add(usuario);
            }

            SaveAll(usuarios);
        }

        public static string GerarHash(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return string.Empty;

            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(texto);
            var hash = sha.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        private void EnsureSeedUser()
        {
            var usuarios = LoadAll();
            if (usuarios.Any())
                return;

            var admin = new Usuario
            {
                NomeUsuario = "vinicius",
                NomeCompleto = "Vinicius Bittencourt",
                SenhaHash = GerarHash("123456"),
                FraseSeguranca = "meu primeiro acesso",
                PrimeiroAcesso = true,
                Nivel = "Nível 2",
                Categoria = "Servidores e Gerenciamento de Rede"
            };

            usuarios.Add(admin);
            SaveAll(usuarios);
        }
    }
}
