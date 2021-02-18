using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniTwitApi.Shared.Models;
using MyApp.Entities;

public class FollowerRepository : IFollowerRepository
{
    public Context Context { get; }

    public FollowerRepository(Context context) 
    {
        Context = context;
    }

    /* Should be implmeneted: int limit = 20*/
    public async Task<ICollection<FollowerDTO>> ReadAllAsync(int userid) 
    {
        var followers = from s in Context.Followers 
                        where s.WhoId == userid
                        select new FollowerDTO
                        {
                            WhoId = s.WhoId,
                            WhomId = s.WhomId
                        };

        return await followers.ToListAsync();
    }

    public async Task<bool> DeleteAsync(FollowerDTO followerDTO) 
    {
        var follower = await Context.Followers.FindAsync(followerDTO);

        if(follower == null)
        {
            throw new ArgumentException($"Could not remove follower, because it does not exist.");
        }

        Context.Followers.Remove(follower);

        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<int> CreateAsync(FollowerDTO follower) 
    {
        var entity = new Follower
        {
            WhoId = follower.WhoId,
            WhomId = follower.WhomId
        };

        await Context.Followers.AddAsync(entity);
        await Context.SaveChangesAsync();

        return entity.WhoId;
    }
    
}