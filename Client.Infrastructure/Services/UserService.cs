using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Client.Core;

namespace Client.Infrastructure
{
    public class UserService : IUserService
    {
        private const ushort port = Common.Ports.UserServicePort;
        private readonly IAuthChannelFactory _factory;
        private readonly IMapper _mapper;

        public UserService(IAuthChannelFactory factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public Task AddUserAsync(UserModel user)
        {
            return Task.Run(() =>
            {
                var proxy = _factory.GetChannelFactory<Common.IUserService>(port).CreateChannel();
                var userDto = _mapper.Map<Common.UserDto>(user);
                proxy.Add(userDto);
            });
        }

        public Task DeleteUserAsync(int id)
        {
            return Task.Run(() =>
            {
                var proxy = _factory.GetChannelFactory<Common.IUserService>(port).CreateChannel();
                proxy.Remove(id);
            });
        }

        public Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return Task.Run(() =>
            {
                var proxy = _factory.GetChannelFactory<Common.IUserService>(port).CreateChannel();
                var userDtos = proxy.GetAll();
                return userDtos.Select(dto => _mapper.Map<UserModel>(dto));
            });
        }

        public Task<UserModel> GetCurrentUserAsync()
        {
            return Task.Run(() =>
            {
                var proxy = _factory.GetChannelFactory<Common.IUserService>(port).CreateChannel();
                var userDto = proxy.GetByUsername(_factory.Username);
                return _mapper.Map<UserModel>(userDto);
            });
        }

        public Task UpdateUserAsync(UserModel user)
        {
            return Task.Run(() =>
            {
                var proxy = _factory.GetChannelFactory<Common.IUserService>(port).CreateChannel();
                var userDto = _mapper.Map<Common.UserDto>(user);
                proxy.Update(user.Id, userDto);
            });
        }
    }
}