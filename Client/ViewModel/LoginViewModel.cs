using System.Windows.Controls;
using System.Windows.Input;
using Client.Helpers;
using Client.Model;

namespace Client.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private ICommand _passwordCommand;

        public LoginModel LoginModel { get; set; } = new LoginModel();

        public ICommand PasswordCommand
        {
            get
            {
                return _passwordCommand ?? (_passwordCommand = new MyICommand<object>(LoginClick));
            }
        }

        private void LoginClick(object p)
        {
            var passwordBox = p as PasswordBox;
            LoginModel.Password = passwordBox?.Password ?? "";
            LoginModel.Validate();
            if (!LoginModel.IsValid)
            {
                return;
            }

            // Todo: LOGIN
        }
    }
}