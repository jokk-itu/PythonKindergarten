using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using MiniTwitApi.Shared;
using MiniTwitApi.Shared.Models.UserModels;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Repositories;
using MiniTwitApi.Shared.Repositories.Abstractions;

namespace MiniTwitApi.Server.Controllers
{
    [ApiController]
    [Route("/old/")]
    public class MiniTwitController : ControllerBase
    {
        private readonly IMiniTwitRepository _database;
        private readonly IConfiguration _configuration;
        private readonly IActionContextAccessor _accessor;

        public MiniTwitController (IMiniTwitRepository repository, IConfiguration configuration, IActionContextAccessor accessor)
        {
            _database = repository;
            _configuration = configuration;
            _accessor = accessor;
        }

        //DONE
        [HttpPost("register")]
        public async Task<ActionResult> PostRegister([FromBody] UserDTO user, [FromQuery] long latest)
        {
            //checks if the user exists
            if(await _database.UserExistsAsync(user.Username))
                BadRequest("User already exists with given username");
            
            //insert the user
            await _database.InsertUserAsync(user);
            
            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);
            
            return NoContent();
        }

        [HttpGet("msgs")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMsgs([FromQuery] int no, [FromQuery] long latest)
        {
            // Query all messages
            var messages = await _database.QueryMessagesAsync(no);

            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);

            // Return messages
            return Ok(messages);
        }

        [HttpGet("msgs/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMsgsByUsername(string username, [FromQuery] int no, [FromQuery] long latest)
        {
            //Query messages by username
            var messages = await _database.QueryMessagesAsync(username, no);
            
            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);
            
            // Return messages
            return Ok(messages);
        }

        [HttpPost("msgs/{username}")]
        public async Task<ActionResult> PostMessageByUsername(string username, [FromBody] CreateMessage createMessage, [FromQuery] long latest)
        {
            //TODO check message for profanity, then flag it if it is true
            var actionUser = await _database.QueryUserByUsernameAsync(username);
            await _database.InsertMessageAsync(new MessageDTO
            {
                Author = actionUser.Id,
                AuthorUsername = username,
                Text = createMessage.Content,
                PublishDate = (int) EpochConverter.ToEpoch(DateTime.Now),
                Flagged = 0 // Flag if profanity is detected
            });

            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);
            
            return NoContent();
        }
        
        //DONE
        [HttpGet("fllws/{username}")]
        public async Task<ActionResult<IList<FollowerDTO>>> GetFollowsByUsername(string username, [FromQuery] long latest)
        {
            // Query follows from database by username
            var follows = await _database.QueryFollowers(username);

            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);
            
            return Ok(follows);
        }
        
        //DONE
        [HttpGet("user/{userid}")]
        public async Task<ActionResult<UserDTO>> GetUserByUserId(int userid, [FromQuery] long latest)
        {
            var user = await _database.QueryUserByIdAsync(userid);
            
            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);
            
            return Ok(user);
        }
        
        //DONE
        [HttpPost("fllws/{username}")]
        public async Task<ActionResult> PostFollowsByUsername(string username, [FromBody] Follow follow, [FromQuery] long latest)
        {
            // Find the user executing the action
            var actionUser = await _database.QueryUserByUsernameAsync(username);
            var targetUser = await _database.QueryUserByUsernameAsync(follow.ToFollow ?? follow.ToUnfollow);

            // Check if user is following or unfollowing
            if(follow.ToFollow != null){
                // Create follow from username to specified username
                await _database.InsertFollowAsync(new FollowerDTO
                {
                    WhoId = actionUser.Id,
                    WhomId = targetUser.Id,
                });
            }else{
                // Unfollow from username
                // Create follow from username to specified username
                await _database.RemoveFollowAsync(new FollowerDTO
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
