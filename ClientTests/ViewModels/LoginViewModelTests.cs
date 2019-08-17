using System.Threading.Tasks;
using Client.Core;
using Moq;
using Prism.Regions;
using Xunit;

namespace Client.ViewModels.Tests
{
    public class LoginViewModelTests
    {
        private readonly Mock<IAuthenticationService> _authServiceMock;
        private readonly Mock<ILogger> _loggerMock;
        private readonly Mock<IRegionManager> _regionManagerMock;
        private readonly LoginViewModel _sut;

        public LoginViewModelTests()
        {
            _authServiceMock = new Mock<IAuthenticationService>();
            _regionManagerMock = new Mock<IRegionManager>();
            _loggerMock = new Mock<ILogger>();
            _sut = new LoginViewModel(_authServiceMock.Object, _regionManagerMock.Object, _loggerMock.Object);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("djura28", "")]
        [InlineData("djura28", "1")]
        [InlineData("", "nikolica123")]
        [InlineData("1", "nikolica123")]
        public async Task Login_WhenLoginModelIsInvalid_DoNotCallAuthService(string username, string password)
        {
            _sut.LoginModel.Username = username;
            _sut.LoginModel.Password = password;

            await _sut.LoginClickAsync(null).ConfigureAwait(false);

            _authServiceMock.Verify(m => m.LoginAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Theory]
        [InlineData("admin", "admin")]
        public async Task Login_WhenLoginModelIsValid_CallAuthService(string username, string password)
        {
            _sut.LoginModel.Username = username;
            _sut.LoginModel.Password = password;

            await _sut.LoginClickAsync(null).ConfigureAwait(false);

            _authServiceMock.Verify(m => m.LoginAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}