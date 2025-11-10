using System.Windows;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Garante um usuário padrão na primeira execução
            var store = new JsonUserStore();
            store.EnsureSeedUser();
        }
    }
}
