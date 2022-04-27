using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Users.Domain.Entities;
using Users.Domain.Interfaces;

namespace TestUsers.API.Mock
{
    public class UserRepositoryMock : IUserRepository
    {
        private static Dictionary<Guid, User> _userData = new Dictionary<Guid, User>();

        public UserRepositoryMock()
        {
            // var users = new User[5];

            // users[0] = new User("pepe", "12345678", "Pepe", "Luis", new EmailAddress("pepe@luis.com"));
            // users[1] = new User("ramiro", "12345678", "Ramiro", "Garcia", new EmailAddress("ramiro@garcia.com"));
            // users[2] = new User("federico", "12345678", "Fede", "Rico", new EmailAddress("fede@rico.com"));
            // users[3] = new User("santiago", "12345678", "Santi", "Hago", new EmailAddress("santi@hago.com"));
            // users[4] = new User("carlos", "12345678", "Carlos", "Ejea", new EmailAddress("carlos@ejea.com"));

            // _userData = new Dictionary<Guid, User>
            // {
            //     { users[0].Id, users[0] },
            //     { users[1].Id, users[1] },
            //     { users[2].Id, users[2] },
            //     { users[3].Id, users[3] },
            //     { users[4].Id, users[4] },

            // };
        }

        public async Task<User> AddAsync(User entity)
        {
            _userData.Add(entity.Id, entity);
            return await Task.FromResult(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = _userData.FirstOrDefault(c => c.Key == id);

            if (result.Value == null)
                return await Task.FromResult(false);

            _userData.Remove(id);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(User entity)
        {
            return await DeleteAsync(entity.Id);
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> expression, bool tracking = true)
        {
            var result = _userData.FirstOrDefault(kvp => expression.Compile()(kvp.Value));

            return await Task.FromResult(result.Value);
        }

        public async Task<List<User>> ListAsync(Expression<Func<User, bool>> expression)
        {
            var results = _userData.Where(kvp => expression.Compile()(kvp.Value));
            
            var users = results.Select(c => c.Value).ToList();

            return await Task.FromResult(users);
        }

        public async Task<User> UpdateAsync(User entity)
        {
            _userData[entity.Id] = entity;
            return await Task.FromResult(entity);
        }
    }
}