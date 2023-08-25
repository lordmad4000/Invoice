using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.IdDocumentTypes;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests
{
    public class UpdateIdDocumentTypeValidationTests
    {
        [Fact]
        public void Should_Not_Be_Throw_BusinessRuleValidationException()
        {   
            //Arrange
            var idDocumentType = IdDocumentType.Create("DNI");

            //Act
            var exception = Record.Exception(() => idDocumentType.Update("NIF"));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Empty_Name_Should_Be_Throw_BusinessRuleValidationException()
        {
            // Arrange
            var idDocumentType = IdDocumentType.Create("DNI");
            
            //Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => idDocumentType.Update(""));
        }

        [Fact]
        public void Null_Name_Should_Be_Throw_BusinessRuleValidationException()
        {
            // Arrange
            var idDocumentType = IdDocumentType.Create("DNI");
            
            //Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => idDocumentType.Update(null));
        }        

        [Fact]
        public void Name_Length_Greater_Than_20_Should_Be_Throw_BusinessRuleValidationException()
        {   
            //Arrange
            var idDocumentType = IdDocumentType.Create("DNI");

            //Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => idDocumentType.Update("DNI__________________"));
        }

    }
}