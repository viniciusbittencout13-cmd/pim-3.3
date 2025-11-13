using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GLLRV.DesktopApp.Models;

namespace GLLRV.DesktopApp.Services
{
    public static class JsonDataStore
    {
        private static readonly string BaseDir =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

        private static readonly string ChamadosFile =
            Path.Combine(BaseDir, "chamados.json");

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        static JsonDataStore()
        {
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);

            // Se o arquivo não existe, cria com alguns chamados base
            if (!File.Exists(ChamadosFile))
            {
                var seed = new List<Chamado>
                {
                    new()
                    {
                        Id = 1,
                        Titulo = "Servidor AD fora do ar",
                        Descricao = "Sem autenticação de usuários.",
                        Status = "Pendente",
                        Responsavel = "Vinicius",
                        Solicitante = "Infraestrutura",
                        DataAbertura = DateTime.Now.AddHours(-3)
                    },
                    new()
                    {
                        Id = 2,
                        Titulo = "VPN intermitente",
                        Descricao = "Queda de túnel para matriz.",
                        Status = "Andamento",
                        Responsavel = "Vinicius",
                        Solicitante = "Filial RJ",
                        DataAbertura = DateTime.Now.AddHours(-5)
                    },
                    new()
                    {
                        Id = 3,
                        Titulo = "Impressora departamento financeiro",
                        Descricao = "Sem impressão desde ontem.",
                        Status = "Fechado",
                        Responsavel = "Suporte N1",
                        Solicitante = "Financeiro",
                        DataAbertura = DateTime.Now.AddDays(-2),
                        DataFechamento = DateTime.Now.AddDays(-1)
                    }
                };

                SaveChamados(seed);
            }

            // Sempre garante os 3 chamados fechados (Vinicius, Gustavo, Ralyson)
            EnsureHistoricoBase();
        }

        public static List<Chamado> LoadChamados()
        {
            if (!File.Exists(ChamadosFile))
                return new List<Chamado>();

            var json = File.ReadAllText(ChamadosFile);
            return JsonSerializer.Deserialize<List<Chamado>>(json, JsonOptions)
                   ?? new List<Chamado>();
        }

        public static void SaveChamados(List<Chamado> chamados)
        {
            if (!Directory.Exists(BaseDir))
                Directory.CreateDirectory(BaseDir);

            var json = JsonSerializer.Serialize(chamados, JsonOptions);
            File.WriteAllText(ChamadosFile, json);
        }

        public static IEnumerable<Chamado> GetPendentes() =>
            LoadChamados().Where(c => c.Status.Equals("Pendente", StringComparison.OrdinalIgnoreCase));

        public static IEnumerable<Chamado> GetAndamento() =>
            LoadChamados().Where(c => c.Status.Equals("Andamento", StringComparison.OrdinalIgnoreCase));

        public static IEnumerable<Chamado> GetHistorico() =>
            LoadChamados().Where(c => c.Status.Equals("Fechado", StringComparison.OrdinalIgnoreCase));

        // ---------- NOVO: garante 3 chamados fechados no histórico ----------
        private static void EnsureHistoricoBase()
        {
            var list = LoadChamados();

            bool hasVini   = list.Any(c => c.Status == "Fechado" && c.Responsavel.Equals("Vinicius", StringComparison.OrdinalIgnoreCase));
            bool hasGus    = list.Any(c => c.Status == "Fechado" && c.Responsavel.Equals("Gustavo",  StringComparison.OrdinalIgnoreCase));
            bool hasRaly   = list.Any(c => c.Status == "Fechado" && c.Responsavel.Equals("Ralyson",  StringComparison.OrdinalIgnoreCase));

            if (hasVini && hasGus && hasRaly)
                return;

            int nextId = list.Any() ? list.Max(c => c.Id) + 1 : 1;

            if (!hasVini)
            {
                list.Add(new Chamado
                {
                    Id = nextId++,
                    Titulo = "Restabelecimento de acesso Wi-Fi",
                    Descricao = "Ajuste de política e rotação de senha.",
                    Status = "Fechado",
                    Responsavel = "Vinicius",
                    Solicitante = "Andar 3",
                    DataAbertura = new DateTime(2025, 11, 10, 9, 15, 0),
                    DataFechamento = new DateTime(2025, 11, 10, 10, 15, 0)
                });
            }

            if (!hasGus)
            {
                list.Add(new Chamado
                {
                    Id = nextId++,
                    Titulo = "Atualização de drivers estação CAD",
                    Descricao = "Driver de GPU e pacote DirectX.",
                    Status = "Fechado",
                    Responsavel = "Gustavo",
                    Solicitante = "Engenharia",
                    DataAbertura = new DateTime(2025, 11, 11, 13, 30, 0),
                    DataFechamento = new DateTime(2025, 11, 11, 14, 45, 0)
                });
            }

            if (!hasRaly)
            {
                list.Add(new Chamado
                {
                    Id = nextId++,
                    Titulo = "Correção de perfil no Outlook",
                    Descricao = "Recriado perfil e reindexado pesquisa.",
                    Status = "Fechado",
                    Responsavel = "Ralyson",
                    Solicitante = "Comercial",
                    DataAbertura = new DateTime(2025, 11, 9, 8, 30, 0),
                    DataFechamento = new DateTime(2025, 11, 9, 9, 0, 0)
                });
            }

            SaveChamados(list);
        }
    }
}
