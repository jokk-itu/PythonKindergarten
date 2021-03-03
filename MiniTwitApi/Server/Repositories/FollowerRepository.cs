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
        private IContext _context { get; }

        public FollowerRepository(IContext context) 
        {
            _context = context;
        }

        /* Should be implmeneted: int limit = 20*/
        public async Task<ICollection<FollowerDTO>> ReadAllAsync(string username)
        {
            return await (from s in _context.Followers
                where s.Who.Username.Equals(username)
                select new FollowerDTO
                {
                    WhoId = s.WhoId,
                    WhomId = s.WhomId
                }).ToListAsync();
        }

        public async Task<int> DeleteAsync(FollowerDTO follower) 
        {
            var _follower = await _context.Followers.FindAsync(follower);

            if(_follower is null)
            {
                throw new ArgumentException($"Could not remove follower, because it does not exist.");
            }
            
            _context.Followers.Remove(_follower);

            await _context.SaveChangesAsync();
            return follower.WhomId;
        }


        public async Task<int> CreateAsync(FollowerDTO follower) 
        {
            var entity = new Follower
            {
                WhoId = follower.WhoId,
                WhomId = follower.WhomId
            };
            await _context.Followers.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.WhoId;
        }
    
    }
}