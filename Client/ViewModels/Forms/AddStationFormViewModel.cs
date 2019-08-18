using System;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;

namespace Client.ViewModels
{
    public class AddStationFormViewModel : StationFormViewModel
    {
        private readonly Action _onStationAdded;
        private readonly IRailwayStationService _stationService;

        public AddStationFormViewModel(
            IRailwayStationService stationService,
            ILocationService locationService,
            ILogger logger,
            Action onStationAdded = null) : base(locationService, logger)
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