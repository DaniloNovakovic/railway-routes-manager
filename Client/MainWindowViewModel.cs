using Client.ViewModel;

namespace Client
{
    public class MainWindowViewModel : BindableBase
    {
        private BindableBase currentViewModel;

        public MainWindowViewModel()
        {
            CurrentViewModel = new LoginViewModel();
        }

        public BindableBase CurrentViewModel
        {
            get => currentViewModel;
            set => SetProperty(ref currentViewModel, value);
        }
    }
}