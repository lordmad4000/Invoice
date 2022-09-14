using Invoice.Domain.Entities;
using Invoice.Domain.Validations;
using Invoice.Domain.ValueObjects;
using System.Linq;
using Xunit;

namespace Invoice.Domain.Tests.UnitTests
{
    public class UpdateUserValidatorTests
    {
        private UpdateUserValidator _validator;
        public UpdateUserValidatorTests()
        {
            _validator = new UpdateUserValidator();
        }

        [Fact]
        public void Should_Be_Valid()
        {   
            //Arrange
            var user = new User(new EmailAddress("jose@gmail.com"), "123456", "jose", "antonio");

            //Act
            var result = _validator.Validate(user);

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Email_Length_Should_Be_Greater_Than_40()
        {   
            //Arrange
            var user = new User(new EmailAddress("joseaaaaaaaaaaaaaaaaaaaaaaaaaaa@gmail.com"), "123456", "jose", "antonio");

            //Act
            var result = _validator.Validate(user);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.ErrorCode == "LengthValidator");
        }

        [Fact]
        public void FirstName_Should_Be_Empty()
        {
            // Arrange
            var user = new User(new EmailAddress("jose@gmail.com"), "123456", "", "antonio");
            
            //Act
            var result = _validator.Validate(user);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.ErrorCode == "NotEmptyValidator");
        }

        [Fact]
        public void FirstName_Should_Be_Null()
        {
            // Arrange
            var user = new User(new EmailAddress("jose@gmail.com"), "123456", null, "antonio");
            
            //Act
            var result = _validator.Validate(user);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.ErrorCode == "NotNullValidator");
        }        

        [Fact]
        public void FirstName_Length_Should_Be_Greater_Than_20()
        {   
            //Arrange
            var user = new User(new EmailAddress("jose@gmail.com"), "01234567890", "joseeeeeeeeeeeeeeeeee", "antonio");

            //Act
            var result = _validator.Validate(user);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.ErrorCode == "LengthValidator");
        }

        [Fact]
        public void LastName_Should_Be_Empty()
        {
            // Arrange
            var user = new User(new EmailAddress("jose@gmail.com"), "123456", "jose", "");
            
            //Act
            var result = _validator.Validate(user);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.ErrorCode == "NotEmptyValidator");
        }

        [Fact]
        public void LastName_Should_Be_Null()
        {
            // Arrange
            var user = new User(new EmailAddress("jose@gmail.com"), "123456", "jose", null);
            
            //Act
            var result = _validator.Validate(user);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.ErrorCode == "NotNullValidator");
        }        

        [Fact]
        public void LastName_Length_Should_Be_Greater_Than_20()
        {   
            //Arrange
            var user = new User(new EmailAddress("jose@gmail.com"), "01234567890", "joseeeeeeeeeeeeeeeeee", "antonioooooooooooooooo");

            //Act
            var result = _validator.Validate(user);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.ErrorCode == "LengthValidator");
        }

    }
}