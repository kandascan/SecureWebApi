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
using SecureWebAPI.DataAccess.Entities;
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
        private string Errors; //TODO: Errors should comes from ResponseBase Model.


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
            var response = new List<TodoVM>();
            try
            {
                var result = _context.Todos.Where(u => u.UserId == UserId).ToArray();
                response = _mapper.Map<List<TodoVM>>(result);
            }
            catch (Exception ex)
            {
                Errors = ex.Message;
                _logger.LogError(ex.Message);
            }
            return Ok(new{
                UserId,
                Success = string.IsNullOrEmpty(Errors) ? true : false,
                todos = response,
                Errors,
            });
        }
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var response = new List<TodoVM>();
            try
            {
                var result = _context.Todos.ToArray();
                response = _mapper.Map<List<TodoVM>>(result);
            }
            catch (Exception ex)
            {
                Errors = ex.Message;
                _logger.LogError(ex.Message);
            }
            return Ok(new
            {
                UserId,
                Success = string.IsNullOrEmpty(Errors) ? true : false,
                todos = response,
                Errors,
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new TodoVM();
            try
            {
                var result = _context.Todos.FirstOrDefault(t => t.Id == id);
                response = _mapper.Map<TodoVM>(result);
            }
            catch (Exception ex)
            {
                Errors = ex.Message;
                _logger.LogError(ex.Message);
            }

            return Ok(new{
                UserId,
                Success = string.IsNullOrEmpty(Errors) ? true : false,
                todo = response,
                Errors,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodoVM todoVM)
        {
            var response = new TodoVM();//TODO: Changed to Request Response in future in each method
            var todo = _mapper.Map<TodoEntity>(todoVM);
            todo.UserId = UserId;
            try
            {                           //TODO: Loginc in AppManager 
                var result = _context.Todos.Add(todo);
                _context.SaveChanges();//TODO: repository for this
                response = _mapper.Map<TodoVM>(result.Entity);//TODO: Mapping in AppManager
            }
            catch(Exception ex){
                Errors = ex.Message;
                _logger.LogError(ex.Message);
            }

            return Ok(new{
                UserId,
                Success = string.IsNullOrEmpty(Errors) ? true : false,//TODO: This also should comes from Request Response
                todo = response,
                Errors,
            }); 
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TodoVM todoVM)
        {
            var response = new TodoVM();//TODO: Changed to Request Response in future in each method
            var todo = _mapper.Map<TodoEntity>(todoVM);
            todo.UserId = UserId;
            todo.CreatedDate = DateTime.Now; //Changed in future to get CreatedDate too and leave this as original and implemented UpdateDate and set to now.
            try
            {
                var result = _context.Todos.Update(todo);
                _context.SaveChanges();
                response = _mapper.Map<TodoVM>(result.Entity);
            }
            catch (Exception ex)
            {
                Errors = ex.Message;
                _logger.LogError(ex.Message);
            }

            return Ok(new{
                UserId,
                Success = response != null ? true : false,
                todo = response,
                Errors,
            }); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new TodoVM();
            try
            {
                var todo = _context.Todos.FirstOrDefault(t => t.Id == id);
                var result = todo != null ? _context.Todos.Remove(todo) : null;
                _context.SaveChanges();
                response =_mapper.Map<TodoVM>(result.Entity);
            }
            catch (Exception ex)
            {
                Errors = ex.Message;
                _logger.LogError(ex.Message);
            }            
            
            return Ok(new{
                UserId,
                Success = response != null ? true : false,
                todo = response,
                Errors,
            }); 
        }
    }
}