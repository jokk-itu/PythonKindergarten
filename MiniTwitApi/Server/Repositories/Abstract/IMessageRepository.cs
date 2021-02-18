using MiniTwitApi.Server.Entities;

namespace MiniTwitApi.Server.Repositories.Abstract
{
    public interface IMessageRepository 
    {
        Context Context { get; }
    }
}