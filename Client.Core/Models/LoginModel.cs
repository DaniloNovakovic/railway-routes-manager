using System.ComponentModel.DataAnnotations;
using Prism.Validation;

namespace Client.Core
{
    public class LoginModel : ValidatableBindableBase
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
        [StringLength(maximumLength: 20, MinimumLength = 4)]
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
    }
}