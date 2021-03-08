using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniTwitApi.Server.Entities;
using MiniTwitApi.Server.Repositories.Abstract;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;

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
        
        public async Task<ICollection<MessageDTO>> ReadAllAsync(int limit = 20, int skip = 0)
        {
            return await (
                 _context.Messages
                .OrderByDescending(m => m.PubDate)
                .Skip(skip)
                .Take(limit)
                .Select(m => new MessageDTO()
                {
                    Id = m.MessageId,
                    Author = m.AuthorId,
                    AuthorUsername = m.AuthorUsername,
                    HashedAuthorEmail = UserDTO.MD5Hash(m.User.Email),
                    Text = m.Text,
                    PublishDate = m.PubDate,
                    Flagged = m.Flagged
                })).ToListAsync();
        }
        
        public async Task<ICollection<MessageDTO>> ReadAllUserAsync(string username, int limit = 20, int skip = 0)
        {
            var userRepository = new UserRepository(_context);
            var user = await userRepository.ReadAsync(username);
            if(user is null) 
                throw new ArgumentException("Provided username does not exist");

            return await (
                _context.Messages
                .Where(m => m.AuthorUsername.Equals(username))
                .OrderByDescending(m => m.PubDate)
                .Skip(skip)
                .Take(limit)
                .Select(m => new MessageDTO()
                {
                    Id = m.MessageId,
                    Author = m.AuthorId,
                    AuthorUsername = m.AuthorUsername,
                    HashedAuthorEmail = UserDTO.MD5Hash(m.User.Email),
                    Text = m.Text,
                    PublishDate = m.PubDate,
                    Flagged = m.Flagged
                })).ToListAsync();
        }
    }
}