using System;
using Microsoft.EntityFrameworkCore;
public class UserRepository : IUserRepository 
{
    public DbContext Context { get; }

    public UserRepository(DbContext _context) 
    {
        Context = _context;
    }

    

    
}