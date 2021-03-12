using System.Collections.Generic;
using System.Threading.Tasks;
using MiniTwitApi.Server.Entities;
using MiniTwitApi.Server.Repositories;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Models.UserModels;
using Xunit;

namespace MiniTwitApi.Tests.Repositories
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
            Assert.Equal(true, actual);
        }

        [Fact]
        public async Task Test_If_User_Exist_By_Username_False()
        {
    
            var actual = await _repository.UserExistsAsync("Nonexistant");

            // Assert
            Assert.Equal(false, actual);
        }

        [Fact]
        public async Task Test_If_User_Exist_By_Userid_True()
        {
    
            var actual = await _repository.UserExistsAsync(1);

            // Assert
            Assert.Equal(true, actual);
        }

        [Fact]
        public async Task Test_If_User_Exist_By_Userid_False()
        {
    
            var actual = await _repository.UserExistsAsync(10000000);

            // Assert
            Assert.Equal(false, actual);
        }

        [Fact]
        public async Task Test_If_User_Can_Be_Created_And_Can_Be_Found()
        {
            CreateUserDTO testUser = new CreateUserDTO{Username = "TestUser2", Password = "TestUser", Email="test@tester.test"};

            await _repository.CreateAsync(testUser);
    
            var actual = await _repository.UserExistsAsync("TestUser2");

            // Assert
            Assert.Equal(true, actual);
        }

        [Fact]
        public async Task Test_If_Read_Returns_Correct_By_Userid()
        {
    
            UserDTO actual = await _repository.ReadAsync(1);

            UserDTO expected = new UserDTO{Id = 1, Username = "TestUser1", Email="test@test.com", Password = "TestPassword1234"};

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Test_If_Read_Returns_Null_When_False_By_Userid()
        {
    
            UserDTO actual = await _repository.ReadAsync(10000);

            UserDTO expected = null;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Test_If_Read_Returns_Correct_By_Username()
        {
    
            UserDTO actual = await _repository.ReadAsync("TestUser1");

            UserDTO expected = new UserDTO{Id = 1, Username = "TestUser1", Email="test@test.com", Password = "TestPassword1234"};

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Test_If_Read_Returns_Null_When_False_By_Username()
        {
    
            UserDTO actual = await _repository.ReadAsync("Non Existant Test User");

            UserDTO expected = null;

            // Assert
            Assert.Equal(expected, actual);
        }
        
    }
} 