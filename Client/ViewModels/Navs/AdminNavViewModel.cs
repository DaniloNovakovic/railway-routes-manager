using Client.Core;
using Client.Helpers;
using Prism.Regions;

namespace Client.ViewModels
{
    public class AdminNavViewModel : AuthNavViewModelBase
    {
        public AdminNavViewModel(
            IRegionManager regionManager,
            IAuthenticationService authService,
            ILogger logger) : base(regionManager, authService, logger)
        {
            RegionName = RegionNames.AuthContentRegion;
        }
    }
}