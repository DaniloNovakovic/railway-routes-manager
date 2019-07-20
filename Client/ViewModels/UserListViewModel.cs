using Prism.Mvvm;

namespace Client.ViewModels
{
    public class UserListViewModel : BindableBase
    {
        private string _text;

        public UserListViewModel()
        {
            Text = "These are my users...";
        }

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
    }
}