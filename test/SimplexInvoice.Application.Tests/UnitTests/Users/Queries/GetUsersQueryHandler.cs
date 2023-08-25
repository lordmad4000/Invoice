using AutoMapper;
using SimplexInvoice.Application.AutoMapper;
using SimplexInvoice.Application.Users.Queries;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.ValueObjects;
using Moq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using System;
using Xunit;
using SimplexInvoice.Domain.Users;

namespace SimplexInvoice.Application.Tests.UnitTests
{
    public class GetUsersQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ICustomLogger> _mockLogger;

        public GetUsersQueryHandlerTests()
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
            var users = GetUsers();
            _mockUserRepository.Setup(x => x.ListAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(users);
            var getUsersQueryHandler = new GetUsersQueryHandler(_mockUserRepository.Object, _mapper, _mockLogger.Object);

            //Act
            List<UserDto> usersDto = await getUsersQueryHandler.Handle(new GetUsersQuery(), new CancellationToken());

            //Assert
            Assert.Equal(3, usersDto.Count);
        }

        private List<User> GetUsers()
        {
            return new List<User>
            {
                User.Create("jose@gmail.com", "12345678", "jose", "antonio"),
                User.Create("maria@gmail.com", "12345678", "maria", "antonieta"),
                User.Create("alfonso@gmail.com", "12345678", "alfonso", "garcia"),
            };
        }

    }
}