using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureWebAPI.BusinessLogic;
using SecureWebAPI.BusinessLogic.Request;
using SecureWebAPI.Models;

namespace SecureWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamController : Controller
    {
        private readonly ClaimsPrincipal claimsPrincipal;
        private readonly string UserId;
        private readonly IServiceManager _service;

        public TeamController(IServiceManager service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            claimsPrincipal = httpContextAccessor.HttpContext.User;
            UserId = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpPost]
        public IActionResult Post([FromBody] TeamVM team)
        {
            var request = new TeamRequest { Team = team, UserId = UserId };
            var response = _service.CreateTeam(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }

        [HttpGet]
        [Route("getuserteams")]
        public IActionResult GetUserTeams()
        {
            var request = new TeamRequest { UserId = UserId };
            var response = _service.GetUserTeams(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }

        [HttpGet]
        [Route("getteambyid/{teamid}")]
        public IActionResult GetTeamById(string teamid)
        {
            var request = new TeamRequest { TeamId = Int32.Parse(teamid) };
            var response = _service.GetTeamById(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }
    }
}