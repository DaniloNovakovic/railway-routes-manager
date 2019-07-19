using Client.Core;
using Moq;
using Xunit;

namespace Client.ViewModels.Tests
{
    public class LoginViewModelTests
    {
        private readonly Mock<IAuthenticationService> _authServiceMock;
        private readonly LoginViewModel _sut;

        public LoginViewModelTests()
        {
            _authServiceMock = new Mock<IAuthenticationService>();
            _sut = new LoginViewModel(_authServiceMock.Object, null);
        }

        [Theory]
        [InlineData("admin", "admin")]
        public void Login_WhenLoginModelIsValid_CallAuthService(string username, string password)
        {
            _sut.LoginModel.Username = username;
            _sut.LoginModel.Password = password;

            _sut.LoginCommand.Execute(null);

            _authServiceMock.Verify(m => m.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("djura28", "")]
        [InlineData("djura28", "1")]
        [InlineData("", "nikolica123")]
        [InlineData("1", "nikolica123")]
        public void Login_WhenLoginModelIsInvalid_DoNotCallAuthService(string username, string password)
        {
            _sut.LoginModel.Username = username;
            _sut.LoginModel.Password = password;

            _sut.LoginCommand.Execute(null);

            _authServiceMock.Verify(m => m.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}