using System.ComponentModel.DataAnnotations;
using Common;
using Prism.Validation;

namespace Client.Core
{
    public class RailwayPlatformModel : ValidatableBindableBase
    {
        private EntranceType _entranceType;

        private int _id;

        private string _mark;

        private string _name;

        public EntranceType EntranceType
        {
            get { return _entranceType; }
            set { SetProperty(ref _entranceType, value); }
        }

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        [Required]
        public string Mark
        {
            get { return _mark; }
            set { SetProperty(ref _mark, value); }
        }

        [Required]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public int? RailwayStationId { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }
    }
}