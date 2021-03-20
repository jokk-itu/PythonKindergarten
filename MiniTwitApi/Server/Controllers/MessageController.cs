using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniTwitApi.Shared;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Server.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace MiniTwitApi.Server.Controllers
{
    [ApiController]
    [Route("")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messagesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IActionContextAccessor _accessor;

        private ProfanityFilter filter = new ProfanityFilter();

        public MessageController(IMessageRepository messagesRepository, IUserRepository userRepository, 
            IConfiguration configuration, IActionContextAccessor accessor)
        {
            _messagesRepository = messagesRepository;
            _userRepository = userRepository;
            _configuration = configuration;
            _accessor = accessor;
        }
        
        [HttpGet("msgs")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMsgs([FromQuery] int no, [FromQuery] int skip, [FromQuery] long latest)
        {
            // Query all messages
            var messages = await _messagesRepository.ReadAllAsync(no, skip);

            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);

            // Return messages
            return Ok(messages);
        }
        
        [HttpGet("msgs/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMsgsByUsername(string username, [FromQuery] int no, [FromQuery] int skip, [FromQuery] long latest)
        {
            if(!await _userRepository.UserExistsAsync(username))
                return NotFound();

            //Query messages by username
            var messages = await _messagesRepository.ReadAllUserAsync(username, no, skip);

            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);
            
            // Return messages
            return Ok(messages);
        }

        [HttpPost("msgs/{username}")]
        public async Task<ActionResult> PostMessageByUsername(string username, [FromBody] CreateMessage createMessage, [FromQuery] int latest)
        {

            if(!await _userRepository.UserExistsAsync(username))
                return NotFound();

            if(string.IsNullOrEmpty(createMessage.Content))
                return BadRequest("You have to enter content");

            
            //TODO check message for profanity, then flag it if it is true
            var actionUser = await _userRepository.ReadAsync(username);
            await _messagesRepository.CreateAsync(new MessageDTO
            {
                Author = actionUser.Id,
                AuthorUsername = username,
                Text = createMessage.Content,
                PublishDate = (int) EpochConverter.ToEpoch(DateTime.Now),
                Flagged = filter.checkString(createMessage.Content)
            });

            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);
            
            return NoContent();
        }
    }
}
