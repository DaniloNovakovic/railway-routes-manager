using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Client.Helpers;
using Prism.Commands;
using Prism.Mvvm;

namespace Client.ViewModels
{
    public class UserListViewModel : ViewModelBase
    {
        private readonly IUserService _userService;

        private BindableBase _formViewModel;

        private bool _isDialogOpen = false;

        public UserListViewModel(IUserService userService)
        {
            _userService = userService;

            UserList = new ObservableCollection<UserModel>();
            RefreshListCommand = new DelegateCommand(async () => await RefreshUserListAsync());
            ShowDialogCommand = new DelegateCommand(ShowDialog);
        }

        public BindableBase FormViewModel
        {
            get { return _formViewModel; }
            set { SetProperty(ref _formViewModel, value); }
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

        private void ShowDialog()
        {
            FormViewModel = new AddUserFormViewModel(_userService, OnUserAddedAsync);
            IsDialogOpen = true;
        }
    }
}