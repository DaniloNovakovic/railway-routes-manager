using Prism.Validation;

namespace Client.Core
{
    public class LogModel : ValidatableBindableBase
    {
        private string _date;

        private string _level;

        private string _message;

        public string Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        public string Level
        {
            get { return _level; }
            set { SetProperty(ref _level, value); }
        }

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
    }
}