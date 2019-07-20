using Client.Core;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.ViewModels
{
    public class AdminViewModel : BindableBase
    {
        public AdminViewModel(IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion(RegionNames.NavRegion, typeof(Views.AdminNavView));
            regionManager.RegisterViewWithRegion(RegionNames.AuthContentRegion, typeof(Views.RailwayListView));
        }
    }
}