using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniTwitApi.Shared;
using MiniTwitApi.Shared.Models;

namespace MiniTwitApi.Server.Controllers
{
    [ApiController]
    [Route("")]
    public class LatestController : ControllerBase
    {
        public LatestController() {}
        
        [HttpGet("latest")]
        public async Task<ActionResult<GetLatestResponse>> GetLatest([FromQuery] int latest)
            => Ok(new GetLatestResponse(Latest.GetInstance().Read()));
    }
}