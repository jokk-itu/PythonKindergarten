using MiniTwitApi.Server.Entities;

namespace MiniTwitApi.Tests
{
    public static class TestDataGenerator
    {
        public static void GenerateTestData(this Context context)
        {
            context.Users.AddRange(
                    new User{UserId = 1, Username = "TestUser1", Email="test@test.com", Password = "TestPassword1234"}
                );
            context.SaveChanges();
        }
    }
}