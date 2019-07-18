using System.Windows;
using Client.Services;
using Client.Views;
using Prism.Ioc;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            containerRegistry.RegisterForNavigation<LoginView>(NavigationSources.LoginSource);
            containerRegistry.RegisterForNavigation<ViewB>(NavigationSources.RegisterSource);
        }
    }
}