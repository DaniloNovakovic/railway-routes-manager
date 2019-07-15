using System;
using System.ComponentModel.DataAnnotations;
using Client.Helpers;

namespace Client.Model
{
    public class LoginModel : ValidationBase
    {
        [Required]
        private string _password;

        [Required]
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