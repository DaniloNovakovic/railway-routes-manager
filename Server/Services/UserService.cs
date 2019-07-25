using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading;
using Common;
using Server.Core;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService()
        {
        }

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region CRUD

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

        #endregion CRUD

        #region Authentication

        public bool IsLoggedIn(string username)
        {
            // how?

            throw new NotImplementedException();
        }

        public void Login(string username, string password)
        {
            var user = _unitOfWork.Users.Get(u => u.Username == username && u.Password == password);
            string currPrincipalName = Thread.CurrentPrincipal.Identity.Name;
            Trace.TraceInformation($"currPrincipal: {currPrincipalName}, username: {username}");
            // wat now??
        }

        public void Logout(string username)
        {
            string currPrincipalName = Thread.CurrentPrincipal.Identity.Name;
            Trace.TraceInformation($"currPrincipal: {currPrincipalName}, username: {username}");
        }

        #endregion Authentication
    }
}