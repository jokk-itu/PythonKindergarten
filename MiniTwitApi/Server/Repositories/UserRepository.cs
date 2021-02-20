using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniTwitApi.Server.Entities;
using MiniTwitApi.Server.Repositories.Abstract;
using MiniTwitApi.Shared.Models;

namespace MiniTwitApi.Server.Repositories
{
    public class UserRepository : IUserRepository 
    {
        private IContext _context { get; }

        public UserRepository(IContext context) 
        {
            _context = context;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            if (username is null || username == "")
                throw new ArgumentException($"Username must be a valid username not: {username}");
            var userExists = from u in _context.Users 
                where u.Username.Equals(username) 
                select u;
            return userExists.Any();
        }
        
        public async Task<bool> UserExistsAsync(int userid)
        {
            var user = await _context.Users.FindAsync(userid);
            return user is not null;
        }

        public async Task CreateAsync(UserDTO user) 
        {
            var id = await _context.Users.AddAsync(
                new User() 
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                });
            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> ReadAsync(int userid)
        {
            var user = await _context.Users.FindAsync(userid);
    
            return new UserDTO()
            {
                Id = user.UserId,
                Email = user.Email,
                Password = user.Password,
                Username = user.Username
            };
        }

        /**
        * Is it really needed?
        */
        public async Task<UserDTO> ReadAsync(string username)
        {
            var user = from u in _context.Users
                where u.Username.Equals(username)
                select new UserDTO()
                {
                    Id = u.UserId,
                    Email = u.Email,
                    Password = u.Password,
                    Username = u.Username
                };
            return await user.FirstAsync();
        }


    }
}