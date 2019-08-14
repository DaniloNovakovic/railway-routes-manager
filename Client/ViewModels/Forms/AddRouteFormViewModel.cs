using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Client.Helpers;
using Prism.Commands;
using Prism.Regions;

namespace Client.ViewModels
{
    public class AddRouteFormViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IRouteService _routeService;
        private readonly IRailwayStationService _stationService;
        private bool _canAddRoute;
        private RouteModel _routeModel;

        public AddRouteFormViewModel(IRouteService routeService, IRailwayStationService stationService, IRegionManager regionManager)
        {
            _routeService = routeService;
            _stationService = stationService;
            _regionManager = regionManager;

            RouteModel = new RouteModel();
            RailwayStations = new ObservableCollection<RailwayStationModel>();
            AddRouteCommand = new DelegateCommand(async () => await AddRouteAsync());

            RouteModel.ErrorsChanged += RouteModel_ErrorsChanged;
        }

        public ICommand AddRouteCommand { get; set; }

        public bool CanAddRoute
        {
            get { return _canAddRoute; }
            set { SetProperty(ref _canAddRoute, value); }
        }

        public ObservableCollection<RailwayStationModel> RailwayStations { get; set; }

        public RouteModel RouteModel
        {
            get { return _routeModel; }
            set { SetProperty(ref _routeModel, value); }
        }

        public Task AddRouteAsync()
        {
            return SafeExecuteAsync(
                @try: async () =>
                {
                    CanAddRoute = false;
                    await _routeService.AddRouteAsync(RouteModel);
                    OnRouteAdded();
                },
                @finally: () => CanAddRoute = !RouteModel.HasErrors);
        }

        public override Task OnLoadedAsync()
        {
            return SafeExecuteAsync(async () =>
            {
                var stations = await _stationService.GetAllStationsAsync();
                RailwayStations.Clear();
                RailwayStations.AddRange(stations);
            });
        }

        private void OnRouteAdded()
        {
            _regionManager.RequestNavigate(RegionNames.AuthContentRegion, NavigationPaths.RailwayListPath);
        }

        private void RouteModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            CanAddRoute = !RouteModel.HasErrors;
        }
    }
}