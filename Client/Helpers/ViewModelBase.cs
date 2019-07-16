using System.Text.RegularExpressions;

namespace Client.Helpers
{
    public class ViewModelBase : BindableBase, IViewModel
    {
        public virtual string Route { get; }

        public ViewModelBase()
        {
            string name = this.GetType().Name.Trim();
            Route = Regex.Replace(name, @"\s+|(viewmodel$)", "", RegexOptions.IgnoreCase)?.ToLower();
        }
    }
}