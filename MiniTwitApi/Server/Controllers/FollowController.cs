using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniTwitApi.Shared;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Server.Repositories.Abstract;

namespace MiniTwitApi.Server.Controllers
{
    [ApiController]
    [Route("")]
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
            if(!await _userRepository.UserExistsAsync(username))
                return NotFound();

            // Query follows from database by username
            var follows = await _followerRepository.ReadAllAsync(username);
            Latest.GetInstance().Update(latest);
            return Ok(follows);
        }
        
        [HttpPost("fllws/{username}")]
        public async Task<ActionResult> PostFollowsByUsername(string username, [FromBody] Follow follow, [FromQuery] int latest)
        {
            if(!await _userRepository.UserExistsAsync(username))
                return NotFound();

            if(string.IsNullOrEmpty(follow.ToFollow) && string.IsNullOrEmpty(follow.ToUnfollow))
                return BadRequest("You have to send a username to follow or unfollow");

            // Find the user executing the action
            var actionUser = await _userRepository.ReadAsync(username);
            var targetUser = await _userRepository.ReadAsync(
                string.IsNullOrEmpty(follow.ToFollow) ? follow.ToUnfollow : follow.ToFollow);

            // Check if user is following or unfollowing
            if(string.IsNullOrEmpty(follow.ToFollow))
            {
                await _followerRepository.DeleteAsync(new FollowerDTO
                {
                    WhoId = actionUser.Id,
                    WhomId = targetUser.Id,
                });
            }
            else
            {
                await _followerRepository.CreateAsync(new FollowerDTO
                {
                    WhoId = actionUser.Id,
                    WhomId = targetUser.Id,
                });
            }

            Latest.GetInstance().Update(latest);
            return Ok();
        }
    }
}
