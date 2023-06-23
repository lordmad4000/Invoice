using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.ValueObjects;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests
{
    public class MoneyValidationTests
    {
        [Fact]
        public void Should_Not_Be_Throw_NotValidMoneyException()
        {
            //Arrange

            //Act
            var exception = Record.Exception(() => new Money("EUR", 20.5));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Empty_Currency_Should_Be_Throw_NotValidMoneyException()
        {
            //Arrange            

            //Act & Assert
            Assert.Throws<NotValidMoneyException>(() => new Money("", 4.50));
        }

        [Fact]
        public void Null_Currency_Should_Be_Throw_NotValidMoneyException()
        {
            //Arrange            

            //Act & Assert
            Assert.Throws<NotValidMoneyException>(() => new Money(null, 2.4));
        }

        [Theory]
        [InlineData("USD")]
        [InlineData("EUR")]
        [InlineData("CAD")]
        [InlineData("GBP")]
        public void Currency_Should_Not_Be_Throw_NotValidMoneyException(string currency)
        {
            //Arrange            

            //Act
            var exception = Record.Exception(() => new Money(currency, 2.4));

            //Assert
            Assert.Null(exception);
        }

        [Theory]
        [InlineData("U")]
        [InlineData("CA")]
        [InlineData("EURO")]
        public void Currency_Should_Be_Throw_NotValidMoneyException(string currency)
        {
            //Arrange            

            //Act && Assert
            Assert.Throws<NotValidMoneyException>(() => new Money(currency, 2.4));
        }

    }
}