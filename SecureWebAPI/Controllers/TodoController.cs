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
using SecureWebAPI.Entities;
using SecureWebAPI.Extensions;
using SecureWebAPI.Models;

namespace SecureWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ClaimsPrincipal claimsPrincipal;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private ILoggerManager _logger;
        private readonly string UserId;

        public TodoController(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, IMapper mapper, ILoggerManager logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            claimsPrincipal = httpContextAccessor.HttpContext.User;
            UserId = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = _context.Todos.Where(u => u.UserId == UserId).ToArray();

            return Ok(new{
                UserId = UserId,
                Success = true,
                todos = result,
                Errors = "Errr",
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = _context.Todos.FirstOrDefault(t => t.Id == id);

            return Ok(new{
                UserId = UserId,
                Success = true,
                todos = _mapper.Map<TodoVM>(result),
                Errors = "Errr",
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodoVM todoVM)
        {
            string error = null;
            TodoVM todoresult = new TodoVM();
            try{
                var result = _context.Todos.Add(new Todo{UserId = UserId, Name = todoVM.Name});
                _context.SaveChanges();
                todoresult = _mapper.Map<TodoVM>(result);
            }
            catch(Exception ex){
                error = ex.Message;
                _logger.LogError(ex.Message);
            }

            return Ok(new{
                UserId = UserId,
                Success = !string.IsNullOrEmpty(error) ? true : false,
                todos = todoresult,
                Errors = error,
            }); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] TodoVM todoVM)
        {
            var todo = _mapper.Map<Todo>(todoVM);
            todo.UserId = UserId;
            var result = _context.Todos.Update(todo);
            _context.SaveChanges();

            return Ok(new{
                UserId = UserId,
                Success = result != null ? true : false,
                todos = _mapper.Map<TodoVM>(result),
                Errors = "Errr",
            }); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = _context.Todos.FirstOrDefault(t => t.Id == id);
            var result = todo != null ?_context.Todos.Remove(todo) : null;
            _context.SaveChanges();
            
            return Ok(new{
                UserId = UserId,
                Success = result != null ? true : false,
                todos = _mapper.Map<TodoVM>(result),
                Errors = "Errr",
            }); 
        }
    }
}