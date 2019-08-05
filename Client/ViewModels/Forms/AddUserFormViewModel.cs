using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Client.ViewModels
{
    public class AddUserFormViewModel : BindableBase
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

            return SafeExecuteAsync(async () =>
            {
                await _userService.AddUserAsync(UserModel);
                OnUserAdded();
                UserModel = new UserModel();
            });
        }

        private void OnUserAdded()
        {
            _eventAggregator.GetEvent<UserAddedEvent>().Publish(UserModel.Username);
        }

        private async Task SafeExecuteAsync(Func<Task> callback)
        {
            try
            {
                CanAddUser = false;
                await callback?.Invoke();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.InnerException?.Message ?? ex.Message);
            }
            finally
            {
                CanAddUser = !UserModel.HasErrors;
            }
        }

        private void UserModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            CanAddUser = !UserModel.HasErrors;
        }
    }
}