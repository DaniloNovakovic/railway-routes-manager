using Client.Core;
using Client.Helpers;
using Prism.Regions;

namespace Client.ViewModels
{
    public class RegularUserNavViewModel : NavViewModelBase
    {
        public RegularUserNavViewModel(IRegionManager regionManager) : base(regionManager)
        {
            RegionName = RegionNames.AuthContentRegion;
        }
    }
}