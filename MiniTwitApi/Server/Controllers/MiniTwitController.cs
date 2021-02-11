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
        public async Task<IActionResult> PostRegister([FromQuery] int latest)
        {
            //check for correct userdata
            //check for already used username
            //insert new user
            //return Created()

            _latest = latest;         
        }

        [HttpGet("msgs")]
        public async Task<IActionResult<IEnumerable<MessageDTO>> GetMsgs([FromQuery] int no, [FromQuery] int latest)
        {
            // Query all messages
            var messages = await _database.QueryMessagesAsync(no);

            // Update latest counter for simulator tests
            _latest = latest;

            // Return messages
            return messages;
        }

        [HttpGet("msgs/{username}")]
        public async Task<IActionResult<IEnumerable<MessageDTO>>> GetMsgsByUsername(string username, [FromQuery] int no, [FromQuery] int latest)
        {
            //Query messages by username
            var messages = await _database.QueryMessagesAsync(username, no);
            
            // Update latest counter for simulator tests
            _latest = latest;

            // Return messages
            return messages;
        }

        [HttpPost("msgs/{username}")]
        public async Task<IActionResult> PostMessageByUsername([FromBody] Message message, [FromQuery] int latest)
        {
            //check message for profanity, then flag it if it is true (this is a later thing)

            _latest = latest;            
        }

        [HttpGet("fllws/{username}")]
        public async Task<IActionResult<IEnumerable<FollowerDTO>>> GetFollowsByUsername([FromQuery] int latest)
        {
            // Query follows from database by username

            _latest = latest;
        }

        [HttpPost("fllws/{username}")]
        public async Task<IActionResult> PostFollowsByUsername([FromBody] Follow follow, [FromQuery] int latest)
        {
            // Create follow from username to specified username

            _latest = latest;
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