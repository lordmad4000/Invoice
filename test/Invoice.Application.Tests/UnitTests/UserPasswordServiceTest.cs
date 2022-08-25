using Invoice.Application.Services;
using Invoice.Infra.Services;
using Xunit;

namespace Invoice.Application.Tests.UnitTests
{
    public class UserPasswordServiceTest
    {
        [Theory]
        [InlineData("pepe", "12345678", 16, 53)]
        [InlineData("joaquin", "rxjdirld2d", 8, 41)]
        [InlineData("felipe", "54oDpR_c23E4", 2, 33)]
        public void GeneratePassword_Length_Should_Be(string userName, string password, int saltLength, int expected)
        {
            var passwordEncryptService = new PasswordEncryptService();
            var userPasswordService = new UserPasswordService(passwordEncryptService);

            var encryptedPassword = userPasswordService.GeneratePassword(userName, password, saltLength);

            Assert.Equal(encryptedPassword.Length, expected);
        }
    }
}
