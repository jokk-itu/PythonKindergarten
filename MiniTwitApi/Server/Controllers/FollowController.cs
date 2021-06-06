using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Server.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace MiniTwitApi.Server.Controllers
{
    [ApiController]
    [Route("")]
    public class FollowController : ControllerBase
    {
        private readonly IFollowerRepository _followerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IActionContextAccessor _accessor;
    
        public FollowController(IFollowerRepository repository, IUserRepository userRepository, 
            IConfiguration configuration, IActionContextAccessor accessor)
        {
            _userRepository = userRepository;
            _followerRepository = repository;
            _configuration = configuration;
            _accessor = accessor;
        }

        [HttpGet("fllws")]
        public async Task<ActionResult<FollowerDTO>> GetFollowRelationByWhoAndWhom([FromQuery] string whoUserName, [FromQuery] string whomUserName, [FromQuery] long latest)
        {
            if (string.IsNullOrEmpty(whomUserName) || string.IsNullOrEmpty(whoUserName))
                return BadRequest("whomUserName and whoUserName must be valid Username's");

            var followRelation = await _followerRepository.ReadAsync(whoUserName, whomUserName);
            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);

            return Ok(new FollowerDTO()
            {
                WhoId = followRelation.WhoId,
                WhomId = followRelation.WhomId
            });
        }
        
        [HttpGet("fllws/{username}")]
        public async Task<ActionResult<IList<FollowerDTO>>> GetFollowsByUsername(string username, [FromQuery] long latest)
        {
            if(!await _userRepository.UserExistsAsync(username))
                return NotFound();

            // Query follows from database by username
            var follows = await _followerRepository.ReadAllAsync(username);

            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);

            return Ok(follows);
        }
        
        [HttpPost("fllws/{username}")]
        public async Task<ActionResult> PostFollowsByUsername(string username, [FromBody] Follow follow, [FromQuery] long latest)
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

            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);
            
            return NoContent();
        }
    }
}
