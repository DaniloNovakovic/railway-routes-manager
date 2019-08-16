using Client.Core;
using Client.Helpers;
using Prism.Regions;

namespace Client.ViewModels
{
    public class RegularUserNavViewModel : AuthNavViewModelBase
    {
        public RegularUserNavViewModel(
            IRegionManager regionManager,
            IAuthenticationService authService,
            ILogger logger) : base(regionManager, authService, logger)
        {
            RegionName = RegionNames.AuthContentRegion;
        }
    }
}