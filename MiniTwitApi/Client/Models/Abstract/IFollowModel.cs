using System.Threading.Tasks;

namespace MiniTwitApi.Client.Models.Abstract
{
    public interface IFollowModel
    {
        public Task<bool> FollowUser(string myUsername, string followerUsername);
        public Task<bool> UnfollowUser(string myUsername, string followerUsername);

        public Task<bool> IsFollowed(string whoUsername, string whomUsername);

        public Task<int> FollowerCount(string username);
    }
}