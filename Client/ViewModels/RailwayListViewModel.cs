using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Client.Helpers;
using Prism.Commands;

namespace Client.ViewModels
{
    public class RailwayListViewModel : ViewModelBase
    {
        private readonly IRouteService _routeService;

        public RailwayListViewModel(IRouteService routeService)
        {
            _routeService = routeService;

            Routes = new ObservableCollection<RouteModel>();
            RefreshCommand = new DelegateCommand(async () => await RefreshRoutesAsync());
        }

        public ICommand RefreshCommand { get; }
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