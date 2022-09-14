using Invoice.Application.Services;
using Invoice.Domain.Interfaces;
using Moq;
using Xunit;

namespace Invoice.Application.Tests.UnitTests
{
    public class UserPasswordServiceTests
    {
        private readonly Mock<IPasswordEncryption> _mockPasswordEncryption;
        public UserPasswordServiceTests()
        {
            _mockPasswordEncryption = new Mock<IPasswordEncryption>();
        }

        [Fact]
        public void GeneratePassword_Length_Should_Be_53()
        {
            // Arrange
            string userName = "pepe";
            string password = "12345678";
            int saltLength = 16;
            string salt = "4JbuCHQU7SEY7jHj+5m0gw==";
            string hash = "ZPVpoPFC4LWmNGj0UGjY7nER7RU=";
            _mockPasswordEncryption.Setup(x => x.GenerateSalt(It.IsAny<int>())).Returns(salt);
            _mockPasswordEncryption.Setup(x => x.GenerateHash(It.IsAny<string>(), It.IsAny<string>())).Returns(hash);
            var userPasswordService = new UserPasswordService(_mockPasswordEncryption.Object);

            //Act
            var encryptedPassword = userPasswordService.GeneratePassword(userName, password, saltLength);

            //Assert
            Assert.Equal(53 , encryptedPassword.Length);
        }        

        [Fact]
        public void IsCorrectPassword_Should_Be_True()
        {
            // Arrange
            string userName = "pepe";
            string salt = "4JbuCHQU7SEY7jHj+5m0gw==";
            string hash = "ZPVpoPFC4LWmNGj0UGjY7nER7RU=";
            string saltHashPassword = $"{salt},{hash}";
            string password = "12345678";
            _mockPasswordEncryption.Setup(x => x.GenerateHash(It.IsAny<string>(), It.IsAny<string>())).Returns(hash);
            var userPasswordService = new UserPasswordService(_mockPasswordEncryption.Object);

            //Act
            var isCorrectPassword = userPasswordService.IsCorrectPassword(userName, saltHashPassword, password);

            //Assert
            Assert.True(isCorrectPassword);
        }                
    }
}
