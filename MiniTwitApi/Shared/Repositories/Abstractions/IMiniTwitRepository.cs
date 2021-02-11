namespace MiniTwitApi.Shared.Repositories.Abstractions
{
    public interface IMiniTwitRepository
    {
        Task<bool> UserExistsAsync(string username);
        Task<IList<MessageDTO>> QueryMessagesAsync(string userid, int limit);
        Task<List<MessageDTO>> QueryMessagesAsync(int limit);
        Task InsertUserAsync(UserDTO user);
        Task<UserDTO> QueryUserAsync(int userId);
        Task InsertFollowAsync(FollowDTO follow);
        Task RemoveFollowAsync(FollowDTO follow);
        Task<IList<FollowerDTO>> QueryFollowers(string userid);
    }
}