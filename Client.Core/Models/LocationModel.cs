using System.ComponentModel.DataAnnotations;
using Prism.Validation;

namespace Client.Core
{
    public class LocationModel : ValidatableBindableBase
    {
        private CountryModel _country;
        private int _id;
        private string _name;

        public CountryModel Country
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
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