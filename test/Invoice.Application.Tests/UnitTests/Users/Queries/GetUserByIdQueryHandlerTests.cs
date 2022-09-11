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
    public class GetUserByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;
        public GetUserByIdQueryHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new EntityDtoMappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetUserById_Should_Be_Found()
        {
            // Arrange
            var user = GetUser();
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), true, $"Id=={user.Id}")).ReturnsAsync(user);
            var getUsersQueryHandler = new GetUserByIdQueryHandler(mockUserRepository.Object, _mapper);

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
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), true, $"Id=={user.Id}")).ReturnsAsync(default(User));
            var getUsersQueryHandler = new GetUserByIdQueryHandler(mockUserRepository.Object, _mapper);

            //Act
            UserDto userDto = await getUsersQueryHandler.Handle(new GetUserByIdQuery(user.Id), new CancellationToken());

            //Assert
            Assert.Null(userDto);
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