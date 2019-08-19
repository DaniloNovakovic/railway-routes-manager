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
        private readonly ILogger _logger;

        public UserService(IAuthChannelFactory factory, IMapper mapper, ILogger logger)
        {
            _factory = factory;
            _mapper = mapper;
            _logger = logger;
        }

        public Task AddUserAsync(UserModel user)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                var userDto = _mapper.Map<Common.UserDto>(user);
                int id = proxy.Add(userDto);
                _logger.Info($"Added user '{user.Username}' [{id}]");
            });
        }

        public Task DeleteUserAsync(int id)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                proxy.Remove(id);
                _logger.Info($"Removed user {id}");
            });
        }

        public Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return Task.Run(() =>
            {
                _logger.Debug("Getting all users...");
                var proxy = GetProxy();
                var userDtos = proxy.GetAll();
                return userDtos.Select(dto => _mapper.Map<UserModel>(dto));
            });
        }

        public Task<UserModel> GetCurrentUserAsync()
        {
            return Task.Run(() =>
            {
                _logger.Debug("Getting current user...");
                var proxy = GetProxy();
                var userDto = proxy.GetByUsername(_factory.Username);
                return _mapper.Map<UserModel>(userDto);
            });
        }

        public Task UpdateUserAsync(UserModel user)
        {
            return Task.Run(() =>
            {
                var proxy = GetProxy();
                var userDto = _mapper.Map<Common.UserDto>(user);
                proxy.Update(user.Id, userDto);
                _logger.Info($"Updated user {user.Id}");
            });
        }

        private Common.IUserService GetProxy()
        {
            return _factory.GetChannelFactory<Common.IUserService>(port).CreateChannel();
        }
    }
}