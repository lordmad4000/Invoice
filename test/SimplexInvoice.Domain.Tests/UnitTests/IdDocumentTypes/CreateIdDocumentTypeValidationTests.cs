using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.IdDocumentTypes;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests
{
    public class CreateIdDocumentTypeValidationTests
    {
        [Fact]
        public void Should_Not_Be_Throw_BusinessRuleValidationException()
        {
            //Arrange

            //Act
            var exception = Record.Exception(() => IdDocumentType.Create("DNI"));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Empty_Name_Should_Be_Throw_BusinessRuleValidationException()
        {
            // Arrange            
            
            //Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => IdDocumentType.Create(""));
        }

        [Fact]
        public void Null_Name_Should_Be_Throw_BusinessRuleValidationException()
        {
            // Arrange
            
            //Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => IdDocumentType.Create(null));
        }        

        [Fact]
        public void Name_Length_Greater_Than_20_Should_Be_Throw_BusinessRuleValidationException()
        {   
            //Arrange

            //Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => IdDocumentType.Create("DNI__________________"));
        }

    }
}