using System;
using System.IdentityModel.Selectors;
using Common;
using Server.Core;

namespace Server
{
    public class CustomUserNamePasswordValidator : UserNamePasswordValidator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public CustomUserNamePasswordValidator(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException(nameof(userName));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(nameof(password));
            }

            var user = _unitOfWork.Users.Get(u => u.Username == userName);

            if (user == null)
            {
                throw new NotFoundException($"User `{userName}` not found!");
            }

            if (user.Password != password)
            {
                throw new InvalidPasswordException();
            }

            _logger.Debug($"User {userName} successfully authenticated!");
        }
    }
}