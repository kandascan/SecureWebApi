using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureWebAPI.Extensions;
using SecureWebAPI.Models;

namespace SecureWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        private readonly ClaimsPrincipal claimsPrincipal;
        public ValuesController(IHttpContextAccessor httpContextAccessor)
        {
            claimsPrincipal = httpContextAccessor.HttpContext.User;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var userId = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return new string[] { userId, "value1", "value2" };
        }

        [HttpGet]
        [Route("posts")]
        public ActionResult<IEnumerable<Post>> GetPosts()
        {
            return new List<Post>
            {
                new Models.Post{Id = "1", UserId = claimsPrincipal.Identity.ToString()},
                new Models.Post{Id = "1", UserId = claimsPrincipal.Identity.ToString()},
                new Models.Post{Id = "1", UserId = claimsPrincipal.Identity.ToString()}
            };
        }

        //[Authorize(Policy = "ApiUser")]
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
