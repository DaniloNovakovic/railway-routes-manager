using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Client.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.ViewModels
{
    public class RailwayListViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IRouteService _routeService;
        private readonly IRailwayStationService _stationService;
        private BindableBase _formViewModel;
        private bool _isDialogOpen;

        public RailwayListViewModel(IRouteService routeService, IRailwayStationService stationService, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _routeService = routeService;
            _stationService = stationService;

            Routes = new ObservableCollection<RouteModel>();
            AddCommand = new DelegateCommand(NavigateToAddForm);
            RefreshCommand = new DelegateCommand(async () => await RefreshRoutesAsync());
            RemoveRouteCommand = new DelegateCommand<int?>(async (id) => await RemoveRouteAsync(id));
            EditRouteCommand = new DelegateCommand<RouteModel>(ShowEditRouteForm);
        }

        public ICommand AddCommand { get; }

        public ICommand EditRouteCommand { get; }

        public BindableBase FormViewModel
        {
            get { return _formViewModel; }
            set { SetProperty(ref _formViewModel, value); }
        }

        public bool IsDialogOpen
        {
            get { return _isDialogOpen; }
            set { SetProperty(ref _isDialogOpen, value); }
        }

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

        private async void OnRouteSubmited()
        {
            IsDialogOpen = false;
            await RefreshRoutesAsync();
        }

        private void ShowEditRouteForm(RouteModel route)
        {
            var routeCopy = route.Clone() as RouteModel ?? new RouteModel();
            FormViewModel = new EditRouteFormViewModel(_routeService, _stationService, routeCopy, OnRouteSubmited);
            IsDialogOpen = true;
        }
    }
}