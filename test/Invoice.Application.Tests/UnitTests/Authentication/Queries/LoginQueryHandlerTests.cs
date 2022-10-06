using AutoMapper;
using Invoice.Application.AutoMapper;
using Invoice.Application.CQRS.Authentication.Queries;
using Invoice.Application.Common.Dto;
using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Application.Interfaces;
using Invoice.Domain.Entities;
using Moq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using System;
using Xunit;

namespace Invoice.Application.Tests.UnitTests
{
    public class LoginQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPasswordService> _mockPasswordService;
        private readonly Mock<ICustomLogger> _mockLogger;
        public LoginQueryHandlerTests()
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
        public async Task Login_Should_Be_Found()
        {
            // Arrange
            var user = GetUser();
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(user);
            _mockPasswordService.Setup(x => x.IsCorrectPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var getUsersQueryHandler = new LoginQueryHandler(_mockUserRepository.Object, 
                                                             _mockPasswordService.Object, 
                                                             _mapper, 
                                                             _mockLogger.Object);

            //Act
            UserDto userDto = await getUsersQueryHandler.Handle(new LoginQuery(user.EmailAddress.ToString(), user.Password), new CancellationToken());

            //Assert
            Assert.NotNull(userDto);
        }

        [Fact]
        public async Task Login_Should_Not_Be_Found()
        {
            // Arrange
            var user = GetUser();
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(default(User));
            _mockPasswordService.Setup(x => x.IsCorrectPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var getUsersQueryHandler = new LoginQueryHandler(_mockUserRepository.Object, 
                                                             _mockPasswordService.Object, 
                                                             _mapper, 
                                                             _mockLogger.Object);

            //Act
            UserDto userDto = await getUsersQueryHandler.Handle(new LoginQuery(user.EmailAddress.ToString(), user.Password), new CancellationToken());

            //Assert
            Assert.Null(userDto);
        }

        [Fact]
        public async Task Login_Password_Should_Not_Be_Correct()
        {
            // Arrange
            var user = GetUser();
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(user);
            _mockPasswordService.Setup(x => x.IsCorrectPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var getUsersQueryHandler = new LoginQueryHandler(_mockUserRepository.Object, 
                                                             _mockPasswordService.Object, 
                                                             _mapper, 
                                                             _mockLogger.Object);

            //Act
            UserDto userDto = await getUsersQueryHandler.Handle(new LoginQuery(user.EmailAddress.ToString(), user.Password), new CancellationToken());

            //Assert
            Assert.Null(userDto);
        }

        private User GetUser()
        {
            var userDto = new UserDto
            {
                Id = new Guid("a0769b81-2e5d-4e75-9cfd-e92c38fd70ca"),
                Email = "jose@gmail.com",
                Password = "12345678",
                FirstName = "jose", 
                LastName = "antonio"
            };
            return _mapper.Map<User> (userDto);
        }

    }
}