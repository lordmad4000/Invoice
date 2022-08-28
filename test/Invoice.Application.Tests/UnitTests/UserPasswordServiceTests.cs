using Invoice.Application.Services;
using Invoice.Domain.Interfaces;
using Moq;
using Xunit;

namespace Invoice.Application.Tests.UnitTests
{
    public class UserPasswordServiceTests
    {
        [Fact]
        public void GeneratePassword_Length_Should_Be_53()
        {
            // Arrange
            string userName = "pepe";
            string password = "12345678";
            int saltLength = 16;
            string salt = "4JbuCHQU7SEY7jHj+5m0gw==";
            string hash = "ZPVpoPFC4LWmNGj0UGjY7nER7RU=";
            var mockPasswordEncryption = new Mock<IPasswordEncryption>();
            mockPasswordEncryption.Setup(x => x.GenerateSalt(It.IsAny<int>())).Returns(salt);
            mockPasswordEncryption.Setup(x => x.GenerateHash($"{userName}{password}", salt)).Returns(hash);
            var userPasswordService = new UserPasswordService(mockPasswordEncryption.Object);

            //Act
            var encryptedPassword = userPasswordService.GeneratePassword(userName, password, saltLength);

            //Assert
            Assert.Equal(encryptedPassword.Length, 53);
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
            var mockPasswordEncryption = new Mock<IPasswordEncryption>();
            mockPasswordEncryption.Setup(x => x.GenerateHash($"{userName}{password}", salt)).Returns(hash);
            var userPasswordService = new UserPasswordService(mockPasswordEncryption.Object);

            //Act
            var isCorrectPassword = userPasswordService.IsCorrectPassword(userName, saltHashPassword, password);

            //Assert
            Assert.Equal(isCorrectPassword, true);
        }                
    }
}
