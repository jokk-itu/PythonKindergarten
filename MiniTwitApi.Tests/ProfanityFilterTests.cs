using MiniTwitApi.Server;
using Xunit;

namespace MiniTwitApi.Tests
{
    public class ProfanityFilterTests
    {
        [Theory]
        [InlineData("xtc", 1)]
        [InlineData("xxx", 1)]
        [InlineData("yankee", 1)]
        [InlineData("yellowman", 1)]
        [InlineData("zipperhead", 1)]
        [InlineData("naturalword", 0)]
        [InlineData("goodword", 1)]

        public void both_good_and_bad_single_words(string input, int expected)
        {
            ProfanityFilter filter = new ProfanityFilter();

            var actual = filter.checkString(input);
        }

        [Theory]
        [InlineData("xtc dwwdw", 1)]
        [InlineData("xxx ddwddddd", 1)]
        [InlineData("yankee hahahahaha", 1)]
        [InlineData("yellowman t estt est est", 1)]
        [InlineData("zipperhead this is a test ", 1)]
        [InlineData("naturalword test of multiple words", 0)]
        [InlineData("goodword in a good test", 1)]

        public void both_good_and_bad_multiple_words(string input, int expected)
        {
            ProfanityFilter filter = new ProfanityFilter();

            var actual = filter.checkString(input);
        }
    }
}