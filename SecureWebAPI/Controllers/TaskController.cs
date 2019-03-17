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
            // = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //_service.IsUserTeamMember(new BaseRequest {  UserId = UserId})
        }

        [HttpPost]
        [Route("sortedbacklog")]
        public IActionResult SortBacklogItems([FromBody] IEnumerable<TaskVM> taskVm)
        {
            var request = new SortBacklogItemsRequest { Items = taskVm };
            var response = _service.SortBacklogItems(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }

        [HttpGet]
        [Route("getbacklogtasks/{teamid}")]
        public IActionResult GetBacklog(string teamid)
        {

            var request = new GetBacklogTasksRequest { TeamId = Int32.Parse(teamid), UserId = UserId };
            var response = _service.GetBacklogTasks(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }

        [HttpGet]
        [Route("gettaskbyid/{id}")]
        public IActionResult GetTaskById(string id)
        {
            var request = new TaskRequest { TaskId = Int32.Parse(id) };
            var response = _service.GetTaskById(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }

        [HttpGet]
        [Route("getteambacklog/{teamid}")]
        public IActionResult GetTeamBacklog(string teamid)
        {
            var request = new BacklogRequest { TeamId = Int32.Parse(teamid) };
            var response = _service.GetTeamBacklog(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveTask(int id)
        {
            var request = new RemoveTaskRequest { Id = id };
            var response = await _service.RemoveTask(request);
            return response.Success ? Ok(response) : StatusCode(404, response.Errors);
        }

        [HttpPut]
        public IActionResult Put([FromBody] TaskVM taskVm)
        {
            //taskVm.UserId = UserId;
            var request = new TaskRequest { Task = taskVm };
            var response = _service.UpdateTask(request);
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