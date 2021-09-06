using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Infra.Helper;
using Microsoft.AspNetCore.Http;
using Infra.Services;
using Data.ViewModels;
namespace WebClient.Controllers
{
    public class EvoucherController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }


        public async Task<ActionResult> evoucherlist(int pagesize = 0,int page=0)
        {
            ViewBag.pagesize= pagesize;
            ViewBag.page = page;
            var token = HttpContext.Session.GetString("token");

            PagedListClient<evoucher> model = await EvoucherApiRequestHelper.evoucherlist(token,pagesize,page);

            return PartialView("_List", model);

        }
        
        public async Task<ActionResult> IsActive(int id)
        {
            var token = HttpContext.Session.GetString("token");
            evoucher model = await EvoucherApiRequestHelper.IsActive(id,token);

            if (model != null)
            {
                return Json("Success");
            }
            else
            {
                return Json("Fail");
            }

        }
        public async Task<ActionResult> Add()
        {
            evoucher result = new evoucher();
            return View(result);
        }
        public async Task<ActionResult> Upsert(evoucher evoucherdata)
        {
            var token = HttpContext.Session.GetString("token");
            evoucher model = await EvoucherApiRequestHelper.Upsert(evoucherdata,token);
            if (model != null)
            {
                return Json("Success");
            }
            else
            {
                return Json("Fail");
            }

           

        }
        public async Task<ActionResult> Edit(int id)
        {
            var token = HttpContext.Session.GetString("token");

            evoucher model = await EvoucherApiRequestHelper.Edit(id,token);
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return Json("Fail");
            }
        }
        public async Task<ActionResult> Detail(int id)
        {
            var token = HttpContext.Session.GetString("token");

            evoucher model = await EvoucherApiRequestHelper.Edit(id, token);
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return Json("Fail");
            }
        }

        public async Task<ActionResult> Checkout(int id)
        {
            var token = HttpContext.Session.GetString("token");

            evoucher model = await EvoucherApiRequestHelper.Edit(id, token);
            CheckOutViewModel result = new CheckOutViewModel();
            result.evoucher = model;
            result.customer = new customer();
            result.purchase = new List<purchase>();
            return View(result);
        }

            public async Task<ActionResult> CheckoutUpsert(CheckOutViewModel checkout)
        {
            var token = HttpContext.Session.GetString("token");

            CheckOutViewModel model = await EvoucherApiRequestHelper.CheckoutUpsert(checkout, token);
            if (model != null)
            {
                return Json(model);
            }
            else
            {
                return Json("Fail");
            }
            
        }

    }
}

