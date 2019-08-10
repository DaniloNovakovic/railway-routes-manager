﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Prism.Validation;

namespace Client.Core
{
    public class RouteModel : ValidatableBindableBase
    {
        private int _id;
        private string _mark;
        private string _name;

        public RouteModel()
        {
            RailwayStations = new ObservableCollection<RailwayStationModel>();
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

        public ObservableCollection<RailwayStationModel> RailwayStations { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Id: ").Append(Id)
                .Append(", Name: ").Append(Name)
                .Append(", Mark: ").Append(Mark)
                .ToString();
        }
    }
}