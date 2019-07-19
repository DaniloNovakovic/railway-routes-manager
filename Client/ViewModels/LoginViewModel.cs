using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Client.Core;
using Client.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace Client.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IAuthenticationService _authService;

        private ICollection<string> _errors;

        public LoginViewModel(IAuthenticationService authService)
        {
            _authService = authService;
            LoginCommand = new DelegateCommand<object>(LoginClick);
            LoginModel.ErrorsChanged += (s, e) => Errors = DictionaryFlattener.Flatten(LoginModel.GetAllErrors());
        }

        public ICollection<string> Errors
        {
            get { return _errors; }
            set { SetProperty(ref _errors, value); }
        }

        public ICommand LoginCommand { get; }

        public LoginModel LoginModel { get; set; } = new LoginModel();

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

            _authService.Login(LoginModel.Username, LoginModel.Password);
        }
    }
}