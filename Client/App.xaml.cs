using System.Windows;
using Client.Core;
using Client.Views;
using Prism.DryIoc;
using Prism.Ioc;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            containerRegistry.RegisterForNavigation<LoginView>(NavigationPaths.LoginPath);
            containerRegistry.RegisterForNavigation<RegisterView>(NavigationPaths.RegisterPath);
        }
    }
}