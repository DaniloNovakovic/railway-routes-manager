using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Prism.Commands;
using Prism.Events;

namespace Client.Helpers
{
    public abstract class RouteFormViewModelBase : ViewModelBase
    {
        protected readonly IRailwayStationService _stationService;
        private readonly RouteModel _originalRoute;
        private bool _canSubmit;
        private RouteModel _routeModel;

        #region ctors

        protected RouteFormViewModelBase(IRailwayStationService stationService, ILogger logger, IEventAggregator eventAggregator = null) : this(stationService, logger, new RouteModel(), eventAggregator)
        {
        }

        protected RouteFormViewModelBase(IRailwayStationService stationService, ILogger logger, RouteModel route, IEventAggregator eventAggregator = null) : base(logger, eventAggregator)
        {
            _stationService = stationService;

            RailwayStations = new ObservableCollection<RailwayStationModel>();
            SubmitCommand = new DelegateCommand(async () => await OnSubmitAsync());

            _originalRoute = route ?? new RouteModel();
            RouteModel = route.Clone() as RouteModel;
            RouteModel.ErrorsChanged += RouteModel_ErrorsChanged;
        }

        #endregion ctors

        public bool CanSubmit
        {
            get { return _canSubmit; }
            set { SetProperty(ref _canSubmit, value); }
        }

        public ObservableCollection<RailwayStationModel> RailwayStations { get; set; }

        public RouteModel RouteModel
        {
            get { return _routeModel; }
            set { SetProperty(ref _routeModel, value); }
        }

        public ICommand SubmitCommand { get; set; }

        public override Task OnLoadedAsync()
        {
            UpdateCanSubmit();

            return SafeExecuteAsync(async () =>
            {
                var stations = await _stationService.GetAllStationsAsync();
                RailwayStations.Clear();
                RailwayStations.AddRange(stations);

                int[] ids = _originalRoute.RailwayStations.Select(s => s.Id).ToArray();
                RouteModel.RailwayStations.Clear();
                RouteModel.RailwayStations.AddRange(RailwayStations.Where(s => ids.Contains(s.Id)));
            });
        }

        public abstract Task OnSubmitAsync();

        protected void UpdateCanSubmit()
        {
            CanSubmit = !RouteModel.HasErrors;
        }

        private void RouteModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            UpdateCanSubmit();
        }
    }
}