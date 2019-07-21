using Client.Core;
using Client.Infrastructure;
using Client.Views;
using Prism.Ioc;

namespace Client.Extensions
{
    public static class ContainerRegistryExtensions
    {
        public static void RegisterServices(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
        }

        public static void RegisterViewsForNavigation(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AdminNavView>(NavigationPaths.AdminNavPath);
            containerRegistry.RegisterForNavigation<AdminView>(NavigationPaths.AdminPath);
            containerRegistry.RegisterForNavigation<LoginView>(NavigationPaths.LoginPath);
            containerRegistry.RegisterForNavigation<ProfileView>(NavigationPaths.ProfilePath);
            containerRegistry.RegisterForNavigation<RailwayListView>(NavigationPaths.RailwayListPath);
            containerRegistry.RegisterForNavigation<RegisterView>(NavigationPaths.RegisterPath);
            containerRegistry.RegisterForNavigation<RegularUserNavView>(NavigationPaths.RegularUserNavPath);
            containerRegistry.RegisterForNavigation<RegularUserView>(NavigationPaths.RegularUserPath);
            containerRegistry.RegisterForNavigation<UserListView>(NavigationPaths.UserListPath);
        }
    }
}