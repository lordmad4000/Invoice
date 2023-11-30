using AutoMapper;
using SimplexInvoice.Application.Authentication.Commands;
using SimplexInvoice.Application.AutoMapper;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Domain.Exceptions;
using SimplexInvoice.Domain.Users;
using Moq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using System;
using Xunit;

namespace SimplexInvoice.Application.Tests.UnitTests
{
    public class AuthenticationRegisterHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPasswordService> _mockPasswordService;
        private readonly Mock<ICustomLogger> _mockLogger;
        
        public AuthenticationRegisterHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new EntityToDtoMappingProfile());
            });
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mapper = mapperConfig.CreateMapper();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPasswordService = new Mock<IPasswordService>();
            _mockLogger = new Mock<ICustomLogger>();
        }

        [Fact]
        public async Task AuthenticationRegisterCommand_Should_Not_Be_Null()
        {
            // Arrange
            var user = GetUser();
            var authenticationRegisterCommand = GetAuthenticationRegisterCommand();
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _mockUserRepository.Setup(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<string[]>())).ReturnsAsync(default(User));
            _mockPasswordService.Setup(x => x.GeneratePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns("12345678");
            var authenticationRegisterHandler = new AuthenticationRegisterHandler(_mockUnitOfWork.Object,
                                                                                  _mockUserRepository.Object, 
                                                                                  _mockPasswordService.Object,
                                                                                  _mapper,
                                                                                  _mockLogger.Object);

            //Act
            UserDto userDto = await authenticationRegisterHandler.Handle(authenticationRegisterCommand, new CancellationToken());

            //Assert
            Assert.NotNull(userDto);
        }

        [Fact]
        public async Task AuthenticationRegisterCommand_Should_Throw_An_BusinessRuleValidationException()
        {
            // Arrange
            var user = GetUser();
            var authenticationRegisterCommand = GetAuthenticationRegisterCommand();
            _mockUnitOfWork.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>(), It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<string[]>())).ReturnsAsync(user);
            var AuthenticationRegisterHandler = new AuthenticationRegisterHandler(_mockUnitOfWork.Object, 
                                                                                  _mockUserRepository.Object, 
                                                                                  _mockPasswordService.Object,
                                                                                  _mapper,
                                                                                  _mockLogger.Object);

            //Act & Assert
            await Assert.ThrowsAsync<BusinessRuleValidationException>(async () => await AuthenticationRegisterHandler.Handle(authenticationRegisterCommand, new CancellationToken()));
        }

        private User GetUser()
        {
            return User.Create("jose@gmail.com", "12345678", "jose", "antonio");
        }

        private AuthenticationRegisterCommand GetAuthenticationRegisterCommand()
        {
            return new AuthenticationRegisterCommand("jose@gmail.com", "12345678", "jose", "antonio");
        }

    }
}