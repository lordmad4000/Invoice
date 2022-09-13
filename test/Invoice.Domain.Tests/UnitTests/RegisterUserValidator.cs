using System;
using System.Linq;
using Invoice.Domain.Entities;
using Invoice.Domain.Validations;
using Invoice.Domain.ValueObjects;
using Moq;
using Xunit;

namespace Invoice.Domain.Tests.UnitTests
{
    public class RegisterUserValidatorTests
    {
        private RegisterUserValidator _validator;
        public RegisterUserValidatorTests()
        {
            _validator = new RegisterUserValidator();
        }

        [Fact]
        public void Password_Should_Be_Empty()
        {
            // Arrange
            var user = new User(new EmailAddress("jose@gmail.com"), "", "jose", "antonio");
            
            //Act
            var result = _validator.Validate(user);

            //Assert
            Assert.True(result.Errors.Any(x => x.ErrorCode == "NotEmptyValidator"));
        }

        [Fact]
        public void Password_Length_Should_Be_Less_Than_6()
        {   
            //Arrange
            var user = new User(new EmailAddress("jose@gmail.com"), "12345", "jose", "antonio");

            //Act
            var result = _validator.Validate(user);

            //Assert
            Assert.True(result.Errors.Any(x => x.ErrorCode == "LengthValidator"));
        }

        [Fact]
        public void Password_Length_Should_Be_Greater_Than_10()
        {   
            //Arrange
            var user = new User(new EmailAddress("jose@gmail.com"), "01234567890", "jose", "antonio");

            //Act
            var result = _validator.Validate(user);

            //Assert
            Assert.True(result.Errors.Any(x => x.ErrorCode == "LengthValidator"));
        }

        [Fact]
        public void Email_Length_Should_Be_Greater_Than_40()
        {   
            //Arrange
            var user = new User(new EmailAddress("joseaaaaaaaaaaaaaaaaaaaaaaaaaaa@gmail.com"), "123456", "jose", "antonio");

            //Act
            var result = _validator.Validate(user);

            //Assert
            Assert.True(result.Errors.Any(x => x.ErrorCode == "LengthValidator"));
        }

    }
}