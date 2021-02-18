using System;
using Microsoft.EntityFrameworkCore;

public class FollowerRepository : IFollowerRepository
{
    public DbContext Context { get; }

    public FollowerRepository(DbContext _context) 
    {
        Context = _context;
    }



    
}