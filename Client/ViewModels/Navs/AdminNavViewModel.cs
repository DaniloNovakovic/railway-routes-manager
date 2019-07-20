using Client.Core;
using Client.Helpers;
using Prism.Regions;

namespace Client.ViewModels
{
    public class AdminNavViewModel : NavViewModelBase
    {
        public AdminNavViewModel(IRegionManager regionManager) : base(regionManager)
        {
            RegionName = RegionNames.AuthContentRegion;
        }
    }
}