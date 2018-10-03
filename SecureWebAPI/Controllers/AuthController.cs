using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Request;
using BusinessLogic.Service;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SecureWebAPI.Extensions;
using SecureWebAPI.Models;

namespace SecureWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IConfiguration _configuration;
        //private readonly IMapper _mapper;
        private readonly IServiceManager _service;



        public AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IConfiguration configuration,  IServiceManager service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            //_mapper = mapper;
            _service = service;
        }     
 
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserVM model)
        {
            //var user = _mapper.Map<UserEntity>(model);
            //var result = await _userManager.CreateAsync(user, model.Password);
            //string token = null;
            //if(result.Succeeded)
            //    token = await user.GenerateJwtToken(_configuration);

            //return Ok(new{
            //    token = token,
            //    Success = result.Succeeded,
            //    Errors = result.Errors
            //});
            var request = new RegisterUserRequest { User = new User { UserName = model.UserName, Password = model.Password } };
            var response = _service.RegisterUser(request);
            return Ok(response);
        }

        [HttpGet]        
        public async Task<IActionResult> Logout()
        {
             await _signInManager.SignOutAsync();   
             return Ok(new {Message = "User logout"}); 
        }

        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody] UserVM model)
        //{
        //    var userName = _mapper.Map<UserEntity>(model).UserName;
        //    var user = await _userManager.FindByNameAsync(userName);
        //    var signIn = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
        //    var errors = new List<string>();
        //    string token = null;
        //    if (user == null) 
        //        errors.Add("User not found");
        //    else if (!await _userManager.CheckPasswordAsync(user, model.Password))
        //        errors.Add("User password incorrect");            
        //    else
        //        token = await user.GenerateJwtToken(_configuration); 
            
        //    return Ok(new{
        //        token,
        //        Success = !string.IsNullOrEmpty(token) ? true : false,
        //        Errors = errors
        //    });
        //}        
    }
}