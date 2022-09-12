using Invoice.Infra.Services;
using Xunit;

namespace Invoice.Infra.Tests.UnitTests
{
    public class PasswordEncryptServiceTests
    {
        [Theory]
        [InlineData(16, 24)]
        [InlineData(8, 12)]
        [InlineData(10, 16)]
        public void GenerateSalt_Length_Should_Be(int length, int expected)
        {
            // Arrange
            var passwordEncryptService = new PasswordEncryptService();

            // Act
            string salt = passwordEncryptService.GenerateSalt(length);

            // Assert
            Assert.Equal(expected, salt.Length);
        }

        [Theory]
        [InlineData("pepe", "12345678", "6egJOi1l5Zz6ZCLk55NI8S7SisM=")] 
        [InlineData("joaquin", "rxjdirld2d", "LdNCmq4aexcqDrjnDHbl/ADzMDk=")]
        [InlineData("felipe", "54oDpR_c23E4", "FZuGsiXM5NX6QZ39AvsuH32yhIk=")]
        public void GenerateHash_Should_Be(string pass, string salt, string expected)
        {
            // Arrange
            var passwordEncryptService = new PasswordEncryptService();

            // Act
            string hash = passwordEncryptService.GenerateHash(pass, salt);

            // Assert
            Assert.Equal(expected, hash);
        }
    }
}
