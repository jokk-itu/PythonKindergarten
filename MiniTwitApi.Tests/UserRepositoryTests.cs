using System.Collections.Generic;
using System.Threading.Tasks;
using MiniTwitApi.Server.Entities;
using MiniTwitApi.Server.Repositories;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;
using Xunit;

namespace MiniTwitApi.Tests
{
    public class UserRepositoryTests : DbTest
    {
        private readonly UserRepository _repository;
        
        public UserRepositoryTests()
        {
            _repository = new UserRepository(_context);
        }

        [Fact]
        public async Task Test_If_User_Exist_By_Username_True()
        {
    
            var actual = await _repository.UserExistsAsync("TestUser1");

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public async Task Test_If_User_Exist_By_Username_False()
        {
    
            var actual = await _repository.UserExistsAsync("Nonexistant");

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task Test_If_User_Exist_By_Userid_True()
        {
    
            var actual = await _repository.UserExistsAsync(1);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public async Task Test_If_User_Exist_By_Userid_False()
        {
    
            var actual = await _repository.UserExistsAsync(10000000);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task Test_If_User_Can_Be_Created_And_Can_Be_Found()
        {
            var testUser = new CreateUserDTO{Username = "TestUser2", Password = "TestUser", Email="test@tester.test"};

            await _repository.CreateAsync(testUser);
    
            var actual = await _repository.UserExistsAsync("TestUser2");

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public async Task Test_If_Read_Returns_Null_When_False_By_Userid()
        {
    
            var actual = await _repository.ReadAsync(10000);

            UserDTO expected = null;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Test_If_Read_Returns_Null_When_False_By_Username()
        {
    
            var actual = await _repository.ReadAsync("Non Existant Test User");

            UserDTO expected = null;

            // Assert
            Assert.Equal(expected, actual);
        }
        
    }
} 