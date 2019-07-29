using Client.Core;
using Client.Helpers;
using Prism.Regions;

namespace Client.ViewModels
{
    public class AdminNavViewModel : AuthNavViewModelBase
    {
        public AdminNavViewModel(IRegionManager regionManager, IAuthenticationService authService) : base(regionManager, authService)
        {
            RegionName = RegionNames.AuthContentRegion;
        }
    }
}