using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecureWebAPI.BusinessLogic;
using SecureWebAPI.DataAccess.Entities;
using SecureWebAPI.Extensions;
using SecureWebAPI.Models;
using SecureWebAPI.BusinessLogic.Request;
using SecureWebAPI.BusinessLogic.Response;
using SecureWebAPI.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Authorization;


namespace SecureWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthController(IServiceManager service)
        {
            _service = service;
        }     
 
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserVM model)
        {
            var request = new UserRequest{User = model};  
            var response = await _service.RegisterUser(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserVM model)
        {
            var request = new UserRequest { User = model };
            var response = await _service.LoginUser(request);
            return Ok(response);           
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var request = new UserRequest();
            var response = await _service.LogOutUser(request);
            return Ok(response);
        }
    }
}