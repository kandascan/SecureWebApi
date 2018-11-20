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
    public class TaskController : Controller
    {
        private readonly ClaimsPrincipal claimsPrincipal;
        private readonly string UserId;
        private readonly IServiceManager _service;

        public TaskController(IServiceManager service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            claimsPrincipal = httpContextAccessor.HttpContext.User;
            UserId = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpGet]
        [Route("backlog")]
        public IActionResult GetBacklog()
        {
            var request = new TaskRequest();
            var response = _service.GetAllTasks(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TaskVM taskVm)
        {
            //taskVm.UserId = UserId;
            var request = new TaskRequest { Task = taskVm };
            var response = _service.CreateTask(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }
    }
}