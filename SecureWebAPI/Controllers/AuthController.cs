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

namespace SecureWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IServiceManager _service;
        private readonly IUnitOfWork _uow;
 
        public AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, 
        IConfiguration configuration, IMapper mapper, IServiceManager service, IUnitOfWork uow)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
            _service = new ServiceManager(uow, mapper, userManager, signInManager, configuration);
            _uow = uow;
        }     
 
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserVM model)
        {
            var request = new RegisterUserRequest{User = model, RequestId = new Guid()};  
            var response = await _service.RegisterUser(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
             await _signInManager.SignOutAsync();   
             return Ok(new {Message = "User logout"}); 
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserVM model)
        {
            var userName = _mapper.Map<UserEntity>(model).UserName;
            var user = await _userManager.FindByNameAsync(userName);
            var signIn = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            var errors = new List<string>();
            string token = null;
            if (user == null) 
                errors.Add("User not found");
            else if (!await _userManager.CheckPasswordAsync(user, model.Password))
                errors.Add("User password incorrect");            
            else
                token = await user.GenerateJwtToken(_configuration); 
            
            return Ok(new{
                token = token,
                Success = !string.IsNullOrEmpty(token) ? true : false,
                Errors = errors
            });
        }        
    }
}