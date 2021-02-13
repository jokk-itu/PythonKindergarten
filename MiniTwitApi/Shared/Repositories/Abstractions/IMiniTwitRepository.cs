namespace MiniTwitApi.Shared.Repositories.Abstractions
{
    public interface IMiniTwitRepository
    {
        Task<bool> UserExistsAsync(string username);
        Task<IList<MessageDTO>> QueryMessagesAsync(string userid, int limit);
        Task<List<MessageDTO>> QueryMessagesAsync(int limit);
        Task InsertUserAsync(UserDTO user);
        Task InsertMessageAsync(MessageDTO message);
        Task<UserDTO> QueryUserByIdAsync(int userId);
        Task<UserDTO> QueryUserByUsernameAsync(string username);
        Task InsertFollowAsync(FollowDTO follow);
        Task RemoveFollowAsync(FollowDTO follow);
        Task<IList<FollowerDTO>> QueryFollowers(string userid);
    }
}