using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniTwitApi.Server.Repositories;
using MiniTwitApi.Shared;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Repositories;
using MiniTwitApi.Server.Repositories.Abstract;

namespace MiniTwitApi.Server.Controllers
{
    [ApiController]
    [Route("")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messagesRepository;
        private readonly IUserRepository _userRepository;

        public MessageController(IMessageRepository messagesRepository, IUserRepository userRepository)
        {
            _messagesRepository = messagesRepository;
            _userRepository = userRepository;
        }
        
        //What is no???? Number of messages perhaps?
        [HttpGet("msgs")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMsgs([FromQuery] int no, [FromQuery] int latest)
        {
            // Query all messages
            var messages = await _messagesRepository.ReadAllAsync(no);

            // Update latest counter for simulator tests
            Latest.GetInstance().Update(latest);
            // Return messages
            return Ok(messages);
        }
        
        [HttpGet("msgs/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMsgsByUsername(string username, [FromQuery] int no, [FromQuery] int latest)
        {
            if(!await _userRepository.UserExistsAsync(username))
                return NotFound();

            //Query messages by username
            var messages = await _messagesRepository.ReadAllUserAsync(username, no);
            foreach(var message in messages)
            {
               Console.WriteLine(message.Text); 
            }
            // Update latest counter for simulator tests
            Latest.GetInstance().Update(latest);
            
            // Return messages
            return Ok(messages);
        }

        [HttpPost("msgs/{username}")]
        public async Task<ActionResult> PostMessageByUsername(string username, [FromBody] MessageToPost message, [FromQuery] int latest)
        {

            if(!await _userRepository.UserExistsAsync(username))
                return NotFound();

            if(string.IsNullOrEmpty(message.Content))
                return BadRequest("You have to enter content");

            
            //TODO check message for profanity, then flag it if it is true
            var actionUser = await _userRepository.ReadAsync(username);
            await _messagesRepository.CreateAsync(new MessageDTO
            {
                Author = actionUser.Id,
                AuthorUsername = username,
                Text = message.Content,
                PublishDate = (int) EpochConverter.ToEpoch(DateTime.Now),
                Flagged = 0 // Flag if profanity is detected
            });

            Latest.GetInstance().Update(latest);
            
            return Ok();
        }
    }
}
