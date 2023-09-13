using AutoMapper;
using SimplexInvoice.Application.AutoMapper;
using SimplexInvoice.Application.Users.Queries;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.ValueObjects;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using SimplexInvoice.Domain.Users;

namespace SimplexInvoice.Application.Tests.UnitTests
{
    public class GetLastUsersQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ICustomLogger> _mockLogger;

        public GetLastUsersQueryHandlerTests()
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
        public async Task GetUsersList_Should_Be_3()
        {
            // Arrange
            var users = await GetUsers();
            int count = 3;
            _mockUserRepository.Setup(x => x.GetLastUsers(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(users.OrderByDescending(x => x.CreationDate)                                                                                                          
                               .Take(3)                                                                                                          
                               .ToList());
            var getLastUsersQueryHandler = new GetLastUsersQueryHandler(_mockUserRepository.Object, _mapper, _mockLogger.Object);

            //Act
            List<UserDto> usersDto = await getLastUsersQueryHandler.Handle(new GetLastUsersQuery(count), new CancellationToken());

            //Assert
            Assert.Equal(3, usersDto.Count);
        }

        private async Task<List<User>> GetUsers()
        {
            var users = new List<User>();
            users.Add(User.Create("jose@gmail.com", "12345678", "jose", "antonio"));
            await Task.Delay(1000);
            users.Add(User.Create("maria@gmail.com", "12345678", "maria", "antonieta"));
            await Task.Delay(1000);
            users.Add(User.Create("alfonso@gmail.com", "12345678", "alfonso", "garcia"));
            await Task.Delay(1000);
            users.Add(User.Create("ramiro@gmail.com", "12345678", "ramiro", "quake"));
            await Task.Delay(1000);
            users.Add(User.Create("carlos@gmail.com", "12345678", "carlos", "perez"));

            return users;
        }

    }
}