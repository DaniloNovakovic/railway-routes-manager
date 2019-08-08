using System.ComponentModel.DataAnnotations;
using Prism.Validation;

namespace Client.Core
{
    public class CountryModel : ValidatableBindableBase
    {
        private string _code;
        private int _id;
        private string _name;

        [Required]
        public string Code
        {
            get { return _code; }
            set { SetProperty(ref _code, value); }
        }

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        [Required]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
    }
}