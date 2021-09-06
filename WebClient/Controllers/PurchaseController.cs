using Data.Models;
using Infra.Services;
using Infra.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    public class PurchaseController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> purchaselist(int pagesize = 0, int page = 0)
        {
            ViewBag.pagesize = pagesize;
            ViewBag.page = page;
            var token = HttpContext.Session.GetString("token");

            PagedListClient<purchase> model = await PurchaseApiRequestHelper.purchaselist(token, pagesize, page);

            return PartialView("_List", model);

        }
        public async Task<ActionResult> checkevoucher(int id=0)
        {
            var token = HttpContext.Session.GetString("token");
            purchase result = await PurchaseApiRequestHelper.checkvoucher(id,token);
            return PartialView("_QRForm", result);
        }

    }
}
