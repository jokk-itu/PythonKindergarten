using System;
using Microsoft.EntityFrameworkCore;


public class MessageRepository : IMessageRepository 
{
    public DbContext Context { get; }

    public MessageRepository(DbContext _context) 
    {
        Context = _context;
    }

    public Task CreateAsync(MessageDTO message) 
    {
        var message = new Message() 
        {
            authorId = message.Author,
            authorUsername = message.AuthorUsername,
            text = message.Text,
            pubDate = message.PublishDate,
            flagged = message.Flagged
        };

        var id = await _context.AddAsync(message);
        await _context.SaveChangesAsync();
    }

    /**
    /* Add support for limit
    */
    public Task<ICollection<MessageDTO>> ReadAllAsync(int limit = 20) =>
        return (from m in await _context.FindAllAsync() 
                select new MessageDTO() 
                {
                    Id = m.messageid,
                    Author = m.authorId,
                    AuthorUsername = m.authorUsername,
                    Text = m.text,
                    PublishDate = m.pubDate,
                    Flagged = m.flagged
                }).ToListAsync(); 

    /**
    /* Add support for limit
    */
    public Task<ICollection<MessageDTO>> ReadAllAsync(int userid, int limit = 20) =>
        return (from m in await _context.FindAllAsync()
                where m.authorId == userid
                select new MessageDTO() 
                {
                    Id = m.messageid,
                    Author = m.authorId,
                    AuthorUsername = m.authorUsername,
                    Text = m.text,
                    PublishDate = m.pubDate,
                    Flagged = m.flagged
                }).ToListAsync();
    
}