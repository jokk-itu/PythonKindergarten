using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniTwitApi.Shared.Models;
using MyApp.Entities;


public class MessageRepository : IMessageRepository 
{
    public Context Context { get; }

    public MessageRepository(Context context) 
    {
        Context = context;
    }

    public async Task CreateAsync(MessageDTO message) 
    {
        var m = new Message() 
        {
            authorId = message.Author,
            authorUsername = message.AuthorUsername,
            text = message.Text,
            pubDate = message.PublishDate,
            flagged = message.Flagged
        };

        var id = await Context.AddAsync(m);
        await Context.SaveChangesAsync();
    }

    /**
    /* Add support for limit
    */
    public async Task<ICollection<MessageDTO>> ReadAllAsync(int limit = 20) =>
        await (from m in Context.Messages
                select new MessageDTO() 
                {
                    Id = m.messageId,
                    Author = m.authorId,
                    AuthorUsername = m.authorUsername,
                    Text = m.text,
                    PublishDate = m.pubDate,
                    Flagged = m.flagged
                }).ToListAsync(); 

    
    /**
    /* Add support for limit
    */
    public async Task<ICollection<MessageDTO>> ReadAllAsync(int userid, int limit = 20) =>
        await (from m in Context.Messages
                where m.authorId == userid
                select new MessageDTO() 
                {
                    Id = m.messageId,
                    Author = m.authorId,
                    AuthorUsername = m.authorUsername,
                    Text = m.text,
                    PublishDate = m.pubDate,
                    Flagged = m.flagged
                }).ToListAsync();
    
}