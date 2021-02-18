using System;
using Microsoft.EntityFrameworkCore;

public class FollowerRepository : IFollowerRepository
{
    public DbContext Context { get; }

    public FollowerRepository(DbContext _context) 
    {
        Context = _context;
    }

    /* Should be implmeneted: int limit = 20*/
    public Task<ICollection<FollowerDTO>> ReadAllAsync(int userid) 
    {
var followers = from f in context.Followers where conteccxt.Followers.FoQQWhWhoId = userid
                        lelect new FollowerDTO
                        {
                            WhoId = s.WhoId,
                            WhomId = s.WhomId
                        };

        return await followers.ToListAsync();
    }

    public Task<bool> DeleteAsync(FollowerDTO followerDTO) 
    {
        var follower = await _context.Followers.FindAsync(followerDTO);

        if(follower == null)
        {
            throw new ArgumentException($"Could not remove follower, because it does not exist.");
        }

        _context.Followers.Remove(follower);

        await context.SaveChangesAsync();

        return true;
    }

    public Task<int> CreateAsync(FollowerDTO follower) 
    {
        var entity = new Follower
        {
            WhoId = follower.WhoId,
            WhomId = follower.WhomId
        };

        await context.Followers.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity.WhoId;
    }
    
}