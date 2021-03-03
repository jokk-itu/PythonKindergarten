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
    public class MiscController : ControllerBase
    {
        public MiscController()
        {
        }       
        
        [HttpGet("latest")]
        public async Task<ActionResult<GetLatestResponse>> GetLatest([FromQuery] int latest)
            => Ok(new GetLatestResponse(Latest.GetInstance().Read()));
        
    }
}