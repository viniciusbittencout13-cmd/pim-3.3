using System.Windows;
using System.Windows.Controls;
using GLLRV.DesktopApp.Views.Pages.Usuarios;

namespace GLLRV.DesktopApp.Views.Pages
{
    public partial class UsuariosPage : UserControl
    {
        public UsuariosPage()
        {
            InitializeComponent();
        }

        private void CadastroClienteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow main)
            {
                main.MainContentFrame.Navigate(new CadastroClientePage());
            }
        }

        private void CadastroTecnicoButton_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow main)
            {
                main.MainContentFrame.Navigate(new CadastroTecnicoPage());
            }
        }

        private void EditarClienteButton_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow main)
            {
                main.MainContentFrame.Navigate(new EditarClientePage());
            }
        }

        private void EditarTecnicoButton_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow main)
            {
                main.MainContentFrame.Navigate(new EditarTecnicoPage());
            }
        }

        private void VoltarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow main)
            {
                main.MainContentFrame.Navigate(new ChamadosPendentesPage());
            }
        }
    }
}
