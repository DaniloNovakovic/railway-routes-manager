using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Prism.Commands;

namespace Client.Helpers
{
    public abstract class StationFormViewModel : ViewModelBase
    {
        private readonly ILocationService _locationService;
        private bool _canSubmit;
        private RailwayStationModel _stationModel;

        #region ctors

        protected StationFormViewModel(ILocationService locationService, ILogger logger) : this(locationService, logger, new RailwayStationModel())
        {
        }

        protected StationFormViewModel(ILocationService locationService, ILogger logger, RailwayStationModel station) : base(logger)
        {
            _locationService = locationService;

            Locations = new ObservableCollection<LocationModel>();
            RailwayStationModel = station.Clone() as RailwayStationModel;
            RailwayStationModel.ErrorsChanged += RailwayStationModel_ErrorsChanged;

            SubmitCommand = new DelegateCommand(async () => await OnSubmitAsync());
        }

        #endregion ctors

        public bool CanSubmit
        {
            get { return _canSubmit; }
            set { SetProperty(ref _canSubmit, value); }
        }

        public ObservableCollection<LocationModel> Locations { get; set; }

        public RailwayStationModel RailwayStationModel
        {
            get { return _stationModel; }
            set { SetProperty(ref _stationModel, value); }
        }

        public ICommand SubmitCommand { get; set; }

        public override Task OnLoadedAsync()
        {
            UpdateCanSubmit();

            return SafeExecuteAsync(async () =>
            {
                var locations = await _locationService.GetAllLocationsAsync();
                Locations.Clear();
                Locations.AddRange(locations);
            });
        }

        public abstract Task OnSubmitAsync();

        protected void UpdateCanSubmit()
        {
            CanSubmit = !RailwayStationModel.HasErrors;
        }

        private void RailwayStationModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            UpdateCanSubmit();
        }
    }
}