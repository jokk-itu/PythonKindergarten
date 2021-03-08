using System.Text;
using System.Buffers.Text;
using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniTwitApi.Shared;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Server.Repositories.Abstract;
using MiniTwitApi.Shared.Models.UserModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace MiniTwitApi.Server.Controllers
{
    [ApiController]
    [Route("")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IActionContextAccessor _accessor;

        public UserController(IUserRepository repository, IConfiguration configuration, IActionContextAccessor accessor)
        {
            _repository = repository;
            _configuration = configuration;
            _accessor = accessor;
        }
        
        //TODO The bad requests gives away if the username exists. This is only used for debugging.
        [HttpPost("login")]
        public async Task<ActionResult> GetLogin([FromBody] LoginUserDTO user, [FromQuery] long latest)
        {
            var userFromDatabase = await _repository.ReadAsync(user.Username);

            if(userFromDatabase is null)
                return BadRequest("User does not exist");
            
            if(!BCrypt.CheckPassword(user.Password, userFromDatabase.Password))
                return BadRequest("Provided password is wrong");

            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);
            
            return NoContent();
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> PostRegister([FromBody] CreateUserDTO user, [FromQuery] long latest)
        {
            // Check if username is provided
            if(string.IsNullOrEmpty(user.Username))
                return BadRequest("You have to enter a username");

            // Check if password is provided
            if(string.IsNullOrEmpty(user.Password))
                return BadRequest("You have to enter a password");
                
            //Check if email is provided
            if(string.IsNullOrEmpty(user.Email))
                return BadRequest("You have to enter a valid email address");
            
            //checks if the user exists
            if(await _repository.UserExistsAsync(user.Username))
                return BadRequest("User already exists with given username");
            
            //insert the user
            var hashedPassword = BCrypt.HashPassword(user.Password, BCrypt.GenerateSalt(12));
            user.Password = hashedPassword;
            await _repository.CreateAsync(user);

            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);

            return NoContent();
        }
        
        [HttpGet("user/{userid}")]
        public async Task<ActionResult<UserDTO>> GetUserByUserId(int userid, [FromQuery] long latest)
        {
            var user = await _repository.ReadAsync(userid);
            
            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);
            
            return Ok(user);
        }
        
        [HttpGet("user/{username}")]
        public async Task<ActionResult<UserDTO>> GetUserByUserId(string username, [FromQuery] long latest)
        {
            var user = await _repository.ReadAsync(username);
            
            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);
            
            return Ok(user);
        }
    }
}
