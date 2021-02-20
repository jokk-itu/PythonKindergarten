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
        private Context _context { get; }

        public MessageRepository(Context context) 
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

            var id = await _context.AddAsync(m);
            await _context.SaveChangesAsync();
        }

        /**
        /* Add support for limit
        */
        public async Task<ICollection<MessageDTO>> ReadAllAsync(int limit = 20)
        {
            return await (from m in _context.Messages
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


        /**
        /* Add support for limit
        */
        public async Task<ICollection<MessageDTO>> ReadAllAsync(string username, int limit = 20)
        {
            var userRepository = new UserRepository(_context);
            var userid = userRepository.ReadAsync(username).Id;

            return await (from m in _context.Messages
                where m.AuthorId == userid
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