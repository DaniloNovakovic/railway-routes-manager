using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;

namespace Client.ViewModels
{
    public class AddRouteFormViewModel : ViewModelBase
    {
        private readonly IRouteService _routeService;
        private readonly IRailwayStationService _stationService;

        public AddRouteFormViewModel(IRouteService routeService, IRailwayStationService stationService)
        {
            _routeService = routeService;
            _stationService = stationService;

            RailwayStations = new ObservableCollection<RailwayStationModel>();
        }

        public ObservableCollection<RailwayStationModel> RailwayStations { get; set; }

        public override Task OnLoadedAsync()
        {
            return SafeExecuteAsync(async () =>
            {
                var stations = await _stationService.GetAllStationsAsync();
                RailwayStations.Clear();
                RailwayStations.AddRange(stations);
            });
        }

        private async Task SafeExecuteAsync(Func<Task> callback)
        {
            try
            {
                await callback?.Invoke();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}