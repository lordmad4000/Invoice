using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.ValueObjects;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests
{
    public class AddressValidationTests
    {
        [Fact]
        public void Should_Not_Be_Throw_NotValidAddressException()
        {
            //Arrange

            //Act
            var exception = Record.Exception(() => new Address("24, White Dog St.",
                                                               "Kingston",
                                                               "New York",
                                                               "12401"));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Empty_Street_Should_Be_Throw_NotValidAddressException()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NotValidAddressException>(() => new Address("",
                                                                      "Kingston",
                                                                      "New York",
                                                                      "12401"));
        }

        [Fact]
        public void Null_Street_Should_Be_Throw_NotValidAddressException()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NotValidAddressException>(() => new Address(null,
                                                                      "Kingston",
                                                                      "New York",
                                                                      "12401"));
        }

        [Fact]
        public void Street_Length_Greater_Than_40_Should_Be_Throw_NotValidAddressException()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NotValidAddressException>(() => new Address("24, White Dog St.________________________",
                                                                      "Kingston",
                                                                      "New York",
                                                                      "12401"));
        }

        [Fact]
        public void Empty_City_Should_Be_Throw_NotValidAddressException()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NotValidAddressException>(() => new Address("24, White Dog St.",
                                                                      "",
                                                                      "New York",
                                                                      "12401"));
        }

        [Fact]
        public void Null_City_Should_Be_Throw_NotValidAddressException()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NotValidAddressException>(() => new Address("24, White Dog St.",
                                                                      null,
                                                                      "New York",
                                                                      "12401"));
        }

        [Fact]
        public void City_Length_Greater_Than_40_Should_Be_Throw_NotValidAddressException()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NotValidAddressException>(() => new Address("24, White Dog St.",
                                                                      "Kingston_________________________________",
                                                                      "New York",
                                                                      "12401"));
        }

        [Fact]
        public void Empty_Country_Should_Be_Throw_NotValidAddressException()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NotValidAddressException>(() => new Address("24, White Dog St.",
                                                                      "Kingston",
                                                                      "",
                                                                      "12401"));
        }

        [Fact]
        public void Null_Country_Should_Be_Throw_NotValidAddressException()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NotValidAddressException>(() => new Address("24, White Dog St.",
                                                                      "Kingston",
                                                                      null,
                                                                      "12401"));
        }

        [Fact]
        public void Country_Length_Greater_Than_40_Should_Be_Throw_NotValidAddressException()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NotValidAddressException>(() => new Address("24, White Dog St.",
                                                                      "Kingston",
                                                                      "New York_________________________________",
                                                                      "12401"));
        }

        [Fact]
        public void Empty_PostalCode_Should_Be_Throw_NotValidAddressException()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NotValidAddressException>(() => new Address("24, White Dog St.",
                                                                      "Kingston",
                                                                      "New York",
                                                                      ""));
        }

        [Fact]
        public void Null_PostalCode_Should_Be_Throw_NotValidAddressException()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NotValidAddressException>(() => new Address("24, White Dog St.",
                                                                      "Kingston",
                                                                      "New York",
                                                                      null));
        }

        [Fact]
        public void PostalCode_Length_Greater_Than_40_Should_Be_Throw_NotValidAddressException()
        {
            //Arrange

            //Act & Assert
            Assert.Throws<NotValidAddressException>(() => new Address("24, White Dog St.",
                                                                      "Kingston",
                                                                      "New York",
                                                                      "12401____________________________________"));
        }

    }
}