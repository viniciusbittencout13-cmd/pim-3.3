using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public static class UsuarioStorage
    {
        // POCO usado para gravar/ler o arquivo tecnicos.json
        public class TecnicoInfo
        {
            public string Cpf { get; set; } = string.Empty;
            public string NomeCompleto { get; set; } = string.Empty;
            public string Telefone { get; set; } = string.Empty;
            public string NivelTecnico { get; set; } = string.Empty;
            public string NomeUsuario { get; set; } = string.Empty;
            public string SenhaPrimeiroAcesso { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string CategoriaChamados { get; set; } = string.Empty;
        }

        private static readonly string BaseDir =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

        private static readonly string TecnicosFile =
            Path.Combine(BaseDir, "tecnicos.json");

        // novo arquivo para clientes
        private static readonly string ClientesFile =
            Path.Combine(BaseDir, "clientes.json");

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        // ================== TÉCNICOS ==================

        private static List<TecnicoInfo> LoadTecnicos()
        {
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);

            if (!File.Exists(TecnicosFile))
                return new List<TecnicoInfo>();

            var json = File.ReadAllText(TecnicosFile);
            if (string.IsNullOrWhiteSpace(json))
                return new List<TecnicoInfo>();

            return JsonSerializer.Deserialize<List<TecnicoInfo>>(json, JsonOptions)
                   ?? new List<TecnicoInfo>();
        }

        private static void SaveTecnicos(List<TecnicoInfo> tecnicos)
        {
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);

            var json = JsonSerializer.Serialize(tecnicos, JsonOptions);
            File.WriteAllText(TecnicosFile, json);
        }

        public static TecnicoInfo? ObterTecnicoPorCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return null;

            var todos = LoadTecnicos();
            return todos.FirstOrDefault(t =>
                t.Cpf.Equals(cpf, StringComparison.OrdinalIgnoreCase));
        }

        public static void SalvarOuAtualizarTecnico(TecnicoInfo tecnico)
        {
            if (tecnico == null) return;

            var lista = LoadTecnicos();
            var existente = lista.FirstOrDefault(t =>
                t.Cpf.Equals(tecnico.Cpf, StringComparison.OrdinalIgnoreCase));

            if (existente == null)
            {
                lista.Add(tecnico);
            }
            else
            {
                existente.NomeCompleto        = tecnico.NomeCompleto;
                existente.Telefone            = tecnico.Telefone;
                existente.NivelTecnico        = tecnico.NivelTecnico;
                existente.NomeUsuario         = tecnico.NomeUsuario;
                existente.SenhaPrimeiroAcesso = tecnico.SenhaPrimeiroAcesso;
                existente.Email               = tecnico.Email;
                existente.CategoriaChamados   = tecnico.CategoriaChamados;
            }

            SaveTecnicos(lista);
        }

        // ================== CLIENTES ==================

        private static List<ClienteInfo> LoadClientes()
        {
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);

            if (!File.Exists(ClientesFile))
                return new List<ClienteInfo>();

            var json = File.ReadAllText(ClientesFile);
            if (string.IsNullOrWhiteSpace(json))
                return new List<ClienteInfo>();

            return JsonSerializer.Deserialize<List<ClienteInfo>>(json, JsonOptions)
                   ?? new List<ClienteInfo>();
        }

        private static void SaveClientes(List<ClienteInfo> clientes)
        {
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);

            var json = JsonSerializer.Serialize(clientes, JsonOptions);
            File.WriteAllText(ClientesFile, json);
        }

        // usado na tela EditarClientePage
        public static ClienteInfo? GetClientePorCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return null;

            var todos = LoadClientes();
            return todos.FirstOrDefault(c =>
                c.Cpf.Equals(cpf, StringComparison.OrdinalIgnoreCase));
        }

        // usado em CadastroClientePage / EditarClientePage
        public static void AddOrUpdateCliente(ClienteInfo cliente)
        {
            if (cliente == null) return;

            var lista = LoadClientes();
            var existente = lista.FirstOrDefault(c =>
                c.Cpf.Equals(cliente.Cpf, StringComparison.OrdinalIgnoreCase));

            if (existente == null)
            {
                lista.Add(cliente);
            }
            else
            {
                existente.NomeCompleto        = cliente.NomeCompleto;
                existente.Funcao              = cliente.Funcao;
                existente.NomeUsuario         = cliente.NomeUsuario;
                existente.SenhaPrimeiroAcesso = cliente.SenhaPrimeiroAcesso;
                existente.Email               = cliente.Email;
            }

            SaveClientes(lista);
        }
    }
}
