using System;
using Microsoft.EntityFrameworkCore;
public class MessageRepository : IMessageRepository 
{
    public DbContext Context { get; }

    public MessageRepository(DbContext _context) 
    {
        Context = _context;
    }

    

    
}