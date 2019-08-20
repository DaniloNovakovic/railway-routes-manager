using System;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;
using Prism.Events;

namespace Client.ViewModels
{
    public class EditStationFormViewModel : StationFormViewModelBase
    {
        private readonly IRailwayStationService _stationService;
        private readonly Action _onStationAdded;

        public EditStationFormViewModel(
            IRailwayStationService stationService,
            ILocationService locationService,
            ILogger logger,
            RailwayStationModel station,
            Action onStationAdded = null,
            IEventAggregator eventAggregator = null) : base(locationService, logger, station, eventAggregator)
        {
            _stationService = stationService;
            _onStationAdded = onStationAdded;
        }

        public override Task OnSubmitAsync()
        {
            return SafeExecuteAsync(
                @try: async () =>
                {
                    CanSubmit = false;
                    await _stationService.UpdateStationAsync(RailwayStationModel);
                    OnStationAdded();
                },
                @finally: UpdateCanSubmit);
        }

        private void OnStationAdded()
        {
            _onStationAdded?.Invoke();
        }
    }
}