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
        private bool _canSaveChanges = true;
        private UserModel _userModel;

        public ProfileViewModel(IUserService userService)
        {
            _userService = userService;
            SaveChangesCommand = new DelegateCommand(async () => await SaveChangesAsync());
            OnLoadedCommand = new DelegateCommand(async () => await OnLoadedAsync());
        }

        public bool CanSaveChanges
        {
            get { return _canSaveChanges; }
            set { SetProperty(ref _canSaveChanges, value); }
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
                CanSaveChanges = false;
                await callback?.Invoke();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.InnerException?.Message ?? ex.Message);
            }
            finally
            {
                CanSaveChanges = true;
            }
        }

        public Task SaveChangesAsync()
        {
            if (UserModel.HasErrors)
            {
                HandleUserModelErrors();
                return Task.CompletedTask;
            }
            return SafeExecuteAsync(() => _userService.UpdateUserAsync(UserModel));
        }

        private void HandleUserModelErrors()
        {
            var list = DictionaryFlattener.Flatten(UserModel.GetAllErrors());
            Trace.TraceWarning(string.Join(Environment.NewLine, list));
        }
    }
}