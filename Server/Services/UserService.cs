using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
using Common;
using Server.Core;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public int Add(UserDto entity)
        {
            _logger.Info($"Adding new user...");

            var user = _mapper.Map<User>(entity);

            if (string.IsNullOrWhiteSpace(user.RoleName))
            {
                user.RoleName = RoleNames.RegularUser;
            }

            var addedUser = _unitOfWork.Users.Add(user);
            _unitOfWork.SaveChanges();

            _logger.Info($"User {addedUser.Id},'{addedUser.Username}' added");

            return addedUser.Id;
        }

        public UserDto Get(int key)
        {
            _logger.Info($"Getting user {key}...");

            var user = _unitOfWork.Users.Get(key);
            return _mapper.Map<UserDto>(user);
        }

        public IEnumerable<UserDto> GetAll()
        {
            _logger.Info($"Getting all users...");

            var users = _unitOfWork.Users.GetAll();
            return users.Select(u =>
            {
                var dto = _mapper.Map<UserDto>(u);
                dto.Password = "";
                return dto;
            }).ToList();
        }

        public UserDto GetByUsername(string username)
        {
            _logger.Info($"Getting user by username '{username}'...");

            var user = _unitOfWork.Users.Get(u => u.Username == username);
            return _mapper.Map<UserDto>(user);
        }

        public void Remove(int key)
        {
            _logger.Info($"Removing user {key}...");

            var user = _unitOfWork.Users.Get(key);
            _unitOfWork.Users.Remove(user);
            _unitOfWork.SaveChanges();
        }

        public void Update(int key, UserDto entity)
        {
            _logger.Info($"Updating user {key}...");

            var user = _unitOfWork.Users.Get(key);
            _mapper.Map(source: entity, destination: user);
            _unitOfWork.SaveChanges();
        }
    }
}