using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Infra.Helper;
using Microsoft.AspNetCore.Authorization;
using Data.ViewModels;

namespace WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IJWTAuthenticationManager jWTAuthenticationManager;

        public AccountController(IJWTAuthenticationManager jWTAuthenticationManager)
        {
            this.jWTAuthenticationManager = jWTAuthenticationManager;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] user userCred)
        {
            var token = jWTAuthenticationManager.Authenticate(userCred.UserName, userCred.Password);
            UserTokenViewModel usertoken = new UserTokenViewModel();
            usertoken.token = token;
            if (token == null)
                return Unauthorized();

            return Ok(usertoken);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "New York", "New Jersey" };
        }

        // GET: api/Name/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

      

    }
}
