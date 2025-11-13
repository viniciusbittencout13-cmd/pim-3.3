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
                File.WriteAllText(TecnicosFile, "[]");

            if (!File.Exists(ClientesFile))
                File.WriteAllText(ClientesFile, "[]");
        }

        // ----------------- TÉCNICOS -----------------

        public static List<TecnicoInfo> CarregarTecnicos()
        {
            if (!File.Exists(TecnicosFile))
                return new List<TecnicoInfo>();

            var json = File.ReadAllText(TecnicosFile);
            return JsonSerializer.Deserialize<List<TecnicoInfo>>(json, JsonOptions)
                   ?? new List<TecnicoInfo>();
        }

        public static void SalvarTecnicos(List<TecnicoInfo> tecnicos)
        {
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);

            var json = JsonSerializer.Serialize(tecnicos, JsonOptions);
            File.WriteAllText(TecnicosFile, json);
        }

        // Usado no cadastro e na edição
        public static void SalvarOuAtualizarTecnico(TecnicoInfo tecnico)
        {
            var lista = CarregarTecnicos();

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

            SalvarTecnicos(lista);
        }

        public static TecnicoInfo? ObterTecnicoPorCpf(string cpf)
        {
            return CarregarTecnicos()
                .FirstOrDefault(t =>
                    t.Cpf.Equals(cpf, StringComparison.OrdinalIgnoreCase));
        }

        public static TecnicoInfo? ObterTecnicoPorNomeUsuario(string username)
        {
            return CarregarTecnicos()
                .FirstOrDefault(t =>
                    t.NomeUsuario.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        // ----------------- CLIENTES -----------------

        public static List<ClienteInfo> CarregarClientes()
        {
            if (!File.Exists(ClientesFile))
                return new List<ClienteInfo>();

            var json = File.ReadAllText(ClientesFile);
            return JsonSerializer.Deserialize<List<ClienteInfo>>(json, JsonOptions)
                   ?? new List<ClienteInfo>();
        }

        public static void SalvarClientes(List<ClienteInfo> clientes)
        {
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);

            var json = JsonSerializer.Serialize(clientes, JsonOptions);
            File.WriteAllText(ClientesFile, json);
        }

        // Se quiser, depois podemos criar SalvarOuAtualizarCliente, ObterClientePorCpf etc.
    }
}
