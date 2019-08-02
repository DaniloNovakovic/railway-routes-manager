using System.Collections.ObjectModel;
using Client.Core;
using Client.Helpers;

namespace Client.ViewModels
{
    public class UserListViewModel : ViewModelBase
    {
        public UserListViewModel()
        {
            UserList = new ObservableCollection<UserModel>()
            {
                new UserModel()
                {
                    Username = "admin",
                    Password = "admin",
                    FirstName = "Danilo",
                    LastName = "Novakovic",
                    RoleName = "Administrator",
                    Id = 1
                }
            };
        }

        public UserModel SelectedUser { get; set; }
        public ObservableCollection<UserModel> UserList { get; }
    }
}