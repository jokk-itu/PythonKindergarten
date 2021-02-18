using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniTwitApi.Client.Pages;
using MiniTwitApi.Shared.Models;
using MyApp.Entities;
using System.Linq;

public class UserRepository : IUserRepository 
{
    public Context Context { get; }

    public UserRepository(Context context) 
    {
        Context = context;
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        if (username is null || username == "")
            throw new ArgumentException($"Username must be a valid username not: {username}");
        var userExists = from u in Context.Users 
                                      where u.Username.Equals(username) 
                                      select u;
        return userExists.Any();
    }

    /**
    /* May not be needed
    */
    public async Task<bool> UserExistsAsync(int userid) 
    {
        throw new NotImplementedException();
    }

    public async Task CreateAsync(UserDTO user) 
    {
        

        var id = await Context.AddAsync(
            new User() 
        {
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email,
            PwHash = user.Password,
        });
        await Context.SaveChangesAsync();
    }

    public async Task<UserDTO> ReadAsync(int userid)
    {
        var user = await Context.Users.FindAsync(userid);
    
        return new UserDTO()
        {
            Id = user.UserId,
            Email = user.Email,
            Password = user.PwHash,
            Username = user.Username
        };
    }
    /**
     * Is it really needed?
     */
    public async Task<UserDTO> ReadAsync(string username)
    {
        throw new NotImplementedException();
    }

    
}