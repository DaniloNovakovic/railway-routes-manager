using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Core
{
    public interface IUserService
    {
        Task<UserModel> GetCurrentUserAsync();

        Task<IEnumerable<UserModel>> GetAllUsersAsync();

        Task UpdateUserAsync(UserModel user);

        Task DeleteUserAsync(int id);
    }
}