using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Client.Core;
using Client.Helpers;

namespace Client.ViewModels
{
    public class UserListViewModel : ViewModelBase
    {
        private readonly IUserService _userService;

        public UserListViewModel(IUserService userService)
        {
            UserList = new ObservableCollection<UserModel>();
            _userService = userService;
        }

        public UserModel SelectedUser { get; set; }
        public ObservableCollection<UserModel> UserList { get; }

        public override async Task OnLoadedAsync()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                UserList.Clear();
                UserList.AddRange(users);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}