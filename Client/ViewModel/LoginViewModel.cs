using System.Windows.Controls;
using System.Windows.Input;
using Client.Helpers;
using Client.ValidationRules;

namespace Client.ViewModel
{
    public class LoginViewModel : BindableBase
    {
        private ICommand _passwordCommand;
        private string _passwordError;
        private string _username;
        public string LoginError { get; set; }

        public ICommand PasswordCommand
        {
            get
            {
                return _passwordCommand ?? (_passwordCommand = new MyICommand<object>(LoginClick));
            }
        }

        public string PasswordError
        {
            get { return _passwordError; }
            set
            {
                _passwordError = value;
                OnPropertyChanged(nameof(PasswordError));
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private void LoginClick(object p)
        {
            var passwordBox = p as PasswordBox;
            string password = passwordBox.Password;
            if (!ValidatePassword(password))
            {
                return;
            }

            // Todo: LOGIN
        }

        private bool ValidatePassword(string password)
        {
            var rule = new PasswordValidationRule();
            var result = rule.Validate(password, System.Globalization.CultureInfo.InvariantCulture);
            PasswordError = result.ErrorContent?.ToString() ?? "";
            return result.IsValid;
        }
    }
}