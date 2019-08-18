using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Prism.Validation;

namespace Client.Core
{
    public class LocationModel : ValidatableBindableBase, ICloneable
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

        public object Clone()
        {
            return new LocationModel()
            {
                Id = Id,
                Name = Name,
                Country = Country.Clone() as CountryModel
            };
        }

        public override string ToString()
        {
            var builder = new StringBuilder().Append(Name);

            if (Country != null)
            {
                builder
                    .Append(',').Append(Country.Code)
                    .Append(',').Append(Country.Name);
            }

            return builder.ToString();
        }
    }
}