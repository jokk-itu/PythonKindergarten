using System.Collections.Generic;
using System.Threading.Tasks;
using MiniTwitApi.Server.Entities;
using MiniTwitApi.Server.Repositories;
using MiniTwitApi.Shared.Models;
using Xunit;

namespace MiniTwitApi.Tests.Repositories
{
    public class UserRepositoryTests : DbTest
    {
        private readonly FollowerRepository _repository;
        
        public UserRepositoryTests()
        {
            _repository = new FollowerRepository(_context);
        }

        private async Task Prepare()
        {
            //create users
            await _context.SaveChangesAsync();
        }
    }
} 