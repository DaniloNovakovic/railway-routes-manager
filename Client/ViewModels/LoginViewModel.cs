using System.Windows.Controls;
using System.Windows.Input;
using Client.Models;
using Client.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace Client.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IAuthenticationService _authService;

        public LoginViewModel(IAuthenticationService authService)
        {
            LoginCommand = new DelegateCommand<object>(LoginClick);
            _authService = authService;
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