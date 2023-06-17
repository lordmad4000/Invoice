using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.ValueObjects;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests
{
    public class PhoneNumberValidationTests
    {
        [Fact]
        public void Should_Not_Be_Throw_NotValidPhoneNumberException()
        {
            //Arrange

            //Act
            var exception = Record.Exception(() => new PhoneNumber("+34 689 45 96 34"));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Empty_Phone_Should_Be_Throw_NotValidPhoneNumberException()
        {
            // Arrange            

            //Act & Assert
            Assert.Throws<NotValidPhoneNumberException>(() => new PhoneNumber(""));
        }

        [Fact]
        public void Null_Phone_Should_Be_Throw_NotValidPhoneNumberException()
        {
            // Arrange            

            //Act & Assert
            Assert.Throws<NotValidPhoneNumberException>(() => new PhoneNumber(null));
        }

        [Theory]
        [InlineData("+34 91 776 91 42")]
        [InlineData("+34 689 54 91 32")]
        [InlineData("+34 934 65 89 22")]
        [InlineData("+01 718 222 2222")]
        public void Phone_Should_Not_Be_Throw_NotValidPhoneNumberException(string phone)
        {
            // Arrange            

            //Act
            var exception = Record.Exception(() => new PhoneNumber(phone));

            // Assert
            Assert.Null(exception);
        }

        [Theory]
        [InlineData("+34 91 776 91 428")]
        [InlineData("+34 689 54 9111 32")]
        [InlineData("+34 934 6511 89 224")]
        [InlineData("+01 718 222 22229")]
        public void Phone_Should_Be_Throw_NotValidPhoneNumberException(string phone)
        {
            // Arrange            

            //Act && Assert
            Assert.Throws<NotValidPhoneNumberException>(() => new PhoneNumber(phone));
        }

    }
}