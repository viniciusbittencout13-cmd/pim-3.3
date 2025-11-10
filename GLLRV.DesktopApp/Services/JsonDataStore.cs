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

            if (!File.Exists(ChamadosFile))
            {
                // alguns chamados fake só pra lista não ficar vazia
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
    }
}
