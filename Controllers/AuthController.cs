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
using SecureWebAPI.Entities;
using SecureWebAPI.Extensions;
using SecureWebAPI.Models;

namespace SecureWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
 
        public AuthController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }     
 
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserVM model)
        {
            var user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            string token = null;
            if(result.Succeeded)
                token = await user.GenerateJwtToken(_configuration);
            
            return Ok(new{
                token = token,
                Success = result.Succeeded,
                Errors = result.Errors
            });
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
            var userName = _mapper.Map<User>(model).UserName;
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