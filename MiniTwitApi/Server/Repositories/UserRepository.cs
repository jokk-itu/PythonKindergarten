using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniTwitApi.Server.Entities;
using MiniTwitApi.Server.Repositories.Abstract;
using MiniTwitApi.Shared.Models.UserModels;

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
            return await ReadAsync(username) is not null;
        }
        
        public async Task<bool> UserExistsAsync(int userid)
        {
            return await ReadAsync(userid) is not null;
        }

        public async Task CreateAsync(CreateUserDTO user) 
        {
            var id = await _context.Users.AddAsync(
                new User() 
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                });
            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> ReadAsync(int userid)
        {
            var user = await _context.Users.FindAsync(userid);

            if(user is null)
                return null;
                
            return new UserDTO()
            {
                Id = user.UserId,
                Email = user.Email,
                Password = user.Password,
                Username = user.Username
            };
        }

        public async Task<UserDTO> ReadAsync(string username)
        {
            var user =  from u in _context.Users
                where u.Username.Equals(username)
                select new UserDTO()
                {
                    Id = u.UserId,
                    Email = u.Email,
                    Password = u.Password,
                    Username = u.Username
                };
            return user.Count() switch
            {
                0 => null,
                1 => await user.FirstAsync(),
                _ => throw new Exception("There exists multiple users with the same username. SHOULD NOT HAPPEN.")
            };
        }


    }
}