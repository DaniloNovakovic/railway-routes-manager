using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Client.Helpers;
using Prism.Commands;
using Prism.Events;

namespace Client.ViewModels
{
    public class AddUserFormViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserService _userService;
        private bool _canAddUser;
        private UserModel _userModel;

        public AddUserFormViewModel(IUserService userService, IEventAggregator eventAggregator)
        {
            _userService = userService;
            _eventAggregator = eventAggregator;

            UserModel = new UserModel();
            AddUserCommand = new DelegateCommand(async () => await AddUserAsync());
            UserModel.ErrorsChanged += UserModel_ErrorsChanged;
        }

        public ICommand AddUserCommand { get; set; }

        public bool CanAddUser
        {
            get { return _canAddUser; }
            set { SetProperty(ref _canAddUser, value); }
        }

        public UserModel UserModel
        {
            get { return _userModel; }
            set { SetProperty(ref _userModel, value); }
        }

        public Task AddUserAsync()
        {
            if (UserModel.HasErrors)
            {
                return Task.CompletedTask;
            }

            return SafeExecuteAsync(
                @try: async () =>
                {
                    CanAddUser = false;
                    await _userService.AddUserAsync(UserModel);
                    OnUserAdded();
                    UserModel = new UserModel();
                },
                @finally: () => CanAddUser = !UserModel.HasErrors);
        }

        private void OnUserAdded()
        {
            _eventAggregator.GetEvent<UserAddedEvent>().Publish(UserModel.Username);
        }

        private void UserModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            CanAddUser = !UserModel.HasErrors;
        }
    }
}