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
        
        //TODO The bad requests gives away if the username exists. This is only used for debugging.
        [HttpPost("login")]
        public async Task<ActionResult> GetLogin([FromBody] LoginUserDTO user, [FromQuery] int latest)
        {
            var userFromDatabase = await _repository.ReadAsync(user.Username);

            if(userFromDatabase is null)
                return BadRequest("User does not exist");
            
            if(!BCrypt.CheckPassword(user.Password, userFromDatabase.Password))
                return BadRequest("Provided password is wrong");

            Latest.GetInstance().Update(latest);
            return Ok();
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> PostRegister([FromBody] CreateUserDTO user, [FromQuery] int latest)
        {
            // Check if username is provided
            if(string.IsNullOrEmpty(user.Username))
                return BadRequest("You have to enter a username");

            // Check if password is provided
            if(string.IsNullOrEmpty(user.Password))
                return BadRequest("You have to enter a password");
                
            //Check if email is provided
            if(string.IsNullOrEmpty(user.Email) || !user.Email.Contains("@"))
                return BadRequest("You have to enter a valid email address");
            
            //checks if the user exists
            if(await _repository.UserExistsAsync(user.Username))
                return BadRequest("User already exists with given username");
            
            //insert the user
            var hashedPassword = BCrypt.HashPassword(user.Password, BCrypt.GenerateSalt(12));
            user.Password = hashedPassword;
            await _repository.CreateAsync(user);
            Latest.GetInstance().Update(latest);
            return Ok();
        }
        
        [HttpGet("user/{userid}")]
        public async Task<ActionResult<UserDTO>> GetUserByUserId(int userid, [FromQuery] int latest)
        {
            var user = await _repository.ReadAsync(userid);
            Latest.GetInstance().Update(latest);
            return user;
        }
    }
}
