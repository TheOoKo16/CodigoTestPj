using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Infra.Helper;
using System.Web.Http;
using Microsoft.AspNetCore.Http;

namespace WebClient.Controllers
{
    public class AccountController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Login(string username = null,string password=null)
        
        {
            user userdata = new user();
            userdata.UserName = username;
            userdata.Password = password;
            var token = await AccountApiRequestHelper.gettoken(userdata);
            if(token!=null)
            {
                //var token = HttpContext.Session.GetString("_token");
                 HttpContext.Session.SetString("token",token.token);
                return RedirectToAction("Index","Evoucher");
            }
            else
            {
                return RedirectToAction("Index","Account");

            }
           

        }

    }
}
