using Client.Core;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.ViewModels
{
    public class RegularUserViewModel : BindableBase
    {
        public RegularUserViewModel(IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion(RegionNames.NavRegion, typeof(Views.RegularUserNavView));
            regionManager.RegisterViewWithRegion(RegionNames.AuthContentRegion, typeof(Views.RailwayListView));
        }
    }
}