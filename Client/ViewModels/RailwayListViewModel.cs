using Prism.Mvvm;

namespace Client.ViewModels
{
    public class RailwayListViewModel : BindableBase
    {
        private string _text;

        public RailwayListViewModel()
        {
            Text = "These are the railways....";
        }

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
    }
}