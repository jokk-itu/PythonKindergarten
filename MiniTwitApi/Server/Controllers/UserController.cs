using System.Text;
using System.Buffers.Text;
using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniTwitApi.Server.Repositories;
using MiniTwitApi.Shared;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Server.Repositories.Abstract;

namespace MiniTwitApi.Server.Controllers
{
    [ApiController]
    [Route("")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }
        
        [HttpPost("login")]
        public async Task<ActionResult> GetLogin([FromBody] UserDTO user, [FromQuery] int latest)
        {
            return Ok();
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> PostRegister([FromBody] CreateUserDTO user, [FromQuery] int latest)
        {
            //checks if the user exists
            if(await _repository.UserExistsAsync(user.Username))
                return BadRequest("User already exists with given username");

            // Check if username is provided
            if(string.IsNullOrEmpty(user.Username))
                return BadRequest("You have to enter a username");

            // Check if password is provided
            if(string.IsNullOrEmpty(user.Password))
                return BadRequest("You have to enter a password");
                
            //Check if email is provided
            if(string.IsNullOrEmpty(user.Email) || !user.Email.Contains("@"))
                return BadRequest("You have to enter a valid email address");
            
            
            //insert the user
            await _repository.CreateAsync(user);
            DeleteMe.Latest = latest;
            return Ok();
        }
        
        [HttpGet("user/{userid}")]
        public async Task<ActionResult<UserDTO>> GetUserByUserId(int userid, [FromQuery] int latest)
        {
            var user = await _repository.ReadAsync(userid);
            
            return user;
        }
    }
}
