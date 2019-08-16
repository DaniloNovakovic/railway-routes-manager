using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Client.Core;
using Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IAuthenticationService _authService;

        private readonly ILogger _logger;
        private readonly IRegionManager _regionManager;
        private bool _canLogIn = true;
        private ICollection<string> _errors = new List<string>();

        public LoginViewModel(IAuthenticationService authService, IRegionManager regionManager, ILogger logger)
        {
            _authService = authService;
            _regionManager = regionManager;
            _logger = logger;

            LoginModel.ErrorsChanged += (s, e) => Errors = DictionaryFlattener.Flatten(LoginModel.GetAllErrors());
            LoginCommand = new DelegateCommand<object>(async (obj) => await LoginClickAsync(obj));
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        public bool CanLogIn
        {
            get { return _canLogIn; }
            set { SetProperty(ref _canLogIn, value); }
        }

        public ICollection<string> Errors
        {
            get { return _errors; }
            set { SetProperty(ref _errors, value); }
        }

        public ICommand LoginCommand { get; }
        public LoginModel LoginModel { get; set; } = new LoginModel();
        public ICommand NavigateCommand { get; }

        public async Task LoginClickAsync(object p)
        {
            if (p is PasswordBox passwordBox)
            {
                LoginModel.Password = passwordBox.Password ?? "";
            }

            if (!LoginModel.ValidateProperties())
            {
                return;
            }

            await SafeExecuteAsync(async () =>
             {
                 string roleName = await _authService.LoginAsync(LoginModel.Username, LoginModel.Password);

                 if (roleName == RoleNames.Admin)
                 {
                     Navigate(NavigationPaths.AdminPath);
                 }
                 else
                 {
                     Navigate(NavigationPaths.RegularUserPath);
                 }
             });
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.WindowRegion, navigatePath);
        }

        private async Task SafeExecuteAsync(Func<Task> callback)
        {
            try
            {
                CanLogIn = false;
                await callback?.Invoke();
            }
            catch (Exception ex)
            {
                Errors = new List<string>() { ex.InnerException?.Message ?? ex.Message };
                _logger.Exception(ex.ToString());
            }
            finally
            {
                CanLogIn = true;
            }
        }
    }
}