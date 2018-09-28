using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecureWebAPI.Entities;
using SecureWebAPI.Helpers;
using SecureWebAPI.Models;

namespace SecureWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
 
        public AuthController(ApplicationDbContext context, UserManager<User> userManager,
            SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }     
 
        [HttpPost]
        public async Task<object> Register([FromBody] User model)
        {
            var user = new User
            {
                UserName = model.Email, 
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return await TokenHelper.GenerateJwtToken(model.Email, user, _configuration);
            }
            
            return new BadRequestObjectResult(result.Errors);
        }

        [HttpPost]
        public async Task<object> Login([FromBody] User model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult("User not found");
            }

            var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
            return await TokenHelper.GenerateJwtToken(model.Email, appUser, _configuration);            
        }        
    }
}