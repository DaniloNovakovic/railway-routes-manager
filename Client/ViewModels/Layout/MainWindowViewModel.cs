using System;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;
using MaterialDesignThemes.Wpf;
using Prism.Events;
using Prism.Regions;

namespace Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        public ISnackbarMessageQueue MessageQueue { get; set; }

        public MainWindowViewModel(IRegionManager regionManager, ILogger logger, IEventAggregator eventAggregator) : base(logger, eventAggregator)
        {
            _regionManager = regionManager;

            MessageQueue = new SnackbarMessageQueue();
            eventAggregator.GetEvent<SnackbarMessageEvent>().Subscribe(ShowSnackbarMessage);
        }

        private void ShowSnackbarMessage(string message)
        {
            MessageQueue.Enqueue(message);
        }

        public override Task OnLoadedAsync()
        {
            _regionManager.RequestNavigate(RegionNames.WindowRegion, NavigationPaths.LoginPath);
            return Task.CompletedTask;
        }
    }
}