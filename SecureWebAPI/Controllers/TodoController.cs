using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LoggerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureWebAPI.BusinessLogic;
using SecureWebAPI.BusinessLogic.Request;
using SecureWebAPI.DataAccess.Entities;
using SecureWebAPI.Extensions;
using SecureWebAPI.Models;

namespace SecureWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ClaimsPrincipal claimsPrincipal;       
        private readonly string UserId;
        private readonly IServiceManager _service;

        public TodoController(IServiceManager service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            claimsPrincipal = httpContextAccessor.HttpContext.User;
            UserId = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var request = new TodoRequest{ UserId = UserId};
            var response = _service.GetUserTodos(request);
            return Ok(response);
        }
        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            var request = new TodoRequest();
            var response = _service.GetAllTodos(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var request = new TodoRequest{ TodoId = id };
            var response = _service.GetTodoById(request);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoVM todoVM)
        {
            todoVM.UserId = UserId;
            var request = new TodoRequest{ Todo = todoVM };
            var response = _service.CreateTodo(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put([FromBody] TodoVM todoVM)
        {
            todoVM.UserId = UserId;
            var request = new TodoRequest{ Todo = todoVM };
            var response = _service.UpdateTodo(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var request = new TodoRequest{ TodoId = id };
            var response = _service.RemoveTodo(request);
            return Ok(response);
        }
    }
}