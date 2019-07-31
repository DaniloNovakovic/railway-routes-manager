using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Prism.Commands;
using Prism.Mvvm;

namespace Client.ViewModels
{
    public class ProfileViewModel : BindableBase
    {
        private readonly IUserService _userService;
        private UserModel _userModel;

        public ProfileViewModel(IUserService userService)
        {
            _userService = userService;
            SaveChangesCommand = new DelegateCommand(async () => await SaveChangesAsync());
            OnLoadedCommand = new DelegateCommand(async () => await OnLoadedAsync());
        }

        public ICommand OnLoadedCommand { get; }

        public ICommand SaveChangesCommand { get; }

        public UserModel UserModel
        {
            get { return _userModel; }
            set { SetProperty(ref _userModel, value); }
        }

        public Task OnLoadedAsync()
        {
            return SafeExecuteAsync(async () =>
            {
                UserModel = await _userService.GetCurrentUserAsync();
            });
        }

        public async Task SafeExecuteAsync(Func<Task> callback)
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

        public Task SaveChangesAsync()
        {
            return SafeExecuteAsync(() => _userService.UpdateUserAsync(UserModel));
        }
    }
}