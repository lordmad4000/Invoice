using AutoMapper;
using Moq;
using SimplexInvoice.Application.AutoMapper;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Application.Users.Commands;
using SimplexInvoice.Application.Users.Exceptions;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Users;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SimplexInvoice.Application.Tests.UnitTests
{
    public class UserRegisterHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPasswordService> _mockPasswordService;
        private readonly Mock<ICustomLogger> _mockLogger;

        public UserRegisterHandlerTests()
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
        public async Task UserRegisterCommand_Should_Not_Be_Null()
        {
            // Arrange
            var user = GetUser();
            var userRegisterCommand = GetUserRegisterCommand();
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(default(User));
            _mockUserRepository.Setup(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _mockPasswordService.Setup(x => x.GeneratePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns("12345678");
            var userRegisterHandler = new UserRegisterHandler(_mockUserRepository.Object,
                                                              _mockPasswordService.Object,
                                                              _mapper,
                                                              _mockLogger.Object);

            //Act
            UserDto userDto = await userRegisterHandler.Handle(userRegisterCommand, new CancellationToken());

            //Assert
            Assert.NotNull(userDto);
        }

        [Fact]
        public async Task UserRegisterCommand_Should_Throw_An_BusinessRuleValidationException()
        {
            // Arrange
            var user = GetUser();
            var userRegisterCommand = GetUserRegisterCommand();
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(user);
            var userRegisterHandler = new UserRegisterHandler(_mockUserRepository.Object,
                                                              _mockPasswordService.Object,
                                                              _mapper,
                                                              _mockLogger.Object);

            //Act & Assert
            await Assert.ThrowsAsync<BusinessRuleValidationException>(async () => 
                await userRegisterHandler.Handle(userRegisterCommand, new CancellationToken()));
        }

        [Fact]
        public async Task UserRegisterCommand_Should_Throw_An_UserRegisteringException()
        {
            // Arrange
            var user = GetUser();
            var userRegisterCommand = GetUserRegisterCommand();
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(default(User));
            _mockUserRepository.Setup(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
            _mockPasswordService.Setup(x => x.GeneratePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns("12345678");
            var userRegisterHandler = new UserRegisterHandler(_mockUserRepository.Object,
                                                              _mockPasswordService.Object,
                                                              _mapper,
                                                              _mockLogger.Object);

            //Act & Assert
            await Assert.ThrowsAsync<UserRegisteringException>(async () =>
                await userRegisterHandler.Handle(userRegisterCommand, new CancellationToken()));
        }

        private User GetUser()
        {
            return User.Create("jose@gmail.com", "12345678", "jose", "antonio");
        }

        private UserRegisterCommand GetUserRegisterCommand()
        {
            return new UserRegisterCommand("jose@gmail.com", "12345678", "jose", "antonio");
        }

    }
}