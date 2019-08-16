using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;
using Prism.Regions;

namespace Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager, ILogger logger) : base(logger)
        {
            _regionManager = regionManager;
        }

        public override Task OnLoadedAsync()
        {
            _regionManager.RequestNavigate(RegionNames.WindowRegion, NavigationPaths.LoginPath);
            return Task.CompletedTask;
        }
    }
}