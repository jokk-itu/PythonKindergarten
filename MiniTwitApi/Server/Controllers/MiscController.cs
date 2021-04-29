using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniTwitApi.Server.Repositories;
using MiniTwitApi.Shared;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Server.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace MiniTwitApi.Server.Controllers
{
    [ApiController]
    [Route("")]
    public class MiscController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IActionContextAccessor _accessor;

        public MiscController(IConfiguration configuration, IActionContextAccessor accessor)
        {
            _configuration = configuration;
            _accessor = accessor;
        }       
        
        [HttpGet("latest")]
        public async Task<ActionResult<GetLatestResponse>> GetLatest([FromQuery] long latest)
        {
            Log.Information($"Received latest request from IP: {_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()}");

            if(latest > 0 && _configuration["ApiSafeList"].Contains(_accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()))
                Latest.GetInstance().Update(latest);
                
            return Ok(new GetLatestResponse(Latest.GetInstance().Read()));
        }
        
        
        
    }
}