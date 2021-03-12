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

        public async Task<Follower> ReadAsync(string whoUsername, string whomUsername)
        {
            var whoUser = await _context.Users
                .Where(u => u.Username.Equals(whoUsername))
                .Select(u => u).FirstAsync();
            
            var whomUser = await _context.Users
                .Where(u => u.Username.Equals(whomUsername))
                .Select(u => u).FirstAsync();

            var relationship = await _context.Followers.FindAsync(whoUser.UserId, whomUser.UserId);
            return relationship ?? new Follower()
            {
                WhoId = -1,
                WhomId = -1
            };
        }
        
        public async Task<ICollection<FollowerDTO>> ReadAllAsync(string username)
        {
            return await (from f in _context.Followers
                where f.Who.Username.Equals(username)
                select new FollowerDTO
                {
                    WhoId = f.WhoId,
                    WhomId = f.WhomId
                }).ToListAsync();
        }

        public async Task<int> DeleteAsync(FollowerDTO follower)
        {
            var _follower = await _context.Followers.FindAsync(follower.WhoId, follower.WhomId);

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