using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniTwitApi.Server.Repositories;
using MiniTwitApi.Shared;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Repositories;

namespace MiniTwitApi.Server.Controllers
{
    [ApiController]
    [Route("/api")]
    public class MessageController : ControllerBase
    {
        private readonly MessageRepository _messagesRepository;
        private readonly UserRepository _userRepository;

        public MessageController(MessageRepository messagesRepository, UserRepository userRepository)
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
            DeleteMe.Latest = latest;
            // Return messages
            return Ok(messages);
        }
        
        [HttpGet("msgs/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMsgsByUsername(string username, [FromQuery] int no, [FromQuery] int latest)
        {
            //Query messages by username
            var messages = await _messagesRepository.ReadAllAsync(username, no);
            
            // Update latest counter for simulator tests
            DeleteMe.Latest = latest;
            
            // Return messages
            return Ok(messages);
        }

        [HttpPost("msgs/{username}")]
        public async Task<ActionResult> PostMessageByUsername(string username, [FromBody] MessageToPost message, [FromQuery] int latest)
        {
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

            DeleteMe.Latest = latest;
            
            return Ok();
        }
    }
}