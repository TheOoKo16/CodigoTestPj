using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http;
using Data.Models;
using Infra.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Infra.Services;
using Data.ViewModels;
using Infra.Helper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EvoucherController : ControllerBase
    {
        public readonly ApplicationDbContext _db;

        UnitOfWork uow;
        public EvoucherController(ApplicationDbContext db)
        {

            
            _db = db;
            this.uow = new UnitOfWork(_db);
        }

        [HttpGet("list")]
        public async Task<ActionResult> list(int pagesize,int page)
        {
            var result = _db.Evouchers.AsQueryable();
            var skipindex = pagesize * (page - 1);
            var totalCount = result.Count();

            Model<evoucher> model = await PagingService<evoucher>.getPaging(page, pagesize, result);


            return Ok(model);
        }

        [HttpGet("IsActive")]
        public async Task<ActionResult> IsActive(int id)
        {
            evoucher result =_db.Evouchers.Where(a=>a.ID==id).AsNoTracking().FirstOrDefault();
            if(result.IsActive!=true)
            {
                result.IsActive = true;
            }
            else
            {
                result.IsActive = false;
            }
            uow.evoucherRepo.UpdateWithObj(result);
            return Ok(result);
        }

        [HttpPost("Upsert")]
       
        public async Task<ActionResult> Upsert(evoucher evoucherdata)
        {
            var s = JsonConvert.SerializeObject(evoucherdata);
            FileUploadViewModel fileupload = new FileUploadViewModel();
            evoucher UpdateEntity;
            if(evoucherdata.ID!=0)
            {
                evoucher data = _db.Evouchers.Where(a => a.ID == evoucherdata.ID).AsNoTracking().FirstOrDefault();
                if (evoucherdata.Image!=null)
                {
                    fileupload.photo = evoucherdata.Image;
                    fileupload.filepath = "Evoucher";
                    ImageNameViewModel filename = await FileUploadApiRequestHelper.upload(fileupload);
                    evoucherdata.Image = filename.ImageName;
                }
                else
                {
                    evoucherdata.Image = data.Image;

                }
                
                uow.evoucherRepo.UpdateWithObj(evoucherdata);

                
            }
            else
            {
                fileupload.photo = evoucherdata.Image;
                fileupload.filepath = "Evoucher";
                ImageNameViewModel filename = await FileUploadApiRequestHelper.upload(fileupload);
               
                if(filename!=null)
                {
                    evoucherdata.Image = filename.ImageName;
                }
                

                evoucherdata = uow.evoucherRepo.InsertReturn(evoucherdata);
            }

           
            return Ok(evoucherdata);

        }

        [HttpGet("Edit")]
        public async Task<ActionResult> Edit(int id)
        {
            var result = uow.evoucherRepo.GetAll().Where(a => a.ID == id).FirstOrDefault();

            return Ok(result);

        }
        

        [HttpPost("CheckoutUpsert")]
        public async Task<ActionResult> CheckoutUpsert(CheckOutViewModel checkout)
        {
            StripeConfiguration.ApiKey = "sk_test_51JWbVTKgo0AFtVuQ2FhIUsra1ZNJjgX23KRuoL1G6jwJIaYaR5asN8p5NrCDdsFnhcjBIxwfFWO9NF6ho99LadKE00WQRoOeDC";

            var s = JsonConvert.SerializeObject(checkout);

            evoucher evoucher = _db.Evouchers.Where(a => a.ID == checkout.evoucher.ID).AsNoTracking().FirstOrDefault();
            int? leftquantity = evoucher.Quantity - checkout.customer.Quantity;

            evoucher.Quantity = leftquantity;
            uow.evoucherRepo.UpdateWithObj(evoucher);

            customer customer = new customer();
            customer.Name = checkout.customer.Name;
            customer.PhoneNo = checkout.customer.PhoneNo;
            customer.Quantity = checkout.customer.Quantity;
            customer.BuyType = checkout.customer.BuyType;
            customer.PaymentMethod = checkout.customer.PaymentMethod;
            customer.EvoucherID = checkout.customer.EvoucherID;
            customer.TotalAmount = checkout.customer.TotalAmount;
            customer.Discount = checkout.customer.Discount;
            customer = uow.customerRepo.InsertReturn(customer);



           
            List<purchase> plist = new List<purchase>();
            for(int i=0;i<checkout.customer.Quantity;i++)
            {
                string promocode = generatePromoCode(customer.PhoneNo);
                purchase purchase = new purchase();
                purchase.EvoucherID = evoucher.ID;
                purchase.Title = evoucher.Title;
                purchase.CustomerID = customer.ID;
                purchase.Name = customer.Name;
                purchase.PaymentType = checkout.customer.PaymentMethod;
                purchase.Quantity = "1";
                purchase.PromoCode = promocode;
                purchase.IsUsed = false;
                purchase.Amount = evoucher.Amount;
                plist.Add(purchase);
            }
             uow.purchaseRepo.InsertList(plist);
            CheckOutViewModel result = new CheckOutViewModel();
            result.evoucher = evoucher;
            result.customer = customer;
            result.purchase = plist;

            var paymentIntents = new PaymentIntentService();

            var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            {
                Amount =Convert.ToInt32(checkout.customer.TotalAmount) ,
                Currency = "usd",
            });
            result.secretkey = paymentIntent.ClientSecret;
            //return Json(new { clientSecret = paymentIntent.ClientSecret });


            return Ok(result);
        }
        public static string generatePromoCode(string phone)
        {
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var alpha = new string(Enumerable.Repeat(alphabet, 5).Select(s => s[random.Next(s.Length)]).ToArray());
            var numer = new string(Enumerable.Repeat(phone, 6).Select(s => s[random.Next(s.Length)]).ToArray());
            var result = alpha + numer;
            string promocodes = new string(result.ToCharArray().OrderBy(s => (random.Next(2) % 2) == 0).ToArray());
            return promocodes;
        }
        [HttpGet("CheckoutItem")]
        public async Task<ActionResult> CheckoutItem(string promocode)
        {
            purchase purchase = _db.Purchases.Where(a => a.PromoCode == promocode).AsNoTracking().FirstOrDefault();
           if(purchase.IsUsed==true)
            {
                return Ok("Purchase Fail.You have already used");
            }
            else
            {
                purchase.IsUsed = true;
                uow.purchaseRepo.UpdateWithObj(purchase);
                return Ok("Purchase Success");

            }
        }

        [HttpGet("CheckPromoCode")]
        public async Task<ActionResult> CheckPromoCode(string promocode)
        {
            purchase purchase = uow.purchaseRepo.GetAll().Where(a => a.PromoCode == promocode).FirstOrDefault();
            if(purchase.IsUsed==true)
            {
                return Ok("Evoucher is Used");
            }
            else
            {
                return Ok("Evoucher is not used");
            }
        }


    }
}