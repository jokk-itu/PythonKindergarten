using System;
using Microsoft.EntityFrameworkCore;
public class UserRepository : IUserRepository 
{
    public DbContext Context { get; }

    public UserRepository(DbContext _context) 
    {
        Context = _context;
    }

    public Task<bool> UserExistsAsync(string username) 
    {
        if(username is null || username == "")
            throw new ArgumentException($"Username must be a valid username not: {username}");
        s
        var userExists = await _context.FindAsync(username);
    }

    /**
    /* May not be needed
    */
    public Task<bool> UserExistsAsync(string userid) 
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(UserDTO user) 
    {
        var _user = new User() 
        {
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email,
            PwHash = user.Password,
        };

        var id = await _context.AddAsync(_user);
        await _context.SaveChangesAsync();
    }

    public Task<UserDTO> ReadAsync(int userid) 
    {
        var user = from u in context.Users where u.Id = userid
                    select new UserDTO
                    {
                        UserId = u.Id,
                        Username = u.Username,
                        Email = u.Email,
                        PwHash = u.PwHash
                    }
    }

    public Task<UserDTO> ReadAsync(string username) 
    {
        
    }

    
}