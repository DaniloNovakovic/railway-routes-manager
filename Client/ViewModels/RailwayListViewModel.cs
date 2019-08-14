using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Client.Helpers;
using Prism.Commands;
using Prism.Regions;

namespace Client.ViewModels
{
    public class RailwayListViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IRouteService _routeService;

        public RailwayListViewModel(IRouteService routeService, IRegionManager regionManager)
        {
            _routeService = routeService;
            _regionManager = regionManager;

            Routes = new ObservableCollection<RouteModel>();
            AddCommand = new DelegateCommand(NavigateToAddForm);
            RefreshCommand = new DelegateCommand(async () => await RefreshRoutesAsync());
            RemoveRouteCommand = new DelegateCommand<int?>(async (id) => await RemoveRouteAsync(id));
        }

        public ICommand AddCommand { get; }

        public ICommand RefreshCommand { get; }
        public ICommand RemoveRouteCommand { get; }
        public ObservableCollection<RouteModel> Routes { get; set; }

        public override Task OnLoadedAsync()
        {
            return RefreshRoutesAsync();
        }

        public Task RefreshRoutesAsync()
        {
            return SafeExecuteAsync(async () =>
            {
                var routes = await _routeService.GetAllRoutesAsync();
                Routes.Clear();
                Routes.AddRange(routes);
            });
        }

        public Task RemoveRouteAsync(int? id)
        {
            if (id is null)
            {
                return Task.CompletedTask;
            }

            return SafeExecuteAsync(async () =>
            {
                await _routeService.RemoveRouteAsync(id.Value);
                await RefreshRoutesAsync();
            });
        }

        private void NavigateToAddForm()
        {
            _regionManager.RequestNavigate(RegionNames.AuthContentRegion, NavigationPaths.AddRouteFormPath);
        }
    }
}