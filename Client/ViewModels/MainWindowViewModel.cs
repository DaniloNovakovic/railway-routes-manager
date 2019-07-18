using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private string _title = "School Project - Main Window";

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        public ICommand NavigateCommand { get; }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }
    }
}