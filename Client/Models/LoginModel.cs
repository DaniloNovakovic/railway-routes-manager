using Prism.Mvvm;

namespace Client.Models
{
    public class LoginModel : BindableBase
    {
        private string _password;

        private string _username;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
    }
}