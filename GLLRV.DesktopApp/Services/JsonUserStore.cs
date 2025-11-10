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
                Nivel = "Nível 2",
                Categoria = "Servidores / Rede",
                PasswordHash = HashPassword("admin"),
                PrimeiroAcesso = true,
                Ativo = true
            };

            usuarios.Add(usuarioPadrao);
            store.SaveAllInternal(usuarios);
        }

        public Usuario GetByUsername(string username)
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
                existing.PrimeiroA
