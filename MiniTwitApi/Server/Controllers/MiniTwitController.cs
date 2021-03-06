using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniTwitApi.Shared;
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

        public MiniTwitController (IMiniTwitRepository repository)
        {
            _database = repository;
        }

        //DONE
        [HttpPost("register")]
        public async Task<ActionResult> PostRegister([FromBody] UserDTO user, [FromQuery] int latest)
        {
            //checks if the user exists
            if(await _database.UserExistsAsync(user.Username))
                BadRequest("User already exists with given username");
            
            //insert the user
            await _database.InsertUserAsync(user);
            DeleteMe.Latest = latest;
            return NoContent();
        }

        [HttpGet("msgs")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMsgs([FromQuery] int no, [FromQuery] int latest)
        {
            // Query all messages
            var messages = await _database.QueryMessagesAsync(no);

            // Update latest counter for simulator tests
            DeleteMe.Latest = latest;
            // Return messages
            return Ok(messages);
        }

        [HttpGet("msgs/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMsgsByUsername(string username, [FromQuery] int no, [FromQuery] int latest)
        {
            //Query messages by username
            var messages = await _database.QueryMessagesAsync(username, no);
            
            // Update latest counter for simulator tests
            DeleteMe.Latest = latest;
            
            // Return messages
            return Ok(messages);
        }

        [HttpPost("msgs/{username}")]
        public async Task<ActionResult> PostMessageByUsername(string username, [FromBody] MessageToPost message, [FromQuery] int latest)
        {
            //TODO check message for profanity, then flag it if it is true
            var actionUser = await _database.QueryUserByUsernameAsync(username);
            await _database.InsertMessageAsync(new MessageDTO
            {
                Author = actionUser.Id,
                AuthorUsername = username,
                Text = message.Content,
                PublishDate = (int) EpochConverter.ToEpoch(DateTime.Now),
                Flagged = 0 // Flag if profanity is detected
            });

            DeleteMe.Latest = latest;
            
            return NoContent();
        }
        
        //DONE
        [HttpGet("fllws/{username}")]
        public async Task<ActionResult<IList<FollowerDTO>>> GetFollowsByUsername(string username, [FromQuery] int latest)
        {
            // Query follows from database by username
            var follows = await _database.QueryFollowers(username);

            DeleteMe.Latest = latest;
            
            return Ok(follows);
        }
        
        //DONE
        [HttpGet("user/{userid}")]
        public async Task<ActionResult<UserDTO>> GetUserByUserId(int userid, [FromQuery] int latest)
        {
            var user = await _database.QueryUserByIdAsync(userid);
            
            return Ok(user);
        }
        
        //DONE
        [HttpPost("fllws/{username}")]
        public async Task<ActionResult> PostFollowsByUsername(string username, [FromBody] Follow follow, [FromQuery] int latest)
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

            DeleteMe.Latest = latest;
            return NoContent();
        }
    }
}
