using System;
using System.Runtime.InteropServices;
using MiniTwitApi.Shared;
using Xunit;
using Xunit.Abstractions;

namespace MiniTwitApi.Tests
{
    public class BCryptTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public BCryptTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData("kelsen")]
        [InlineData("testing")]
        [InlineData("342525#%&&(/%")]
        public void CheckPassword_Given_Password(string plainPassword)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var hashedPassword = BCrypt.HashPassword(plainPassword, BCrypt.GenerateSalt(12));
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            _testOutputHelper.WriteLine(elapsedTime.ToString());
            Assert.True(BCrypt.CheckPassword(plainPassword, hashedPassword));
        }
        
        [Theory]
        [InlineData("kelsen", "hello")]
        [InlineData("214134n¤%#&¤", "%%232353##¤")]
        public void CheckPassword_Given_InValid_Password(string plainPassword, string invalidPassword) 
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var hashedPassword = BCrypt.HashPassword(plainPassword, BCrypt.GenerateSalt(12));
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            _testOutputHelper.WriteLine(elapsedTime.ToString());
            Assert.False(BCrypt.CheckPassword(invalidPassword, hashedPassword));
        }
    }
}