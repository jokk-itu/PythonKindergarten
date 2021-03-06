using System.Threading.Tasks;

namespace MiniTwitApi.Client.Models.Abstract
{
    public interface IFollowModel
    {
        public Task FollowUser(string username);
        public Task UnfollowUser(string username);
    }
}