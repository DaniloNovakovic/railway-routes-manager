using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace Client.Helpers
{
    public class ViewModelBase : BindableBase
    {
        public ViewModelBase()
        {
            OnLoadedCommand = new DelegateCommand(async () => await OnLoadedAsync());
        }

        public ICommand OnLoadedCommand { get; }

        public virtual Task OnLoadedAsync()
        {
            return Task.CompletedTask;
        }
    }
}