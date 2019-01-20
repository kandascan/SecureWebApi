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
    public class SprintController : Controller
    {
        private readonly ClaimsPrincipal claimsPrincipal;
        private readonly string UserId;
        private readonly IServiceManager _service;

        public SprintController(IServiceManager service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            claimsPrincipal = httpContextAccessor.HttpContext.User;
            UserId = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpGet]
        [Route("getcurrentsprint/{teamid}")]
        public IActionResult GetCurrentSprint(string teamid)
        {
            var request = new SprintRequest { TeamId = Int32.Parse(teamid) };
            var response = _service.GetCurrentSprint(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SprintVM sprint)
        {
            var request = new SprintRequest { Sprint = sprint, UserId = UserId };
            var response = _service.CreateSprint(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }
    }
}