using AutoMapper;
using Invoice.Application.AutoMapper;
using Invoice.Application.CQRS.Authentication.Commands;
using Invoice.Application.Common.Dto;
using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Application.Interfaces;
using Invoice.Domain.Entities;
using Invoice.Domain.Exceptions;
using Invoice.Domain.ValueObjects;
using Moq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using System;
using Xunit;

namespace Invoice.Application.Tests.UnitTests
{
    public class AuthenticationRegisterHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IValidatorService> _mockValidatorService;
        private readonly Mock<IPasswordService> _mockPasswordService;
        private readonly Mock<ICustomLogger> _mockLogger;
        
        public AuthenticationRegisterHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new EntityDtoMappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockValidatorService = new Mock<IValidatorService>();
            _mockPasswordService = new Mock<IPasswordService>();
            _mockLogger = new Mock<ICustomLogger>();
        }

        [Fact]
        public async Task AuthenticationRegisterCommand_Should_Not_Be_Null()
        {
            // Arrange
            var user = GetUser();
            var authenticationRegisterCommand = GetAuthenticationRegisterCommand();
            _mockUserRepository.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(default(User));
            _mockPasswordService.Setup(x => x.GeneratePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns("12345678");
            var authenticationRegisterHandler = new AuthenticationRegisterHandler(_mockUserRepository.Object, 
                                                                                  _mockUnitOfWork.Object, 
                                                                                  _mockValidatorService.Object, 
                                                                                  _mockPasswordService.Object,
                                                                                  _mapper,
                                                                                  _mockLogger.Object);

            //Act
            UserDto userDto = await authenticationRegisterHandler.Handle(authenticationRegisterCommand, new CancellationToken());

            //Assert
            Assert.NotNull(userDto);
        }

        [Fact]
        public async Task AuthenticationRegisterCommand_Should_Throw_An_EntityValidationException()
        {
            // Arrange
            var user = GetUser();
            var authenticationRegisterCommand = GetAuthenticationRegisterCommand();
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(user);
            var AuthenticationRegisterHandler = new AuthenticationRegisterHandler(_mockUserRepository.Object, 
                                                                                  _mockUnitOfWork.Object, 
                                                                                  _mockValidatorService.Object, 
                                                                                  _mockPasswordService.Object,
                                                                                  _mapper,
                                                                                  _mockLogger.Object);

            //Act & Assert
            await Assert.ThrowsAsync<EntityValidationException>(async () => await AuthenticationRegisterHandler.Handle(authenticationRegisterCommand, new CancellationToken()));
        }

        private User GetUser()
        {
            return new User(new EmailAddress("jose@gmail.com"), "12345678", "jose", "antonio");
        }

        private AuthenticationRegisterCommand GetAuthenticationRegisterCommand()
        {
            return new AuthenticationRegisterCommand("jose@gmail.com", "12345678", "jose", "antonio");
        }

    }
}