using System.Windows.Controls;
using Client.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace Client.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        public LoginModel LoginModel { get; set; } = new LoginModel();
        public DelegateCommand<object> LoginCommand { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand<object>(LoginClick);
        }

        private void LoginClick(object p)
        {
            var passwordBox = p as PasswordBox;
            LoginModel.Password = passwordBox?.Password ?? "";
            //LoginModel.Validate();
            //if (!LoginModel.IsValid)
            //{
            //    return;
            //}

            // Todo: LOGIN
        }
    }
}