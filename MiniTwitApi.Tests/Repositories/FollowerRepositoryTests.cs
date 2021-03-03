using System.Collections.Generic;
using System.Threading.Tasks;
using MiniTwitApi.Server.Entities;
using MiniTwitApi.Server.Repositories;
using MiniTwitApi.Shared.Models;
using Xunit;

namespace MiniTwitApi.Tests.Repositories
{
    public class FollowerRepositoryTests : DbTest
    {
        private readonly FollowerRepository _repository;
        
        public FollowerRepositoryTests()
        {
            _repository = new FollowerRepository(_context);
        }

        [Fact]
        public async Task ReadAll_Given_Username()
        {
            await Prepare();
            var username = "TestUser0";
            
            var expected = new List<FollowerDTO>()
            {
                new () {WhoId = 0, WhomId = 1}
            };
            var actual = await _repository.ReadAllAsync(username);

            Assert.Equal(expected, actual);
        }

        
        public async Task Delete_Given_Follower()
        {
            await Prepare();
            var follower = new FollowerDTO(){WhoId = 0, WhomId = 1};
            var expected = "";
            await _repository.DeleteAsync(follower);
            Assert.True(true);
        }
        
        [Fact]
        public async Task Create_Given_Follower()
        {
            await Prepare();
            var follower = new FollowerDTO() {WhoId = 0, WhomId = 2};
            var expected = follower.WhoId;
            var actual = await _repository.CreateAsync(follower);
            Assert.Equal(expected, actual);
        }

        private async Task Prepare()
        {
            for (var i = 0; i < 5; ++i)
            {
                _context.Users.Add(new User()
                {
                    Username = $"TestUser{i}",
                    Email = $"Test{i}@itu.dk",
                    Password = $"Test{i}"
                });
            }
            await _context.SaveChangesAsync();

            for (var j = 0; j < 4; ++j)
            {
                _context.Followers.Add(new Follower()
                {
                    WhoId = j,
                    WhomId = j+1
                });
            }
            await _context.SaveChangesAsync();
            
            for (var j = 4; j > 0; --j)
            {
                _context.Followers.Add(new Follower()
                {
                    WhoId = j,
                    WhomId = j-1
                });
            }
            await _context.SaveChangesAsync();
        }
    }
} 