using System.Windows;
using GLLRV.DesktopApp.Services;
using GLLRV.DesktopApp.Views;

namespace GLLRV.DesktopApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            JsonUserStore.EnsureSeedUser();

            var login = new LoginWindow();
            login.Show();
        }
    }
}
