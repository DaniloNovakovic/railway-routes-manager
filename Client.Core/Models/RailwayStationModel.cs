using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Prism.Validation;

namespace Client.Core
{
    public class RailwayStationModel : ValidatableBindableBase, ICloneable
    {
        private int _id;
        private LocationModel _location;
        private string _name;
        private int _numberOfPlatforms;

        public RailwayStationModel()
        {
            RailwayPlatforms = new ObservableCollection<RailwayPlatformModel>();
        }

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        [Required]
        public LocationModel Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }

        [Required]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public int NumberOfPlatforms
        {
            get { return _numberOfPlatforms; }
            set { SetProperty(ref _numberOfPlatforms, value); }
        }

        public ObservableCollection<RailwayPlatformModel> RailwayPlatforms { get; set; }

        public object Clone()
        {
            var station = new RailwayStationModel()
            {
                Id = Id,
                Name = Name,
                Location = Location?.Clone() as LocationModel,
                NumberOfPlatforms = NumberOfPlatforms
            };
            station.RailwayPlatforms = new ObservableCollection<RailwayPlatformModel>(RailwayPlatforms);
            return station;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }
    }
}