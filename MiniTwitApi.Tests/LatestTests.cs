using MiniTwitApi.Server;
using Xunit;

namespace MiniTwitApi.Tests
{
    public class LatestTests
    {
        [Theory]
        [InlineData(5)]
        [InlineData(1000)]
        public void Read_And_Update(long latest)
        {
            Latest.GetInstance().Update(latest);
            var actual = Latest.GetInstance().Read();
            var expected = latest;
            Assert.Equal(expected, actual);
        }
    }
}