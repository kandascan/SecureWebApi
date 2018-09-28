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
        private readonly IConfiguration _configuration;
 
        public AuthController(ApplicationDbContext context, UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
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
                return await user.GenerateJwtToken(_configuration);
            }
            
            return new BadRequestObjectResult(result.Errors);
        }

        [HttpPost]
        public async Task<object> Login([FromBody] User model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null) 
                return new BadRequestObjectResult("User not found");

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                return new BadRequestObjectResult("User password incorrect");

            return await user.GenerateJwtToken(_configuration);           
        }        
    }
}