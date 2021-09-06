using Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Infra.Services
{
    public class AuthServices
    {
        //public void AuthorizeUser(Members member, HttpContext context)
        //{
            //if(member != null)
            //{
            //    var identity = new[] {
            //    new Claim(ClaimTypes.Email, member.AuthorizedDealerEmail),
            //    new Claim(ClaimTypes.Name,member.Username),
            //    new Claim(ClaimTypes.Sid, Convert.ToString(member.MemberId)),
            //    new Claim(ClaimTypes.Actor, member.Role),
            //    new Claim(ClaimTypes.Uri, member.Photo)
            //    };

            //    var claimsIdentity = new ClaimsIdentity(identity, CookieAuthenticationDefaults.AuthenticationScheme);
            //    var principle = new ClaimsPrincipal();
            //    principle.AddIdentity(claimsIdentity);

            //    context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);

            //}

        //}

        //public void LogoutUser(HttpContext context)
        //{
        //    context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //}
    }
}
