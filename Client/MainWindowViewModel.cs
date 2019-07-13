using Client.ViewModel;

namespace Client
{
    public class MainWindowViewModel : BindableBase
    {
        private BindableBase currentViewModel;

        public BindableBase CurrentViewModel
        {
            get => this.currentViewModel;
            set => SetProperty(ref currentViewModel, value);
        }

        public MainWindowViewModel()
        {
            CurrentViewModel = new LoginViewModel();
        }
    }
}