using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Client.Core;
using Client.Helpers;
using Prism.Commands;

namespace Client.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private bool _canSaveChanges = true;
        private UserModel _userModel;

        public ProfileViewModel(IUserService userService, ILogger logger) : base(logger)
        {
            _userService = userService;

            SaveChangesCommand = new DelegateCommand(async () => await SaveChangesAsync());
        }

        public bool CanSaveChanges
        {
            get { return _canSaveChanges; }
            set { SetProperty(ref _canSaveChanges, value); }
        }

        public ICommand SaveChangesCommand { get; }

        public UserModel UserModel
        {
            get { return _userModel; }
            set { SetProperty(ref _userModel, value); }
        }

        public override Task OnLoadedAsync()
        {
            return SafeExecuteAsync(async () =>
            {
                UserModel = await _userService.GetCurrentUserAsync();
            });
        }

        public Task SaveChangesAsync()
        {
            if (UserModel.HasErrors)
            {
                HandleUserModelErrors();
                return Task.CompletedTask;
            }

            return SafeExecuteAsync(
                @try: () =>
                {
                    CanSaveChanges = false;
                    return _userService.UpdateUserAsync(UserModel);
                },
                @finally: () => CanSaveChanges = true
            );
        }

        private void HandleUserModelErrors()
        {
            var list = DictionaryFlattener.Flatten(UserModel.GetAllErrors());
            Logger.Warn(string.Join(Environment.NewLine, list));
        }
    }
}