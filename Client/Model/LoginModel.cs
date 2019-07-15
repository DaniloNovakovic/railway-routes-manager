using System.ComponentModel.DataAnnotations;
using Client.Helpers;

namespace Client.Model
{
    public class LoginModel : ValidationBase
    {
        private string _password;

        private string _username;

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 4)]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        [Required]
        [StringLength(maximumLength: 12, MinimumLength = 4)]
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
    }
}