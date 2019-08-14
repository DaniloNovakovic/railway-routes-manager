using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Prism.Commands;

namespace Client.Helpers
{
    public abstract class RouteFormViewModel : ViewModelBase
    {
        protected readonly IRailwayStationService _stationService;
        private bool _canSubmit;
        private RouteModel _routeModel;

        #region ctors

        protected RouteFormViewModel(IRailwayStationService stationService) : this(stationService, new RouteModel())
        {
        }

        protected RouteFormViewModel(IRailwayStationService stationService, RouteModel route)
        {
            _stationService = stationService;

            RailwayStations = new ObservableCollection<RailwayStationModel>();
            SubmitCommand = new DelegateCommand(async () => await OnSubmitAsync());

            RouteModel = route;
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