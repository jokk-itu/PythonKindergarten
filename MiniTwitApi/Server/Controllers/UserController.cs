using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniTwitApi.Server.Repositories;
using MiniTwitApi.Shared;
using MiniTwitApi.Shared.Models;

namespace MiniTwitApi.Server.Controllers
{
    [ApiController]
    [Route("/user")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repository;

        public UserController(UserRepository repository)
        {
            _repository = repository;
        }
        
        [HttpPost("login")]
        public async Task<ActionResult> GetLogin([FromBody] UserDTO user, [FromQuery] int latest)
        {
            return Ok();
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> PostRegister([FromBody] UserDTO user, [FromQuery] int latest)
        {
            //checks if the user exists
            if(await _repository.UserExistsAsync(user.Username))
                BadRequest("User already exists with given username");
            
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