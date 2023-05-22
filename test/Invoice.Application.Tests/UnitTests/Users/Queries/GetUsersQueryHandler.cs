using AutoMapper;
using Invoice.Application.AutoMapper;
using Invoice.Application.CQRS.Users.Queries;
using Invoice.Application.Common.Dto;
using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Domain.Entities;
using Invoice.Domain.ValueObjects;
using Moq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using System;
using Xunit;

namespace Invoice.Application.Tests.UnitTests
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
                new User(new EmailAddress("jose@gmail.com"), "12345678", "jose", "antonio"),
                new User(new EmailAddress("maria@gmail.com"), "12345678", "maria", "antonieta"),
                new User(new EmailAddress("alfonso@gmail.com"), "12345678", "alfonso", "garcia"),
            };
        }

    }
}