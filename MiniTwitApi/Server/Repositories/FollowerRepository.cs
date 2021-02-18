using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniTwitApi.Server.Entities;
using MiniTwitApi.Server.Repositories.Abstract;
using MiniTwitApi.Shared.Models;

namespace MiniTwitApi.Server.Repositories
{
    public class FollowerRepository : IFollowerRepository
    {
        public Context Context { get; }

        public FollowerRepository(Context context) 
        {
            Context = context;
        }

        /* Should be implmeneted: int limit = 20*/
        public async Task<ICollection<FollowerDTO>> ReadAllAsync(string username) =>
            await (from s in Context.Followers
                where s.WhoUser.Username.Equals(username)
                select new FollowerDTO
                {
                    WhoId = s.WhoId,
                    WhomId = s.WhomId
                }).ToListAsync();

        

        public async Task DeleteAsync(FollowerDTO follower) 
        {
            var _follower = await Context.Followers.FindAsync(follower);

            if(_follower == null)
            {
                throw new ArgumentException($"Could not remove follower, because it does not exist.");
            }

            Context.Followers.Remove(_follower);

            await Context.SaveChangesAsync();
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
}