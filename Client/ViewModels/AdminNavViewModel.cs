using System.Windows.Input;
using Client.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.ViewModels
{
    public class AdminNavViewModel : BindableBase
    {
        public ICommand NavigateCommand { get; }

        private readonly IRegionManager _regionManager;

        public AdminNavViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.AuthContentRegion, navigatePath);
        }
    }
}