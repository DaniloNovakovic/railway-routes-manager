using System.ComponentModel.DataAnnotations;
using Prism.Validation;

namespace Client.Core
{
    public class UserModel : ValidatableBindableBase
    {
        private string _firstName;
        private int _id;
        private string _lastName;
        private string _password;
        private string _roleName;
        private string _username;

        [Required]
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        [Required]
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 4)]
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public string RoleName
        {
            get { return _roleName; }
            set { SetProperty(ref _roleName, value); }
        }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 4)]
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
    }
}