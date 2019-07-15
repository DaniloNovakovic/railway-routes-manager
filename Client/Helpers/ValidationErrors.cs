using System.Collections.Generic;

namespace Client.Helpers
{
    public class ValidationErrors : BindableBase
    {
        private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();

        public bool IsValid
        {
            get
            {
                return _validationErrors.Count < 1;
            }
        }

        public string this[string fieldName]
        {
            get => GetError(fieldName);

            set => SetError(fieldName, value);
        }

        public void Clear()
        {
            _validationErrors.Clear();
        }

        public string GetError(string fieldName)
        {
            return _validationErrors.ContainsKey(fieldName) ?
                    _validationErrors[fieldName] : string.Empty;
        }

        public void SetError(string fieldName, string value)
        {
            if (_validationErrors.ContainsKey(fieldName))
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _validationErrors.Remove(fieldName);
                }
                else
                {
                    _validationErrors[fieldName] = value;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _validationErrors.Add(fieldName, value);
                }
            }
            OnPropertyChanged(nameof(IsValid));
        }
    }
}