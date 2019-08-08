using System.Collections.ObjectModel;
using Client.Core;
using Prism.Mvvm;

namespace Client.ViewModels
{
    public class RailwayListViewModel : BindableBase
    {
        public ObservableCollection<RouteModel> Routes { get; set; }

        public RailwayListViewModel()
        {
            Routes = new ObservableCollection<RouteModel>();
        }
    }
}