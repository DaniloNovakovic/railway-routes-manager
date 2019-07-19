using Client.Core;
using Client.Views;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "School Project - Main Window";

        public MainWindowViewModel(IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(LoginView));
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}