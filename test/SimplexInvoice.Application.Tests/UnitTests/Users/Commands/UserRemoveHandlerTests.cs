using AutoMapper;
using SimplexInvoice.Application.AutoMapper;
using SimplexInvoice.Application.Users.Commands;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Domain.ValueObjects;
using Moq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using System;
using Xunit;
using SimplexInvoice.Domain.Users;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Application.Users.Exceptions;

namespace SimplexInvoice.Application.Tests.UnitTests
{
    public class UserRemoveHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IValidatorService> _mockValidatorService;
        private readonly Mock<IPasswordService> _mockPasswordService;
        private readonly Mock<ICustomLogger> _mockLogger;

        public UserRemoveHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new EntityDtoMappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockValidatorService = new Mock<IValidatorService>();
            _mockPasswordService = new Mock<IPasswordService>();  
            _mockLogger = new Mock<ICustomLogger>();          
        }

        [Fact]
        public async Task UserRemoveCommand_Should_Not_Be_Null()
        {
            // Arrange
            var user = GetUser();
            var userRemoveCommand = GetUserRemoveCommand();
            _mockUserRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));
            _mockUserRepository.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var userRemoveHandler = new UserRemoveHandler(_mockUserRepository.Object, 
                                                          _mockLogger.Object);

            //Act
            bool result = await userRemoveHandler.Handle(userRemoveCommand, new CancellationToken());

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UserRemoveCommand_Should_Throw_UserRemovingException()
        {
            // Arrange
            var user = GetUser();
            var userRemoveCommand = GetUserRemoveCommand();
            _mockUserRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));
            _mockUserRepository.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
            var userRemoveHandler = new UserRemoveHandler(_mockUserRepository.Object,
                                                          _mockLogger.Object);

            //Act & Assert
            await Assert.ThrowsAsync<UserRemovingException>(async () =>
                await userRemoveHandler.Handle(userRemoveCommand, new CancellationToken()));
        }

        private User GetUser()
        {
            return User.Create("jose@gmail.com", "12345678", "jose", "antonio");
        }

        private UserRemoveCommand GetUserRemoveCommand()
        {
            return new UserRemoveCommand(new Guid("a0769b81-2e5d-4e75-9cfd-e92c38fd70ca"));
        }

    }
}