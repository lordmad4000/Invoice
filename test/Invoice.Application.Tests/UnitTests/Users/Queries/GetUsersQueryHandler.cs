using AutoMapper;
using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Application.AutoMapper;
using Moq;
using Xunit;
using Invoice.Application.CQRS.Users.Queries;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Invoice.Domain.Entities;
using Invoice.Domain.ValueObjects;
using System.Linq.Expressions;
using System;
using Invoice.Application.Common.Dto;

namespace Invoice.Application.Tests.UnitTests.Users.Queries
{
    public class GetUsersQueryHandlerTests
    {
        private readonly IMapper _mapper;
        public GetUsersQueryHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new EntityDtoMappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetUsersList_Should_Be_3()
        {
            // Arrange
            var users = GetUsers();
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.ListAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(users);
            var getUsersQueryHandler = new GetUsersQueryHandler(mockUserRepository.Object, _mapper);

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