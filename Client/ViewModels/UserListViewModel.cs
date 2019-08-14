using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Client.Helpers;
using Prism.Commands;
using Prism.Events;

namespace Client.ViewModels
{
    public class UserListViewModel : ViewModelBase
    {
        private readonly IUserService _userService;

        private bool _isDialogOpen = false;

        public UserListViewModel(IUserService userService, IEventAggregator eventAggregator)
        {
            _userService = userService;

            UserList = new ObservableCollection<UserModel>();
            RefreshListCommand = new DelegateCommand(async () => await RefreshUserListAsync());
            ShowDialogCommand = new DelegateCommand(ShowDialog);

            eventAggregator.GetEvent<UserAddedEvent>()
                .Subscribe(async _ => await OnUserAddedAsync(), ThreadOption.UIThread);
        }

        public bool IsDialogOpen
        {
            get { return _isDialogOpen; }
            set { SetProperty(ref _isDialogOpen, value); }
        }

        public ICommand RefreshListCommand { get; set; }

        public UserModel SelectedUser { get; set; }

        public ICommand ShowDialogCommand { get; set; }

        public ObservableCollection<UserModel> UserList { get; }

        public override Task OnLoadedAsync()
        {
            return RefreshUserListAsync();
        }

        private Task OnUserAddedAsync()
        {
            IsDialogOpen = false;
            return RefreshUserListAsync();
        }

        private Task RefreshUserListAsync()
        {
            return SafeExecuteAsync(async () =>
            {
                var users = await _userService.GetAllUsersAsync();
                UserList.Clear();
                UserList.AddRange(users);
            });
        }

        private void ShowDialog()
        {
            IsDialogOpen = true;
        }
    }
}