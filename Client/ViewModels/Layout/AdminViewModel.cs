using System.Windows.Input;
using Client.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.ViewModels
{
    public class AdminViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public AdminViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            OnLoadedCommand = new DelegateCommand(OnLoaded);
        }

        public ICommand OnLoadedCommand { get; }

        public void OnLoaded()
        {
            _regionManager.RequestNavigate(RegionNames.AuthContentRegion, NavigationPaths.RailwayListPath);
        }
    }
}