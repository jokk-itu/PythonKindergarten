using System.Collections.Generic;
using System.Threading.Tasks;
using MiniTwitApi.Server.Entities;
using MiniTwitApi.Server.Repositories;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;
using Xunit;

namespace MiniTwitApi.Tests.Repositories
{
    public class FollowerRepositoryTests : DbTest
    {
        private readonly FollowerRepository _followerRepository;
        private readonly UserRepository _userRepository;
        
        public FollowerRepositoryTests()
        {
            _followerRepository = new FollowerRepository(_context);
            _userRepository = new UserRepository(_context);
        }

        public async Task ReadAll_Given_Username()
        {
            await Prepare();
            var username = "TestUser0";
            
            var expected = new List<FollowerDTO>()
            {
                new () {WhoId = 0, WhomId = 1}
            };
            var actual = await _followerRepository.ReadAllAsync(username);

            Assert.Equal(expected, actual);
        }

        public async Task Delete_Given_Follower()
        {
            await Prepare();
            var follower = new FollowerDTO() {WhoId = 0, WhomId = 1};
            var expected = follower.WhoId;
            var actual = await _followerRepository.DeleteAsync(follower);
            Assert.Equal(expected, actual);
        }

        public async Task Create_Given_Follower()
        {
            await Prepare();
            var follower = new FollowerDTO() {WhoId = 0, WhomId = 2};
            var expected = follower.WhoId;
            var actual = await _followerRepository.CreateAsync(follower);
            Assert.Equal(expected, actual);
        }

        private async Task Prepare()
        {
            for (var i = 1; i < 5; ++i)
            {
                await _userRepository.CreateAsync(new CreateUserDTO()
                {
                    Username = $"TestUserFollow{i}",
                    Email = $"Test{i}@itu.dk",
                    Password = $"Test{i}"
                });
            }
            for (var j = 1; j < 4; ++j)
            {
                await _followerRepository.CreateAsync(new FollowerDTO()
                {
                    WhoId = j,
                    WhomId = j+1
                });
            }
            await _context.SaveChangesAsync();
        }
    }
} 