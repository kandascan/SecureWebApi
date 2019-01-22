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
    public class UserController : Controller
    {
        private readonly ClaimsPrincipal claimsPrincipal;
        private readonly string UserId;
        private readonly IServiceManager _service;

        public UserController(IServiceManager service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            claimsPrincipal = httpContextAccessor.HttpContext.User;
            UserId = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpGet]
        [Route("getalluserswithoutme")]
        public IActionResult GetAllUsersWithoutMe()
        {
            var request = new UserRequest { UserId = UserId };
            var response = _service.GetAllUsersWithoutMe(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }

        [HttpGet]
        [Route("getteamusers/{teamid}")]
        public IActionResult GetTeamUsers(string teamid)
        {
            var request = new TeamRequest { TeamId = Int32.Parse(teamid), UserId = UserId };
            var response = _service.GetTeamUsers(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }
    }
}