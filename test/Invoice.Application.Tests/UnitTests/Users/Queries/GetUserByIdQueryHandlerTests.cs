using AutoMapper;
using Invoice.Application.AutoMapper;
using Invoice.Application.Users.Queries;
using Invoice.Application.Common.Dto;
using Invoice.Application.Common.Interfaces.Persistance;
using Moq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using System;
using Xunit;
using Invoice.Domain.Users;

namespace Invoice.Application.Tests.UnitTests
{
    public class GetUserByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ICustomLogger> _mockLogger;

        public GetUserByIdQueryHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new EntityDtoMappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockLogger = new Mock<ICustomLogger>();
        }

        [Fact]
        public async Task GetUserById_Should_Be_Found()
        {
            // Arrange
            var user = GetUser();
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(user);
            var getUsersQueryHandler = new GetUserByIdQueryHandler(_mockUserRepository.Object, _mapper, _mockLogger.Object);

            //Act
            UserDto userDto = await getUsersQueryHandler.Handle(new GetUserByIdQuery(user.Id), new CancellationToken());

            //Assert
            Assert.NotNull(userDto);
        }

        [Fact]
        public async Task GetUserById_Should_Not_Be_Found()
        {
            // Arrange
            var user = GetUser();
            _mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).ReturnsAsync(default(User));
            var getUsersQueryHandler = new GetUserByIdQueryHandler(_mockUserRepository.Object, _mapper, _mockLogger.Object);

            //Act & Assert
            await Assert.ThrowsAsync<System.NullReferenceException>(() => 
            getUsersQueryHandler.Handle(new GetUserByIdQuery(user.Id), new CancellationToken()));
        }

        private User GetUser()
        {
            return _mapper.Map<User> (GetUserDto());
        }

        private UserDto GetUserDto()
        {
            return new UserDto
            {
                Id = new Guid("a0769b81-2e5d-4e75-9cfd-e92c38fd70ca"),
                Email = "jose@gmail.com",
                Password = "12345678",
                FirstName = "jose", 
                LastName = "antonio"
            };
        }

    }
}