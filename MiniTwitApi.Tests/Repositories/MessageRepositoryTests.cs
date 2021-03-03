using System.Collections.Generic;
using System.Threading.Tasks;
using MiniTwitApi.Server.Entities;
using MiniTwitApi.Server.Repositories;
using MiniTwitApi.Shared.Models;
using Xunit;

namespace MiniTwitApi.Tests.Repositories
{
    public class MessageRepositoryTests : DbTest
    {
        private readonly FollowerRepository _repository;
        
        public MessageRepositoryTests()
        {
            _repository = new FollowerRepository(_context);
        }

        private async Task Prepare()
        {
            //Create users and users
            await _context.SaveChangesAsync();
        }
    }
} 