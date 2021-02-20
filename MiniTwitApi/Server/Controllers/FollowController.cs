using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniTwitApi.Server.Repositories;
using MiniTwitApi.Shared;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Server.Repositories.Abstract;

namespace MiniTwitApi.Server.Controllers
{
    [ApiController]
    [Route("/api")]
    public class FollowController : ControllerBase
    {
        private readonly IFollowerRepository _followerRepository;
        private readonly IUserRepository _userRepository;
    
        public FollowController(IFollowerRepository repository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _followerRepository = repository;
        }
        
        [HttpGet("fllws/{username}")]
        public async Task<ActionResult<IList<FollowerDTO>>> GetFollowsByUsername(string username, [FromQuery] int latest)
        {
            // Query follows from database by username
            var follows = await _followerRepository.ReadAllAsync(username);

            DeleteMe.Latest = latest;
            
            return Ok(follows);
        }
        
        [HttpPost("fllws/{username}")]
        public async Task<ActionResult> PostFollowsByUsername(string username, [FromBody] Follow follow, [FromQuery] int latest)
        {
            // Find the user executing the action
            var actionUser = await _userRepository.ReadAsync(username);
            var targetUser = await _userRepository.ReadAsync(follow.ToFollow ?? follow.ToUnfollow);

            // Check if user is following or unfollowing
            if(follow.ToFollow is not null)
            {
                // Create follow from username to specified username
                await _followerRepository.CreateAsync(new FollowerDTO
                {
                    WhoId = actionUser.Id,
                    WhomId = targetUser.Id,
                });
            }
            else
            {
                // Unfollow from username
                // Create follow from username to specified username
                await _followerRepository.DeleteAsync(new FollowerDTO
                {
                    WhoId = actionUser.Id,
                    WhomId = targetUser.Id,
                });
            }

            DeleteMe.Latest = latest;
            return Ok();
        }
    }
}