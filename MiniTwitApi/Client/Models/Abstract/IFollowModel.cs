using System.Threading.Tasks;

namespace MiniTwitApi.Client.Models.Abstract
{
    public interface IFollowModel
    {
        public Task FollowUser(string myUsername, string followerUsername);
        public Task UnfollowUser(string myUsername, string followerUsername);
    }
}