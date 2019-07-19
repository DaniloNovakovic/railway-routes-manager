using System.Windows.Input;
using Client.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.ViewModels
{
    public class RegisterViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public RegisterViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        public ICommand NavigateCommand { get; }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath);
        }
    }
}