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
    public class ListController : Controller
    {
        private readonly ClaimsPrincipal claimsPrincipal;
        private readonly string UserId;
        private readonly IServiceManager _service;

        public ListController(IServiceManager service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            claimsPrincipal = httpContextAccessor.HttpContext.User;
            UserId = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpGet]
        [Route("getpriorities")]
        public IActionResult GetPriorities()
        {
            var request = new GetPrioritiesRequest();
            var response = _service.GetPriorities(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }


        [HttpGet]
        [Route("getefforts")]
        public IActionResult GetEfforts()
        {
            var request = new GetEffortsRequest();
            var response = _service.GetEfforts(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }

        [HttpGet]
        [Route("getsprintslist/{teamid}")]
        public IActionResult GetSprintsList(string teamid)
        {
            var request = new SprintRequest { TeamId = Int32.Parse(teamid) };
            var response = _service.GetSprintsList(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }
    }
}