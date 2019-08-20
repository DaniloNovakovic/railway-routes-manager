using System;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;
using Prism.Events;

namespace Client.ViewModels
{
    public class AddStationFormViewModel : StationFormViewModelBase
    {
        private readonly Action _onStationAdded;
        private readonly IRailwayStationService _stationService;

        public AddStationFormViewModel(
            IRailwayStationService stationService,
            ILocationService locationService,
            ILogger logger,
            Action onStationAdded = null,
            IEventAggregator eventAggregator = null) : base(locationService, logger, eventAggregator)
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
                    await _stationService.AddStationAsync(RailwayStationModel);
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