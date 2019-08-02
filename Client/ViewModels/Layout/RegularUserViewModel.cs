using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;
using Prism.Regions;

namespace Client.ViewModels
{
    public class RegularUserViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;

        public RegularUserViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public bool KeepAlive => false;

        public override Task OnLoadedAsync()
        {
            _regionManager.RequestNavigate(RegionNames.AuthContentRegion, NavigationPaths.RailwayListPath);
            return Task.CompletedTask;
        }
    }
}