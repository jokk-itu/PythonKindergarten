using MiniTwitApi.Server.Entities;

namespace MiniTwitApi.Server.Repositories.Abstract
{
    public interface IUserRepository 
    {
        Context Context { get; }
    }
}