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

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(UserDto entity)
        {
            var user = _mapper.Map<User>(entity);

            if (string.IsNullOrWhiteSpace(user.RoleName))
            {
                user.RoleName = RoleNames.RegularUser;
            }

            _unitOfWork.Users.Add(user);
            _unitOfWork.SaveChanges();
        }

        public UserDto Get(int key)
        {
            var user = _unitOfWork.Users.Get(key);
            return _mapper.Map<UserDto>(user);
        }

        public IEnumerable<UserDto> GetAll()
        {
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
            var user = _unitOfWork.Users.Get(u => u.Username == username);
            return _mapper.Map<UserDto>(user);
        }

        public void Remove(int key)
        {
            var user = _unitOfWork.Users.Get(key);
            _unitOfWork.Users.Remove(user);
            _unitOfWork.SaveChanges();
        }

        public void Update(int key, UserDto entity)
        {
            var user = _unitOfWork.Users.Get(key);
            _mapper.Map(source: entity, destination: user);
            _unitOfWork.SaveChanges();
        }
    }
}