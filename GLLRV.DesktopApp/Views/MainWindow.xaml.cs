using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using GLLRV.DesktopApp.Models;
using GLLRV.DesktopApp.Views.Pages;

namespace GLLRV.DesktopApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly Usuario _usuario;

        public MainWindow(Usuario usuario)
        {
            InitializeComponent();

            _usuario = usuario ?? new Usuario
            {
                NomeUsuario = "vinicius",
                NomeCompleto = "Vinicius Bittencourt",
                Nivel = "Nível 2",
                Categoria = "Servidores / Rede"
            };

            Loaded += MainWindow_Loaded;
        }

        public MainWindow()
            : this(new Usuario
            {
                NomeUsuario = "vinicius",
                NomeCompleto = "Vinicius Bittencourt",
                Nivel = "Nível 2",
                Categoria = "Servidores / Rede"
            })
        {
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarUsuarioNoTopo();
            IniciarRelogio();
            AbrirChamadosPendentes();
        }

        private void CarregarUsuarioNoTopo()
        {
            if (_usuario == null) return;

            UserNameText.Text = _usuario.NomeCompleto ?? _usuario.NomeUsuario ?? "-";
            UserLevelText.Text = $"TÉCNICO: {_usuario.Nivel ?? "-"}";
            UserCategoryText.Text = $"CATEGORIA: {_usuario.Categoria ?? "-"}";

            var nome = _usuario.NomeCompleto ?? _usuario.NomeUsuario ?? "";
            var partes = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string iniciais = "?";
            if (partes.Length >= 2)
                iniciais = $"{partes[0][0]}{partes[1][0]}";
            else if (partes.Length == 1)
                iniciais = partes[0][0].ToString();

            InitialsText.Text = iniciais.ToUpperInvariant();
        }

        private void IniciarRelogio()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += (_, _) =>
            {
                DateTimeText.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
            };

            timer.Start();
        }

        // ---------- Navegação ----------

        private void AbrirChamadosPendentes()
        {
            try
            {
                ContentArea.Children.Clear();
                ContentArea.Children.Add(new ChamadosPendentesPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao abrir Chamados Pendentes:\n{ex.Message}",
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void ChamadosPendentesButton_Click(object sender, RoutedEventArgs e)
            => AbrirChamadosPendentes();

        private void UsuariosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new TextBlock
            {
                Text = "Usuários (tela em construção)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            });
        }

        private void RelatoriosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new TextBlock
            {
                Text = "Relatórios (tela em construção)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            });
        }

        private void HistoricoChamadosButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new TextBlock
            {
                Text = "Histórico de chamados (tela em construção)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            });
        }

        private void ChamadosAndamentoButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new TextBlock
            {
                Text = "Chamados em andamento (tela em construção)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            });
        }

        private void ConfiguracaoButton_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new TextBlock
            {
                Text = "Configurações (tela em construção)",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 18
            });
        }

        private void SairButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
