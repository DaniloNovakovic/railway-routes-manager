using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Prism.Validation;

namespace Client.Core
{
    public class RailwayStationModel : ValidatableBindableBase
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

        [Range(1, int.MaxValue)]
        public int NumberOfPlatforms
        {
            get { return _numberOfPlatforms; }
            set { SetProperty(ref _numberOfPlatforms, value); }
        }

        public ObservableCollection<RailwayPlatformModel> RailwayPlatforms { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }
    }
}