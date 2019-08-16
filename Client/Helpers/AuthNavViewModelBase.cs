using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Prism.Commands;
using Prism.Regions;

namespace Client.Helpers
{
    public class AuthNavViewModelBase : NavViewModelBase
    {
        protected readonly IAuthenticationService _authService;

        protected AuthNavViewModelBase(IRegionManager regionManager) : base(regionManager)
        {
        }

        public AuthNavViewModelBase(IRegionManager regionManager, IAuthenticationService authService, ILogger logger) : base(regionManager)
        {
            _authService = authService;
            _logger = logger;

            LogoutCommand = new DelegateCommand(async () => await LogoutClick());
        }

        private bool _canLogOut = true;
        private readonly ILogger _logger;

        public bool CanLogOut
        {
            get { return _canLogOut; }
            private set { SetProperty(ref _canLogOut, value); }
        }

        public ICommand LogoutCommand { get; }

        public virtual async Task LogoutClick()
        {
            try
            {
                CanLogOut = false;
                await _authService?.LogoutAsync();
                _regionManager?.RequestNavigate(RegionNames.WindowRegion, NavigationPaths.LoginPath);
            }
            catch (Exception ex)
            {
                _logger.Exception(ex.InnerException?.Message ?? ex.Message);
            }
            finally
            {
                CanLogOut = true;
            }
        }
    }
}