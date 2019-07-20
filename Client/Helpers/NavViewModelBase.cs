using System.Windows.Input;
using Client.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.Helpers
{
    public abstract class NavViewModelBase : BindableBase
    {
        protected readonly IRegionManager _regionManager;

        private string _regionName = RegionNames.AuthContentRegion;

        protected NavViewModelBase(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        public virtual ICommand NavigateCommand { get; }

        public virtual string RegionName
        {
            get { return _regionName; }
            protected set { SetProperty(ref _regionName, value); }
        }

        protected virtual void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionName, navigatePath);
        }
    }
}