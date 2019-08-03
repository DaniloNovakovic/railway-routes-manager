using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Client.Helpers;
using Prism.Commands;

namespace Client.ViewModels
{
    public class UserListViewModel : ViewModelBase
    {
        private readonly IUserService _userService;

        public UserListViewModel(IUserService userService)
        {
            _userService = userService;

            UserList = new ObservableCollection<UserModel>();
            RefreshListCommand = new DelegateCommand(async () => await RefreshUserListAsync());
        }

        public ICommand RefreshListCommand { get; set; }
        public UserModel SelectedUser { get; set; }
        public ObservableCollection<UserModel> UserList { get; }

        public override Task OnLoadedAsync()
        {
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

        private async Task SafeExecuteAsync(Func<Task> callback)
        {
            try
            {
                await callback?.Invoke();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}