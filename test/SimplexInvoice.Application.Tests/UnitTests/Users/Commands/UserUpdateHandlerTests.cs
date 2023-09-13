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
    public class UserUpdateHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPasswordService> _mockPasswordService;
        private readonly Mock<ICustomLogger> _mockLogger;

        public UserUpdateHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new EntityDtoMappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPasswordService = new Mock<IPasswordService>();            
            _mockLogger = new Mock<ICustomLogger>();
        }

        [Fact]
        public async Task UserUpdateCommand_Should_Not_Be_Null()
        {
            // Arrange
            var user = GetUser();
            var userUpdateCommand = GetUserUpdateCommand();
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _mockPasswordService.Setup(x => x.GeneratePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns("12345678");
            var userUpdateHandler = new UserUpdateHandler(_mockUserRepository.Object, 
                                                          _mockPasswordService.Object,
                                                          _mapper,
                                                          _mockLogger.Object);

            //Act
            UserDto userDto = await userUpdateHandler.Handle(userUpdateCommand, new CancellationToken());

            //Assert
            Assert.NotNull(userDto);
        }

        [Fact]
        public async Task UserUpdateCommand_Should_Throw_NotFoundException()
        {
            // Arrange
            User user = null;
            var userUpdateCommand = GetUserUpdateCommand();
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(user);
            var userUpdateHandler = new UserUpdateHandler(_mockUserRepository.Object, 
                                                          _mockPasswordService.Object,
                                                          _mapper,
                                                          _mockLogger.Object);

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await userUpdateHandler.Handle(userUpdateCommand, new CancellationToken()));
        }

        [Fact]
        public async Task UserUpdateCommand_Should_Throw_UserUpdatingException()
        {
            // Arrange
            var user = GetUser();
            var userUpdateCommand = GetUserUpdateCommand();
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
            _mockPasswordService.Setup(x => x.GeneratePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns("12345678");
            var userUpdateHandler = new UserUpdateHandler(_mockUserRepository.Object,
                                                          _mockPasswordService.Object,
                                                          _mapper,
                                                          _mockLogger.Object);

            //Act & Assert
            await Assert.ThrowsAsync<UserUpdatingException>(async () =>
                await userUpdateHandler.Handle(userUpdateCommand, new CancellationToken()));
        }

        private User GetUser()
        {
            return User.Create("jose@gmail.com", "12345678", "jose", "antonio");
        }

        private UserUpdateCommand GetUserUpdateCommand()
        {
            return new UserUpdateCommand(new Guid("a0769b81-2e5d-4e75-9cfd-e92c38fd70ca"), "jose@gmail.com", "12345678", "jose", "antonio");            
        }

    }
}