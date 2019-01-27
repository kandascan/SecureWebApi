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

        [HttpDelete]
        [Route("deleteuserfromteam")]
        public IActionResult DeleteUserFromTeam(XRefUserTeam xrefUserTeam)
        {
            var request = new UserRequest { UserTeam = xrefUserTeam };
            var response = _service.DeleteUserFromTeam(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }
        [HttpGet]
        [Route("getalluserswitusersinteam/{teamid}")]
        public IActionResult GetAllUsersWithouUsersInTeam(string teamid)
        {
            var request = new UserRequest { UserTeam = new XRefUserTeam { TeamId = Int32.Parse(teamid), UserId = UserId } };
            var response = _service.GetAllUsersWithouUsersInTeam(request);
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

        [HttpGet]
        [Route("getuserroles")]
        public IActionResult GetUserRoles()
        {
            var request = new RoleRequest();
            var response = _service.GetUserRoles(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }

        [HttpPost]
        [Route("addusertoteam")]
        public IActionResult AddUserToTeam(UserVM teamUser)
        {
            var request = new UserRequest { User = teamUser };
            var response = _service.AddUserToTeam(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }
    }
}