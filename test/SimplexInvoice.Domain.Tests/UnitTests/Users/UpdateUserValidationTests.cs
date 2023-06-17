using System;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Users;
using Xunit;

namespace SimplexInvoice.Domain.Tests.UnitTests
{
    public class UpdateUserValidationTests
    {
        [Fact]
        public void Should_Not_Be_Throw_BusinessRuleValidationException()
        {   
            //Arrange
            var user = User.Create("jose@gmail.com", "123456", "jose", "antonio");

            //Act
            var exception = Record.Exception(() => user.Update("jose1@gmail.com", "1234567", "jose1", "antonio1"));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Empty_Password_Should_Be_Throw_BusinessRuleValidationException()
        {
            // Arrange
            var user = User.Create("jose@gmail.com", "123456", "jose", "antonio");

            // Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => user.Update("jose@gmail.com", "", "jose", "antonio"));
        }

        [Fact]
        public void Null_Password_Should_Be_Throw_BusinessRuleValidationException()
        {
            // Arrange
            var user = User.Create("jose@gmail.com", "123456", "jose", "antonio");


            //Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => user.Update("jose@gmail.com", null, "jose", "antonio"));
        }

        [Fact]
        public void Empty_Email_Should_Be_Throw_NotValidEmailAddressException()
        {
            // Arrange
            var user = User.Create("jose@gmail.com", "123456", "jose", "antonio");
            
            //Act & Assert
            Assert.Throws<NotValidEmailAddressException>(() => user.Update("", "123456", "jose", "antonio"));
        }

        [Fact]
        public void Null_Email_Should_Be_Throw_NotValidEmailAddressException()
        {
            // Arrange
            var user = User.Create("jose@gmail.com", "123456", "jose", "antonio");
            
            //Act & Assert
            Assert.Throws<NotValidEmailAddressException>(() => user.Update(null, "123456", "jose", "antonio"));
        }        

        [Fact]
        public void Email_Length_Greater_Than_40_Should_Throw_BusinessRuleValidationException()
        {   
            //Arrange
            var user = User.Create("jose@gmail.com", "123456", "jose", "antonio");

            //Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => user.Update("joseaaaaaaaaaaaaaaaaaaaaaaaaaaa@gmail.com", "123456", "jose", "antonio"));
        }

        [Fact]
        public void Empty_FirstName_Should_Be_Throw_BusinessRuleValidationException()
        {
            // Arrange
            var user = User.Create("jose@gmail.com", "123456", "jose", "antonio");
            
            //Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => user.Update("jose@gmail.com", "123456", "", "antonio"));
        }

        [Fact]
        public void Null_FirstName_Should_Be_Throw_BusinessRuleValidationException()
        {
            // Arrange
            var user = User.Create("jose@gmail.com", "123456", "jose", "antonio");
            
            //Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => user.Update("jose@gmail.com", "123456", null, "antonio"));
        }        

        [Fact]
        public void FirstName_Length_Greater_Than_20_Should_Be_Throw_BusinessRuleValidationException()
        {   
            //Arrange
            var user = User.Create("jose@gmail.com", "123456", "jose", "antonio");

            //Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => user.Update("jose@gmail.com", "123456", "joseeeeeeeeeeeeeeeeee", "antonio"));
        }

        [Fact]
        public void Empty_LastName_Should_Be_Throw_BusinessRuleValidationException()
        {
            // Arrange
            var user = User.Create("jose@gmail.com", "123456", "jose", "antonio");

            //Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => user.Update("jose@gmail.com", "123456", "jose", ""));
        }

        [Fact]
        public void Null_LastName_Should_Be_Throw_BusinessRuleValidationException()
        {
            // Arrange
            var user = User.Create("jose@gmail.com", "123456", "jose", "antonio");

            //Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => user.Update("jose@gmail.com", "123456", "jose", null));
        }        

        [Fact]
        public void LastName_Length_Greater_Than_20_Should_Be_Throw_BusinessRuleValidationException()
        {   
            //Arrange
            var user = User.Create("jose@gmail.com", "123456", "jose", "antonio");

            //Act & Assert
            Assert.Throws<BusinessRuleValidationException>(() => user.Update("jose@gmail.com", "123456", "jose", "antonioooooooooooooooo"));
        }

    }
}