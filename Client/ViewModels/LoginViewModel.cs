using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Client.Models;
using Client.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Validation;

namespace Client.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IAuthenticationService _authService;

        private ICollection<string> _errors;

        public LoginViewModel(IAuthenticationService authService)
        {
            LoginCommand = new DelegateCommand<object>(LoginClick);
            _authService = authService;
            LoginModel.ErrorsChanged += (s, e) => Errors = FlattenErrors(LoginModel);
        }

        public ICollection<string> Errors
        {
            get { return _errors; }
            set { SetProperty(ref _errors, value); }
        }

        public ICommand LoginCommand { get; }

        public LoginModel LoginModel { get; set; } = new LoginModel();

        private ICollection<string> FlattenErrors(IValidatableBindableBase item)
        {
            var errors = new List<string>();
            var allErrors = item.GetAllErrors();
            foreach (string propertyName in allErrors.Keys)
            {
                foreach (string errorString in allErrors[propertyName])
                {
                    errors.Add(propertyName + ": " + errorString);
                }
            }
            return errors;
        }

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