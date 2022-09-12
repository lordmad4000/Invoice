using AutoMapper;
using Invoice.Application.AutoMapper;
using Invoice.Application.CQRS.Users.Queries;
using Invoice.Application.Common.Dto;
using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Domain.Entities;
using Invoice.Domain.ValueObjects;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Xunit;

namespace Invoice.Application.Tests.UnitTests.Users.Queries
{
    public class GetLastUsersQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockUserRepository;
        public GetLastUsersQueryHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new EntityDtoMappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();
            _mockUserRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task GetUsersList_Should_Be_3()
        {
            // Arrange
            var users = await GetUsers();
            int count = 3;
            _mockUserRepository.Setup(x => x.GetLastUsers(It.IsAny<int>())).ReturnsAsync(users.OrderByDescending(x => x.CreationDate)
                                                                                              .Take(3)
                                                                                              .ToList());
            var getLastUsersQueryHandler = new GetLastUsersQueryHandler(_mockUserRepository.Object, _mapper);

            //Act
            List<UserDto> usersDto = await getLastUsersQueryHandler.Handle(new GetLastUsersQuery(count), new CancellationToken());

            //Assert
            Assert.Equal(3, usersDto.Count);
        }

        private async Task<List<User>> GetUsers()
        {
            var users = new List<User>();
            users.Add(new User(new EmailAddress("jose@gmail.com"), "12345678", "jose", "antonio"));
            await Task.Delay(1000);
            users.Add(new User(new EmailAddress("maria@gmail.com"), "12345678", "maria", "antonieta"));
            await Task.Delay(1000);
            users.Add(new User(new EmailAddress("alfonso@gmail.com"), "12345678", "alfonso", "garcia"));
            await Task.Delay(1000);
            users.Add(new User(new EmailAddress("ramiro@gmail.com"), "12345678", "ramiro", "quake"));
            await Task.Delay(1000);
            users.Add(new User(new EmailAddress("carlos@gmail.com"), "12345678", "carlos", "perez"));

            return users;
        }

    }
}