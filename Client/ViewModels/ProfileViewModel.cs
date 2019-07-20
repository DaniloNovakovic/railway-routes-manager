using Prism.Mvvm;

namespace Client.ViewModels
{
    public class ProfileViewModel : BindableBase
    {
        private string _text;

        public ProfileViewModel()
        {
            Text = "This is my profile!";
        }

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
    }
}