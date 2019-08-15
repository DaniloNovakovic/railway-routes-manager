using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Client.Helpers;
using Prism.Commands;
using Prism.Mvvm;

namespace Client.ViewModels
{
    public class RailwayListViewModel : ViewModelBase
    {
        private readonly IRouteService _routeService;
        private readonly IRailwayStationService _stationService;
        private BindableBase _formViewModel;
        private bool _isDialogOpen;

        public RailwayListViewModel(IRouteService routeService, IRailwayStationService stationService)
        {
            _routeService = routeService;
            _stationService = stationService;

            Routes = new ObservableCollection<RouteModel>();
            AddCommand = new DelegateCommand(ShowAddRouteForm);
            DuplicateRouteCommand = new DelegateCommand<RouteModel>(async (route) => await DuplicateRouteAsync(route));
            EditRouteCommand = new DelegateCommand<RouteModel>(ShowEditRouteForm);
            RefreshCommand = new DelegateCommand(async () => await RefreshRoutesAsync());
            RemoveRouteCommand = new DelegateCommand<int?>(async (id) => await RemoveRouteAsync(id));
        }

        public ICommand AddCommand { get; }

        public ICommand DuplicateRouteCommand { get; }

        public ICommand EditRouteCommand { get; }

        public BindableBase FormViewModel
        {
            get { return _formViewModel; }
            set { SetProperty(ref _formViewModel, value); }
        }

        public bool IsDialogOpen
        {
            get { return _isDialogOpen; }
            set { SetProperty(ref _isDialogOpen, value); }
        }

        public ICommand RefreshCommand { get; }

        public ICommand RemoveRouteCommand { get; }

        public ObservableCollection<RouteModel> Routes { get; set; }

        public Task DuplicateRouteAsync(RouteModel route)
        {
            return SafeExecuteAsync(async () =>
            {
                await _routeService.AddRouteAsync(route);
                await RefreshRoutesAsync();
            });
        }

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

        public Task RemoveRouteAsync(int? id)
        {
            if (id is null)
            {
                return Task.CompletedTask;
            }

            return SafeExecuteAsync(async () =>
            {
                await _routeService.RemoveRouteAsync(id.Value);
                await RefreshRoutesAsync();
            });
        }

        private async void OnRouteSubmited()
        {
            IsDialogOpen = false;
            await RefreshRoutesAsync();
        }

        private void ShowAddRouteForm()
        {
            FormViewModel = new AddRouteFormViewModel(_routeService, _stationService, OnRouteSubmited);
            IsDialogOpen = true;
        }

        private void ShowEditRouteForm(RouteModel route)
        {
            var routeCopy = route.Clone() as RouteModel ?? new RouteModel();
            FormViewModel = new EditRouteFormViewModel(_routeService, _stationService, routeCopy, OnRouteSubmited);
            IsDialogOpen = true;
        }
    }
}