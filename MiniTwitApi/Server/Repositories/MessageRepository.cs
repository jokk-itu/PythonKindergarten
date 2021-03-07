using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniTwitApi.Server.Entities;
using MiniTwitApi.Server.Repositories.Abstract;
using MiniTwitApi.Shared.Models;

namespace MiniTwitApi.Server.Repositories
{
    public class MessageRepository : IMessageRepository 
    {
        private IContext _context { get; }

        public MessageRepository(IContext context) 
        {
            _context = context;
        }

        public async Task CreateAsync(MessageDTO message) 
        {
            var m = new Message() 
            {
                AuthorId = message.Author,
                AuthorUsername = message.AuthorUsername,
                Text = message.Text,
                PubDate = message.PublishDate,
                Flagged = message.Flagged
            };

            var id = await _context.Messages.AddAsync(m);
            await _context.SaveChangesAsync();
        }

        /**
        /* Add support for limit
        /* Add support for Skip(). Use before Take()
        */
        public async Task<ICollection<MessageDTO>> ReadAllAsync(int limit = 20)
        {
            return await (
                 _context.Messages
                .OrderByDescending(m => m.PubDate)
                .Take(limit)).Select(m => new MessageDTO()
                {
                    Id = m.MessageId,
                    Author = m.AuthorId,
                    AuthorUsername = m.AuthorUsername,
                    Text = m.Text,
                    PublishDate = m.PubDate,
                    Flagged = m.Flagged
                }).ToListAsync();
        }


        /**
        /* Add support for limit
        /* Sometimes works, sometimes doesn't. wth.
        */
        public async Task<ICollection<MessageDTO>> ReadAllUserAsync(string username, int limit = 20)
        {
            Console.WriteLine($"This is the username: {username}");
            var userRepository = new UserRepository(_context);
            var user = await userRepository.ReadAsync(username);
            if(user is null) 
                throw new ArgumentException("Provided username does not exist");

            return await (from m in _context.Messages
                where m.AuthorId == user.Id
                select new MessageDTO()
                {
                    Id = m.MessageId,
                    Author = m.AuthorId,
                    AuthorUsername = m.AuthorUsername,
                    Text = m.Text,
                    PublishDate = m.PubDate,
                    Flagged = m.Flagged
                }).ToListAsync();
        }
    }
}