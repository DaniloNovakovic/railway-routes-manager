using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Prism.Commands;
using Prism.Mvvm;

namespace Client.ViewModels
{
    public class AddUserFormViewModel : BindableBase
    {
        private readonly Func<Task> _onUserAddedAsync;
        private readonly IUserService _userService;
        private UserModel _userModel;

        public AddUserFormViewModel(IUserService userService) : this(userService, null)
        {
        }

        public AddUserFormViewModel(IUserService userService, Func<Task> OnUserAddedAsync)
        {
            _userService = userService;
            UserModel = new UserModel();
            AddUserCommand = new DelegateCommand(async () => await AddUserAsync());
            UserModel.ErrorsChanged += UserModel_ErrorsChanged;
            _onUserAddedAsync = OnUserAddedAsync;
        }

        public ICommand AddUserCommand { get; set; }

        public bool CanLogIn => !UserModel.HasErrors;

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

            return SafeExecuteAsync(async () =>
            {
                await _userService.AddUserAsync(UserModel);
                await _onUserAddedAsync?.Invoke();
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

        private void UserModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanLogIn)));
        }
    }
}