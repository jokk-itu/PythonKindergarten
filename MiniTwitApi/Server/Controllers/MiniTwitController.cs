using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniTwitApi.Shared;

namespace MiniTwitApi.Server.Controllers
{
    [ApiController]
    [Route("")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISQLRepository _database;
        private int _latest;

        public MiniTwitController(ISQLRepository repository)
        {
            _database = respository;
        }

        [HttpGet("latest")]
        public async Task<IActionResult<GetLatestResponse>> GetLatest([FromQuery] int latest)
            => Ok(new GetLatestResponse(_latest));


        [HttpPost("register")]
        public async Task<IActionResult> PostRegister([FromBody] UserDTO user, [FromQuery] int latest)
        {
            //checks if the user exists
            if(_database.UserExistsAsync(user.Username))
                BadRequest("User already exists with given username");
            
            //insert the user
            await _database.InsertUserAsync(user);
            _latest = latest;         
            Created();
        }

        [HttpGet("msgs")]
        public async Task<IActionResult<IEnumerable<MessageDTO>> GetMsgs([FromQuery] int no, [FromQuery] int latest)
        {
            // Query all messages
            var messages = await _database.QueryMessagesAsync(no);

            // Update latest counter for simulator tests
            _latest = latest;

            // Return messages
            return Ok(messages);
        }

        [HttpGet("msgs/{username}")]
        public async Task<IActionResult<IEnumerable<MessageDTO>>> GetMsgsByUsername(string username, [FromQuery] int no, [FromQuery] int latest)
        {
            //Query messages by username
            var messages = await _database.QueryMessagesAsync(username, no);
            
            // Update latest counter for simulator tests
            _latest = latest;

            // Return messages
            return Ok(messages);
        }

        [HttpPost("msgs/{username}")]
        public async Task<IActionResult> PostMessageByUsername(string username, [FromBody] MessageToPost message, [FromQuery] int latest)
        {
            //check message for profanity, then flag it if it is true (this is a later thing)
            var actionUser = await _database.QueryUserByUsernameAsync(username);
            var insert = await _database.InsertMessage(new MessageDTO{
                Author = actionUser.Id,
                Text = message.Content,
                PublishDate = EpochConverter.ToEpoch(DateTime.Now),
                Flagged = 0 // Flag if profanity is detected
            })

            _latest = latest;

            return OK();
        }

        [HttpGet("fllws/{username}")]
        public async Task<IActionResult<IEnumerable<FollowerDTO>>> GetFollowsByUsername(string username, [FromQuery] int latest)
        {
            // Query follows from database by username
            var follows = _database.QueryFollowers(string username)

            _latest = latest;

            return Ok(follows);
        }

        [HttpPost("fllws/{username}")]
        public async Task<IActionResult> PostFollowsByUsername(string username, [FromBody] Follow follow, [FromQuery] int latest)
        {
            // Find the user executing the action
            var actionUser = await _database.QueryUserByUsernameAsync(username);
            var targetUser = await _database.QueryUserByUsernameAsync(follow.Follow ?? follow.Unfollow);

            // CHeck if user is following or unfollowing
            if(follow.Follow != null){
                // Create follow from username to specified username
                var insert = await InsertFollowAsync(new FollowerDTO{
                    WhoId = actionUser.Id,
                    WhomId = targetUser.Id,
                }                
                });

            }else{
                // Unfollow from username
                // Create follow from username to specified username
                var remove = await RemoveFollowAsync(new FollowerDTO{
                    WhoId = actionUser.Id,
                    WhomId = targetUser.Id,
                }                
                });
            }

            _latest = latest;

            return Ok();
        }
    }
}

/** ENDPOINTS
* /latest    GET 
* /register  POST {'username': username, 'email': email, 'pwd': pwd} params ?latest=1
* /msgs  GET ?no=20&latest=3
* /msgs/{username}   GET ?no=20?latest=3, POST{'content' : content} ?latest=2
* /fllws/{username}     GET ?no=20&latest=9, POST {'follow': 'b'} | {'unfollow': 'b'} params ?latest=1
*/