using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public static class JsonUserStore
    {
        private static readonly string BaseDir =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

        private static readonly string UsersFile =
            Path.Combine(BaseDir, "usuarios.json");

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        static JsonUserStore()
        {
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);

            if (!File.Exists(UsersFile))
            {
                // SEED: cria um técnico nível 2 e um cliente
                var seed = new List<Usuario>
                {
                    new()
                    {
                        Id = 1,
                        NomeCompleto = "Vinicius Técnico",
                        Username = "vinicius",
                        Tipo = "Tecnico",
                        Nivel = 2,
                        Categoria = "Servidores / Rede",
                        PasswordHash = "1234",         // por enquanto simples
                        FraseSeguranca = "primeiro acesso"
                    },
                    new()
                    {
                        Id = 2,
                        NomeCompleto = "Cliente Teste",
                        Username = "cliente",
                        Tipo = "Cliente",
                        Nivel = 0,
                        Categoria = "Usuário Final",
                        PasswordHash = "1234",
                        FraseSeguranca = "primeiro acesso"
                    }
                };

                SaveUsuarios(seed);
            }
        }

        public static List<Usuario> LoadUsuarios()
        {
            if (!File.Exists(UsersFile))
                return new List<Usuario>();

            var json = File.ReadAllText(UsersFile);
            return JsonSerializer.Deserialize<List<Usuario>>(json, JsonOptions)
                   ?? new List<Usuario>();
        }

        public static void SaveUsuarios(List<Usuario> usuarios)
        {
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);

            var json = JsonSerializer.Serialize(usuarios, JsonOptions);
            File.WriteAllText(UsersFile, json);
        }

        /// <summary>
        /// Valida login pelo username e senha.
        /// Retorna o usuário ou null se não encontrar.
        /// </summary>
        public static Usuario? ValidarLogin(string username, string senha)
        {
            var usuarios = LoadUsuarios();

            return usuarios.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                && u.PasswordHash == senha);
        }
    }
}
