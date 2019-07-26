using System;
using System.Collections.Generic;
using System.ServiceModel;
using Common;
using Server.Core;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(UserDto entity)
        {
            throw new NotImplementedException();
        }

        public UserDto Get(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int key)
        {
            throw new NotImplementedException();
        }

        public void Update(int key, UserDto entity)
        {
            throw new NotImplementedException();
        }
    }
}