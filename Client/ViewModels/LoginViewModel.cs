using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private readonly IRegionManager _regionManager;
        private ICollection<string> _errors = new List<string>();

        public LoginViewModel(IAuthenticationService authService, IRegionManager regionManager)
        {
            _authService = authService;
            _regionManager = regionManager;

            LoginModel.ErrorsChanged += (s, e) => Errors = DictionaryFlattener.Flatten(LoginModel.GetAllErrors());
            LoginCommand = new DelegateCommand<object>(LoginClick);
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        public ICollection<string> Errors
        {
            get { return _errors; }
            set { SetProperty(ref _errors, value); }
        }

        public ICommand LoginCommand { get; }
        public LoginModel LoginModel { get; set; } = new LoginModel();
        public ICommand NavigateCommand { get; }

        private void LoginClick(object p)
        {
            if (p is PasswordBox passwordBox)
            {
                LoginModel.Password = passwordBox.Password ?? "";
            }

            if (!LoginModel.ValidateProperties())
            {
                return;
            }

            try
            {
                string roleName = _authService.Login(LoginModel.Username, LoginModel.Password);

                if (roleName == RoleNames.Admin)
                {
                    Navigate(NavigationPaths.AdminPath);
                }
                else
                {
                    Navigate(NavigationPaths.RegularUserPath);
                }
            }
            catch (Exception ex)
            {
                Errors = new List<string>() { ex.Message };
                Trace.TraceError(ex.Message);
            }
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.WindowRegion, navigatePath);
        }
    }
}