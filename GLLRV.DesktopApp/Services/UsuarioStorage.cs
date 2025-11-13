using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace GLLRV.DesktopApp.Services
{
    // Técnico da área de suporte
    public class TecnicoInfo
    {
        public string Cpf { get; set; } = "";
        public string NomeCompleto { get; set; } = "";
        public string Telefone { get; set; } = "";
        public string NivelTecnico { get; set; } = "";
        public string NomeUsuario { get; set; } = "";
        public string SenhaPrimeiroAcesso { get; set; } = "";
        public string Email { get; set; } = "";
        public string CategoriaChamados { get; set; } = "";
    }

    // Cliente (usuário final)
    public class ClienteInfo
    {
        public string Cpf { get; set; } = "";
        public string NomeCompleto { get; set; } = "";
        public string Telefone { get; set; } = "";
        public string Funcao { get; set; } = "";
        public string NomeUsuario { get; set; } = "";
        public string SenhaPrimeiroAcesso { get; set; } = "";
        public string Email { get; set; } = "";
    }

    public static class UsuarioStorage
    {
        private static readonly string BaseDir =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

        private static readonly string TecnicosFile =
            Path.Combine(BaseDir, "tecnicos.json");

        private static readonly string ClientesFile =
            Path.Combine(BaseDir, "clientes.json");

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        static UsuarioStorage()
        {
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);

            if (!File.Exists(TecnicosFile))
                SaveTecnicos(new List<TecnicoInfo>());

            if (!File.Exists(ClientesFile))
                SaveClientes(new List<ClienteInfo>());
        }

        // --------- Técnicos ---------

        public static List<TecnicoInfo> LoadTecnicos()
        {
            if (!File.Exists(TecnicosFile))
                return new List<TecnicoInfo>();

            var json = File.ReadAllText(TecnicosFile);
            return JsonSerializer.Deserialize<List<TecnicoInfo>>(json, JsonOptions)
                   ?? new List<TecnicoInfo>();
        }

        public static void SaveTecnicos(List<TecnicoInfo> lista)
        {
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);

            var json = JsonSerializer.Serialize(lista, JsonOptions);
            File.WriteAllText(TecnicosFile, json);
        }

        public static void AddOrUpdateTecnico(TecnicoInfo tecnico)
        {
            var lista = LoadTecnicos();

            var existente = lista.FirstOrDefault(t => t.Cpf == tecnico.Cpf);
            if (existente == null)
            {
                lista.Add(tecnico);
            }
            else
            {
                existente.NomeCompleto       = tecnico.NomeCompleto;
                existente.Telefone           = tecnico.Telefone;
                existente.NivelTecnico       = tecnico.NivelTecnico;
                existente.NomeUsuario        = tecnico.NomeUsuario;
                existente.SenhaPrimeiroAcesso = tecnico.SenhaPrimeiroAcesso;
                existente.Email              = tecnico.Email;
                existente.CategoriaChamados  = tecnico.CategoriaChamados;
            }

            SaveTecnicos(lista);
        }

        public static TecnicoInfo? GetTecnicoPorCpf(string cpf)
        {
            return LoadTecnicos().FirstOrDefault(t => t.Cpf == cpf);
        }

        // --------- Clientes ---------

        public static List<ClienteInfo> LoadClientes()
        {
            if (!File.Exists(ClientesFile))
                return new List<ClienteInfo>();

            var json = File.ReadAllText(ClientesFile);
            return JsonSerializer.Deserialize<List<ClienteInfo>>(json, JsonOptions)
                   ?? new List<ClienteInfo>();
        }

        public static void SaveClientes(List<ClienteInfo> lista)
        {
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);

            var json = JsonSerializer.Serialize(lista, JsonOptions);
            File.WriteAllText(ClientesFile, json);
        }

        public static void AddOrUpdateCliente(ClienteInfo cliente)
        {
            var lista = LoadClientes();

            var existente = lista.FirstOrDefault(t => t.Cpf == cliente.Cpf);
            if (existente == null)
            {
                lista.Add(cliente);
            }
            else
            {
                existente.NomeCompleto        = cliente.NomeCompleto;
                existente.Telefone            = cliente.Telefone;
                existente.Funcao              = cliente.Funcao;
                existente.NomeUsuario         = cliente.NomeUsuario;
                existente.SenhaPrimeiroAcesso = cliente.SenhaPrimeiroAcesso;
                existente.Email               = cliente.Email;
            }

            SaveClientes(lista);
        }

        public static ClienteInfo? GetClientePorCpf(string cpf)
        {
            return LoadClientes().FirstOrDefault(c => c.Cpf == cpf);
        }
    }
}
