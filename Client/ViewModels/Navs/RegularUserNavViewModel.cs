using Client.Core;
using Client.Helpers;
using Prism.Regions;

namespace Client.ViewModels
{
    public class RegularUserNavViewModel : AuthNavViewModelBase
    {
        public RegularUserNavViewModel(IRegionManager regionManager, IAuthenticationService authService) : base(regionManager, authService)
        {
            RegionName = RegionNames.AuthContentRegion;
        }
    }
}