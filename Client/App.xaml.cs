using System.Windows;
using Client.Extensions;
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
            containerRegistry.RegisterServices();
            containerRegistry.RegisterViewsForNavigation();
        }
    }
}