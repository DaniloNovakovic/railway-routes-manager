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
        private bool _canAdd;
        private RouteModel _routeModel;

        public AddRouteFormViewModel(IRouteService routeService, IRailwayStationService stationService, IRegionManager regionManager)
        {
            _routeService = routeService;
            _stationService = stationService;
            _regionManager = regionManager;

            RouteModel = new RouteModel();
            RailwayStations = new ObservableCollection<RailwayStationModel>();
            AddCommand = new DelegateCommand(async () => await AddRouteAsync());

            RouteModel.ErrorsChanged += RouteModel_ErrorsChanged;
        }

        public ICommand AddCommand { get; set; }

        public bool CanAdd
        {
            get { return _canAdd; }
            set { SetProperty(ref _canAdd, value); }
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
                    CanAdd = false;
                    await _routeService.AddRouteAsync(RouteModel);
                    OnRouteAdded();
                },
                @finally: () => CanAdd = !RouteModel.HasErrors);
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
            CanAdd = !RouteModel.HasErrors;
        }
    }
}