using Data.Models;
using Infra.Services;
using Infra.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        public readonly ApplicationDbContext _db;
        UnitOfWork uow;
        public PurchaseController(ApplicationDbContext db)
        {
            _db = db;
            this.uow = new UnitOfWork(_db);
        }

        [HttpGet("list")]
        public async Task<ActionResult> list(int pagesize, int page)
        {
            var d = uow.purchaseRepo.GetAll();
            var result = _db.Purchases.AsQueryable();
            var skipindex = pagesize * (page - 1);
            var totalCount = result.Count();

            Model<purchase> model = await PagingService<purchase>.getPaging(page, pagesize, result);


            return Ok(model);
        }
        [HttpGet("checkvoucher")]
        public async Task<ActionResult> checkvoucher(int id=0)
        {
            var result = uow.purchaseRepo.GetAll().Where(a=>a.ID==id).FirstOrDefault();

            return Ok(result);
        }

        [HttpGet("PaymentMethodList")]
        public async Task<ActionResult> PaymentMethodList()
        {
            List<string> paymentmethod = new List<string>();
            List<purchase> result = _db.Purchases.DistinctBy(a => a.PaymentType).ToList();
            foreach(var payment in result)
            {
                paymentmethod.Add(payment.PaymentType);
            }

            return Ok(paymentmethod);
        }
    }
}
