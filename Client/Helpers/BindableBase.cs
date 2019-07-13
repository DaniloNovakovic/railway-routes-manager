using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = "")
        {
            if (Equals(member, val)) return;

            member = val;
            OnPropertyChanged(propertyName);
        }
    }
}